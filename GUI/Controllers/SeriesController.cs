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
        
        [Route("/Series")]
        public async Task<IActionResult> Series()
        {

            return View("GraphForm");
        }

        [Route("/Series/Graph")]
        public async Task<IActionResult> Graph(GraphForm graphForm)
        {
            int? sensorId = graphForm.sensorId == 0 ? null : graphForm.sensorId;
            string? sensorType = graphForm.sensorType == "" ? null : graphForm.sensorType;
            DateTime? dateTime = graphForm.dateTime == DateTime.MinValue ? null : graphForm.dateTime;
            
            string jsonResponseSensorData = await _apiService.GetSensorData(sensorId, sensorType,
                dateTime, null, null, null,
                null, null);
            
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);

            @ViewBag.name = graphForm.sensorType;
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