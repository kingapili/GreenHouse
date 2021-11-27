using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Model;
using Sensors.Services;
using Sensors.Utils;

namespace Sensors.Controllers
{
    [Route("[controller]")]
    public class SensorsController : Controller
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISensorService _sensorService;

        public SensorsController(IPublishEndpoint publishEndpoint, ISensorService sensorService)
        {
            _publishEndpoint = publishEndpoint;
            _sensorService = sensorService;
        }

        /// <summary>
        /// Get a List of all available sensors.
        /// </summary>
        /// <returns>List of sensors</returns>
        [HttpGet("")]
        public IEnumerable<ISensor> GetSensors()
        {
            foreach (var sensor in _sensorService.GetSensors())
            {
                Console.WriteLine(sensor);
            }

            return _sensorService.GetSensors();
        }

        /// <summary>
        /// Get a sensor with given id.
        /// </summary>
        /// <returns>Sensor with a given id</returns>
        [HttpGet("{id:int}")]
        public ISensor GetSensor(int id)
        {
            return _sensorService.GetSensor(id);
        }

        /// <summary>
        /// Generate one SensorData object from a sensor with a given id
        /// </summary>
        /// <param name="id">Given sensor id</param>
        /// <returns>Http status code indicating if single SensorData object was successfully generated</returns>
        [HttpGet("{id:int}/generate-single")]
        public async Task<IActionResult> GenerateSingleDataFromSensor(int id)
        {
            var sensorData = _sensorService.GetSensor(id).GenerateSingleValue();

            try
            {
                await _publishEndpoint.Publish(sensorData);
                await Console.Out.WriteLineAsync(SensorUtils.SensorDataToJson(sensorData));
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.StackTrace);
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Generate one SensorData object for every existing sensor
        /// </summary>
        /// <returns>Http status code indicating if all SensorData objects were successfully generated</returns>
        [HttpGet("generate-single")]
        public async Task<IActionResult> GenerateSingleDataFromAllSensors()
        {
            var sensors = _sensorService.GetSensors();
            foreach (var sensor in sensors)
            {
                try
                {
                    var sensorData = sensor.GenerateSingleValue();
                    await _publishEndpoint.Publish(sensorData);
                    await Console.Out.WriteLineAsync(SensorUtils.SensorDataToJson(sensorData));
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.StackTrace);
                    return BadRequest();
                }
            }
            return Ok();
        }

        /// <summary>
        /// Change single sensor IsRunning properties to true
        /// </summary>
        /// <returns>Http status code</returns>
        [HttpGet("{id:int}/start")]
        public IActionResult StartSensor(int id)
        {
            try
            {
                _sensorService.GetSensor(id).IsRunning = true;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Change single sensor IsRunning properties to false
        /// </summary>
        /// <returns>Http status code</returns>
        [HttpGet("{id:int}/stop")]
        public IActionResult StopSensor(int id)
        {
            try
            {
                _sensorService.GetSensor(id).IsRunning = false;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Change all sensor IsRunning properties to true
        /// </summary>
        /// <returns>Http status code</returns>
        [HttpGet("start")]
        public IActionResult StartAll()
        {
            foreach (var sensor in _sensorService.GetSensors())
            {
                try
                {
                    sensor.IsRunning = true;
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.StackTrace);
                    return BadRequest();
                }
            }
            return Ok();
        }

        /// <summary>
        /// Change all sensor IsRunning properties to false
        /// </summary>
        /// <returns>Http status code</returns>
        [HttpGet("stop")]
        public IActionResult StopAll()
        {
            foreach (var sensor in _sensorService.GetSensors())
            {
                try
                {
                    sensor.IsRunning = false;
                    
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.StackTrace);
                    return BadRequest();
                }
            }
            return Ok();
        }

        //TODO: do poprawy / dokończenia
        /// <summary>
        /// Run all existing sensors for data generation
        /// </summary>
        [HttpGet("runall")]
        public void RunAll()
        {
            var cancellationToken = new CancellationTokenSource();
            var tasks = new List<Task>();
            foreach (var sensor in _sensorService.GetSensors())
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        while (true)
                        {
                            var sensorData = sensor.GenerateSingleValue();
                            await Console.Out.WriteLineAsync(SensorUtils.SensorDataToJson(sensorData));
                            await _publishEndpoint.Publish(sensorData, cancellationToken.Token);
                            Thread.Sleep(sensor.Interval);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync(ex.StackTrace);
                    }
                }, cancellationToken.Token));
            }

            var t = Task.WhenAll(tasks);
            try
            {
                t.Wait();
            }
            catch
            {
                // ignored
            }
        }
    }
}