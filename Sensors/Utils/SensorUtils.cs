using System;
using System.Collections.Generic;
using System.Text.Json;
using Model;

namespace Sensors.Utils
{
    public static class SensorUtils
    {
        /// <summary>
        /// Creates a List of sensors, generated randomly.
        /// </summary>
        /// <param name="numberOfSensors">Specifies how many sensors to be generated, default number is 30</param>
        /// <returns>List of created sensors.</returns>
        public static List<ISensor> CreateSensors(int numberOfSensors = 30)
        {
            var sensors = new List<ISensor>();

            for (var i = 0; i < numberOfSensors; i++)
            {
                var rng = new Random().Next(1, 5);
                switch (rng)
                {
                    case 1:
                    {
                        var sensor = new CarbonDioxideSensor()
                        {
                            Id = i,
                            Name = "Carbon dioxide sensor #" + i,
                            Interval = (i + 1) * 50,
                            IsRunning = false
                        };
                        sensors.Add(sensor);
                        break;
                    }
                    case 2:
                    {
                        var sensor = new HumiditySensor()
                        {
                            Id = i,
                            Name = "Humidity sensor #" + i,
                            Interval = (i + 1) * 100,
                            IsRunning = false
                        };
                        sensors.Add(sensor);
                        break;
                    }
                    case 3:
                    {
                        var sensor = new SolarRadiationSensor()
                        {
                            Id = i,
                            Name = "Solar radiation sensor #" + i,
                            Interval = (i + 1) * 150,
                            IsRunning = false
                        };
                        sensors.Add(sensor);
                        break;
                    }
                    case 4:
                    {
                        var sensor = new TemperatureSensor()
                        {
                            Id = i,
                            Name = "Temperature sensor #" + i,
                            Interval = (i + 1) * 200,
                            IsRunning = false
                        };
                        sensors.Add(sensor);
                        break;
                    }
                }
            }
            return sensors;
        }

        /// <summary>
        /// Convert SensorData type to JSON for easier messaging
        /// </summary>
        /// <param name="data">SensorData object to be converted</param>
        /// <returns>Data from a sensor converted to a serialized JSON string</returns>
        public static string SensorDataToJson(SensorData data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}