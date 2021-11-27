using System;
using System.Collections.Generic;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class SensorsController
    {
        private readonly SensorsService _service;

        public SensorsController(SensorsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetSensorData")]
        public ActionResult<List<SensorData>> GetSensorData([FromQuery]int? sensorId, [FromQuery]string sensorType,
            [FromQuery]DateTime? dateTime, [FromQuery]double? value, [FromQuery]int? page,[FromQuery] int? pageSize,
            [FromQuery]string sortBy, [FromQuery]bool? ascending)
        {
            return _service.GetAll(sensorId, sensorType, dateTime, value, page, pageSize, sortBy, ascending);
        }
        
        [HttpGet]
        [Route("GetSensorDataInFormat/{format}")]
        public ActionResult<List<SensorData>> GetSensorDataInFormat([FromQuery]int? sensorId, [FromQuery]string sensorType,
            [FromQuery]DateTime? dateTime, [FromQuery]double? value, [FromQuery]int? page,[FromQuery] int? pageSize,
            [FromQuery]string sortBy, [FromQuery]bool? ascending)
        {
            return _service.GetAll(sensorId, sensorType, dateTime, value, page, pageSize, sortBy, ascending);
        }
    }
}