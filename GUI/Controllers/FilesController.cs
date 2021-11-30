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
    public class FilesController : Controller
    {
        private readonly ILogger<FilesController> _logger;
        private readonly IApiService _apiService;


        public FilesController(ILogger<FilesController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        [HttpGet]
        [Route("/GetJson")]
        [Produces("application/json")]
        public async Task<IActionResult> GetJson(
            [FromQuery]int? sensorId, 
            [FromQuery]string sensorType,
            [FromQuery]DateTime? startDateTime, 
            [FromQuery]DateTime? endDateTime,
            [FromQuery]double? value, 
            [FromQuery]int? page,
            [FromQuery] int? pageSize,
            [FromQuery] string sortBy,
            [FromQuery] bool? ascending)
        {
            string jsonResponseSensorData = await _apiService.GetSensorDataInFormat("json",sensorId, sensorType,
                startDateTime, endDateTime, value, page, pageSize,
                sortBy, ascending);
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(jsonResponseSensorData));  
            return new FileStreamResult(stream, new MediaTypeHeaderValue("application/json"))  
            {  
                FileDownloadName = "data.json"  
            }; 
        }
        
        [HttpGet]
        [Route("/GetCSV")]
        [Produces("text/csv")]
        public async Task<IActionResult> GetCSV(
            [FromQuery]int? sensorId, 
            [FromQuery]string sensorType,
            [FromQuery]DateTime? startDateTime, 
            [FromQuery]DateTime? endDateTime,
            [FromQuery]double? value, 
            [FromQuery]int? page,
            [FromQuery] int? pageSize,
            [FromQuery] string sortBy,
            [FromQuery] bool? ascending)
        {
            String csvResponseSensorData = await _apiService.GetSensorDataInFormat("csv",sensorId, sensorType,
                startDateTime, endDateTime, value, page, pageSize,
                sortBy, ascending);
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(csvResponseSensorData));  
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/csv"))  
            {  
                FileDownloadName = "data.csv"  
            };  
        }
    }
}