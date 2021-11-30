using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GUI.Models;
using GUI.Services;
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

        public async Task<IActionResult> Index(
            [FromQuery]int? sensorId, 
            [FromQuery]string sensorType,
            [FromQuery]DateTime? dateTime, 
            [FromQuery]double? value, 
            [FromQuery]int? page,
            [FromQuery] int? pageSize,
            [FromQuery]string sortBy, 
            [FromQuery]bool? ascending)
        {
            if(!page.HasValue || !pageSize.HasValue)
                return RedirectToAction("Index", new
                {
                    page = 1,
                    sensorId,
                    dateTime,
                    sensorType,
                    value,
                    pageSize = 20,
                    sortBy,
                    ascending
                });
            
            string jsonResponseSensorData = await _apiService.GetSensorData(sensorId, sensorType,
                dateTime, value, page, pageSize,
                sortBy, ascending);

            ViewBag.raw_json = jsonResponseSensorData;

            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);
            
            ViewBag.json_response = sensorData;
            
            return View();
        }
        
        [HttpPost]
        [Route("/setFilter")]
        public async Task<IActionResult> setFilter(
            FilterTableForm filterTableForm,
            [FromQuery]double? value, 
            [FromQuery]int? page,
            [FromQuery] int? pageSize,
            [FromQuery]string sortBy, 
            [FromQuery]bool? ascending)
        {
            int? sensorId = filterTableForm.sensorId == 0 ? null : filterTableForm.sensorId;
            string sensorType = filterTableForm.sensorType == "" ? null : filterTableForm.sensorType;
            DateTime? dateTime = filterTableForm.dateTime == DateTime.MinValue ? null : filterTableForm.dateTime;

            return RedirectToAction("Index", new
            {
                page,
                sensorId,
                dateTime,
                sensorType,
                value,
                pageSize,
                sortBy,
                ascending
            });
        }

        [HttpPost]
        [Route("/setPage")]
        public async Task<IActionResult> SetPage(
            [FromForm(Name = "page")] int pageNumber,
            [FromQuery]int? sensorId, 
            [FromQuery]string sensorType,
            [FromQuery]DateTime? dateTime, 
            [FromQuery]double? value,
            [FromQuery]int? pageSize,
            [FromQuery]string sortBy, 
            [FromQuery]bool? ascending)
        {
            return RedirectToAction("Index", new
            {
                page = pageNumber,
                sensorId,
                dateTime,
                sensorType,
                value,
                pageSize,
                sortBy,
                ascending
            });
        }
        
        [HttpPost]
        [Route("/setSort")]
        public async Task<IActionResult> setSort(
            [FromForm(Name = "sortBy")] string sortBy,
            [FromQuery]int? sensorId, 
            [FromQuery]string sensorType,
            [FromQuery]DateTime? dateTime, 
            [FromQuery]double? value,
            [FromQuery] int? page,
            [FromQuery]int? pageSize,
            [FromQuery]bool? ascending)
        {
            bool newAscending = ascending.HasValue? !ascending.Value: true;
            return RedirectToAction("Index", new
            {
                page = page,
                sensorId = sensorId,
                dateTime = dateTime,
                sensorType = sensorType,
                value = value,
                pageSize = pageSize,
                sortBy = sortBy,
                ascending = newAscending
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

       
 
    }
}