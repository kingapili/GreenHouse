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
    public class PulpitController : Controller
    {
        private readonly ILogger<PulpitController> _logger;
        private readonly IApiService _apiService;


        public PulpitController(ILogger<PulpitController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Pulpit()
        {
            string jsonResponseSensorData = await _apiService.GetSensorData(null,
                "Model.SolarRadiationSensor",
                null, null, 1, 100,
                null, null);
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);

            if (sensorData != null)
            {
                ViewBag.solar_last = sensorData[^1].value;


                Double solarMean = 0.0;
                for (int i = 0; i < sensorData.Count; i++)
                {
                    solarMean += sensorData[i].value;
                }

                ViewBag.solar_mean = solarMean / sensorData.Count;

            }
            
            jsonResponseSensorData = await _apiService.GetSensorData(null,
                "Model.HumiditySensor",
                null, null, 1, 100,
                null, null);
            sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);

            if (sensorData != null)
            {
                ViewBag.humitidy_last = sensorData[^1].value;


                Double Mean = 0.0;
                for (int i = 0; i < sensorData.Count; i++)
                {
                    Mean += sensorData[i].value;
                }

                ViewBag.humitidy_mean = Mean / sensorData.Count;

            }
            
            jsonResponseSensorData = await _apiService.GetSensorData(null,
                "Model.CarbonDioxideSensor",
                null, null, 1, 100,
                null, null);
            sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);

            if (sensorData != null)
            {
                ViewBag.carbon_last = sensorData[^1].value;


                Double Mean = 0.0;
                for (int i = 0; i < sensorData.Count; i++)
                {
                    Mean += sensorData[i].value;
                }

                ViewBag.carbon_mean = Mean / sensorData.Count;

            }
            
            jsonResponseSensorData = await _apiService.GetSensorData(null,
                "Model.TemperatureSensor",
                null, null, 1, 100,
                null, null);
            sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);

            if (sensorData != null)
            {
                ViewBag.temperature_last = sensorData[^1].value;


                Double Mean = 0.0;
                for (int i = 0; i < sensorData.Count; i++)
                {
                    Mean += sensorData[i].value;
                }

                ViewBag.temperature_mean = Mean / sensorData.Count;

            }
            return View();
        }
    }
}