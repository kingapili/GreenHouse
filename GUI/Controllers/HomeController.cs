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
                null, null, 1, 50,
                null, null);

            ViewBag.raw_json = jsonResponseSensorData;

            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            
            ViewBag.json_response = sensorData;
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Filter(FilterTableForm filterForm)
        {
            string jsonResponseSensorData = await _apiService.GetSensorData(filterForm.sensorId, filterForm.sensorType,
                null, null, 1, 50,
                null, null);
            ViewBag.raw_json = jsonResponseSensorData;
            
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            
            ViewBag.json_response = sensorData;
            
            return View("Index");
            
        }

        [HttpGet]
        public async Task<IActionResult> GetJson(FilterJsonForm filterForm)
        {
            string jsonResponseSensorData = await _apiService.GetSensorData(filterForm.sensorId, filterForm.sensorType,
                null, null, 1, 50,
                null, null);
            return Ok(jsonResponseSensorData);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCSV(FilterCSVForm filterForm)
        {
            String csvResponseSensorData = await _apiService.GetSensorDataInFormat("csv",filterForm.sensorId, filterForm.sensorType,
                null, null, 1, 50,
                null, null);
            return Ok(csvResponseSensorData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet]
        [Route("/{sort}")]
        public async Task<IActionResult> GetFiltered([FromRoute(Name = "sort")] string sortBy = null)
        {
            
            string jsonResponseSensorData = await _apiService.GetSensorData(null, null,
                null, null, 1, 50,
                sortBy, null);
            ViewBag.raw_json = jsonResponseSensorData;
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            ViewBag.json_response = sensorData;
            
            return View("Index");
        }

        public IActionResult JsonForm()
        {
            return View("FilterJsonForm");
        }
        
        public IActionResult CSVForm()
        {
            return View("FilterCSVForm");
        }
    }
}