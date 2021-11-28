using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GUI.Models;
using GUI.Services;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;


        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            string jsonResponseSensorData = await _apiService.GetSensorData(null, null,
                null, null, 1, 20,
                null, null);

            ViewBag.raw_json = jsonResponseSensorData;

            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            
            ViewBag.json_response = sensorData;
            ViewBag.currentPage = 1;
            ViewBag.currentsort = "DataTime";

            ViewBag.sensorId = null;
            ViewBag.sensorType = null;
            ViewBag.dataTime = null;
            
            return View();
        }

        [HttpGet]
        [Route("/{sortBy}/{page}")]
        public async Task<IActionResult> GetTablePage(
            [FromRoute(Name = "page")] int pageNumber,
            [FromRoute(Name = "sortBy")] string sortBy, 
            [FromQuery]int? sensorId, 
            [FromQuery]string sensorType,
            [FromQuery]DateTime? dateTime)
        {
            

            string jsonResponseSensorData = await _apiService.GetSensorData(sensorId, sensorType,
                dateTime, null, pageNumber, 20,
                sortBy, null);

            ViewBag.raw_json = jsonResponseSensorData;
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            ViewBag.json_response = sensorData;
            
            ViewBag.currentPage = pageNumber;
            ViewBag.currentsort = sortBy;
            
            ViewBag.sensorId = sensorId;
            ViewBag.sensorType = sensorType;
            ViewBag.dataTime = sensorData;
            
            return View("Index");
        }

        [HttpPost]
        [Route("/{sortBy}/{page}")]
        public async Task<IActionResult> SetTableFilters(
            [FromRoute(Name = "page")] int pageNumber,
            [FromRoute(Name = "sortBy")] string sortBy,
            FilterTableForm filterTableForm)
        {
            int? sensorId = filterTableForm.sensorId == 0 ? null : filterTableForm.sensorId;
            string? sensorType = filterTableForm.sensorType == "" ? null : filterTableForm.sensorType;
            DateTime? dateTime = filterTableForm.dateTime == DateTime.MinValue ? null : filterTableForm.dateTime;

            string jsonResponseSensorData = await _apiService.GetSensorData(sensorId, sensorType,
                dateTime, null, pageNumber, 20,
                sortBy, null);

            ViewBag.raw_json = jsonResponseSensorData;
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            ViewBag.json_response = sensorData;
            
            ViewBag.currentPage = pageNumber;
            ViewBag.currentsort = sortBy;
            
            ViewBag.sensorId = sensorId;
            ViewBag.sensorType = sensorType;
            ViewBag.dataTime = sensorData;
            
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

       
 
    }
}