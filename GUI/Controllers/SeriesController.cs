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
    public class SeriesController : Controller
    {
        private readonly ILogger<SeriesController> _logger;
        private readonly IApiService _apiService;


        public SeriesController(ILogger<SeriesController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        

        [Route("/Series/Graph")]
        public async Task<IActionResult> Graph(
            [FromQuery]int? sensorId, 
            [FromQuery]string sensorType,
            [FromQuery]DateTime? dateTime, 
            [FromQuery]double? value, 
            [FromQuery]int? page,
            [FromQuery] int? pageSize)
        {
            string jsonResponseSensorData = await _apiService.GetSensorData(sensorId, sensorType,
                dateTime, value, page, pageSize,
                null, null);
            
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);

            @ViewBag.name = "Graf for";
            List<double> values = new List<double>();

            for (int i = 0; i < sensorData.Count; i++)
            {
                values.Add(sensorData[i].value);
            }

            @ViewBag.data ="[" +  string.Join( ",", values.ToArray() ) + "]";

            return View("Series");
        }
    }
}