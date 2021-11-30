using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using GUI.Model;
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
            [FromQuery]DateTime? startDateTime, 
            [FromQuery]DateTime? endDateTime,
            [FromQuery]double? value, 
            [FromQuery]int? page,
            [FromQuery] int? pageSize,
            [FromQuery] string sortBy,
            [FromQuery] bool? ascending
            )
        {
            string jsonResponseSensorData = await _apiService.GetSensorData(sensorId, sensorType,
                startDateTime, endDateTime,  value, page, pageSize,
                sortBy, ascending);
            
            List<SensorData> sensorData = JsonSerializer.Deserialize<List<SensorData>>(jsonResponseSensorData);

            @ViewBag.name = "Graf for ";
            if(sensorType!= null)
                @ViewBag.name += sensorType + " type ";
            if(sensorId.HasValue)
                @ViewBag.name += "sensorId: " + sensorId.Value ;
            if(startDateTime.HasValue)
                @ViewBag.name += "from date: " + startDateTime.Value + " ";
            if(endDateTime.HasValue)
                @ViewBag.name += "to date: " + endDateTime.Value + " ";
                
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