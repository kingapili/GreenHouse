using System.Collections.Generic;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController
    {
        private readonly SensorsService _service;

        public SensorsController(SensorsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetSensorData")]
        public ActionResult<List<SensorData>> GetSensorData()
        {
            return _service.Get();
        }
    }
}