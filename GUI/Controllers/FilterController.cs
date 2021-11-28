using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using GUI.Models;
using GUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> GetJson(FilterJsonForm filterForm)
        {
            int? sensorId = filterForm.sensorId == 0 ? null : filterForm.sensorId;
            string? sensorType = filterForm.sensorType == "" ? null : filterForm.sensorType;
            DateTime? dateTime = filterForm.dateTime == DateTime.MinValue ? null : filterForm.dateTime;
            string jsonResponseSensorData = await _apiService.GetSensorData(sensorId, sensorType,
                dateTime, null, null, null,
                null, null);
            return Ok(jsonResponseSensorData);
        }
        
        [HttpGet]
        [Route("/GetCSV")]
        public async Task<IActionResult> GetCSV(FilterCSVForm filterForm)
        {
            int? sensorId = filterForm.sensorId == 0 ? null : filterForm.sensorId;
            string? sensorType = filterForm.sensorType == "" ? null : filterForm.sensorType;
            DateTime? dateTime = filterForm.dateTime == DateTime.MinValue ? null : filterForm.dateTime;
            String csvResponseSensorData = await _apiService.GetSensorDataInFormat("csv",sensorId, sensorType,
                dateTime, null, null, null,
                null, null);
            return Ok(csvResponseSensorData);
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
                filterForm.dateTime, null, 1, 50,
                null, null);
            ViewBag.raw_json = jsonResponseSensorData;
            
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            
            ViewBag.json_response = sensorData;
            
            return View("FilteredTableView");
            
        }
    }
}