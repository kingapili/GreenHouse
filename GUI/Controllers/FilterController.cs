using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GUI.Models;
using GUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace GUI.Controllers
{
    public class FilterController : Controller
    {
        private readonly ILogger<FilterController> _logger;
        private readonly IApiService _apiService;


        public FilterController(ILogger<FilterController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        [HttpGet]
        [Route("/GetJson")]
        [Produces("application/json")]
        public async Task<IActionResult> GetJson(FilterJsonForm filterForm)
        {
            int? sensorId = filterForm.sensorId == 0 ? null : filterForm.sensorId;
            string? sensorType = filterForm.sensorType == "" ? null : filterForm.sensorType;
            DateTime? startDateTime = filterForm.startDatetime == DateTime.MinValue ? null : filterForm.startDatetime;
            DateTime? endDateTime = filterForm.endDatetime == DateTime.MinValue ? null : filterForm.endDatetime;
            string jsonResponseSensorData = await _apiService.GetSensorDataInFormat("json", sensorId, sensorType,
                startDateTime, endDateTime, null, null, null,
                null, null);
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(jsonResponseSensorData));  
            return new FileStreamResult(stream, new MediaTypeHeaderValue("application/json"))  
            {  
                FileDownloadName = "data.json"  
            }; 
        }
        
        [HttpGet]
        [Route("/GetCSV")]
        [Produces("text/csv")]
        public async Task<IActionResult> GetCSV(FilterCSVForm filterForm)
        {
            int? sensorId = filterForm.sensorId == 0 ? null : filterForm.sensorId;
            string? sensorType = filterForm.sensorType == "" ? null : filterForm.sensorType;
            DateTime? startDateTime = filterForm.startDatetime == DateTime.MinValue ? null : filterForm.startDatetime;
            DateTime? endDateTime = filterForm.endDatetime == DateTime.MinValue ? null : filterForm.endDatetime;
            String csvResponseSensorData = await _apiService.GetSensorDataInFormat("csv",sensorId, sensorType,
                startDateTime, endDateTime, null, null, null,
                null, null);
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(csvResponseSensorData));  
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/csv"))  
            {  
                FileDownloadName = "data.csv"  
            };  
        }
        
        [Route("/JsonForm")]
        public IActionResult JsonForm()
        {
            return View("FilterJsonForm");
        }
        
        [Route("/CSVForm")]
        public IActionResult CSVForm()
        {
            return View("FilterCSVForm");
        }

        [Route("/Filter")]
        public IActionResult FilterForm()
        {
            return View("FilterForm");
        }
        
        [HttpPost]
        [Route("/Filter/Filter")]
        public async Task<IActionResult> Filter(FilterTableForm filterForm)
        {
            string jsonResponseSensorData = await _apiService.GetSensorData(filterForm.sensorId, filterForm.sensorType,
                filterForm.startDatetime,filterForm.endDatetime, null, 1, 50,
                null, null);
            ViewBag.raw_json = jsonResponseSensorData;
            
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            
            ViewBag.json_response = sensorData;
            
            return View("FilteredTableView");
            
        }
    }
}