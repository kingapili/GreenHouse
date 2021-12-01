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
                        // Generates values from 0 ppm to 2000 ppm by default
                        var sensor = new CarbonDioxideSensor
                        {
                            Id = i,
                            Name = "Carbon dioxide sensor #" + i,
                            Interval = (i + 1) * 50,
                            IsRunning = false,
                            MinValue = 0,
                            MaxValue = 2000
                        };
                        sensors.Add(sensor);
                        break;
                    }
                    case 2:
                    {
                        // Generates humidity values in 0% - 100% range by default
                        var sensor = new HumiditySensor
                        {
                            Id = i,
                            Name = "Humidity sensor #" + i,
                            Interval = (i + 1) * 100,
                            IsRunning = false,
                            MinValue = 0,
                            MaxValue = 100
                        };
                        sensors.Add(sensor);
                        break;
                    }
                    case 3:
                    {
                        // Generates values from 0.5 to 7.0 [W/m^2] by default
                        var sensor = new SolarRadiationSensor
                        {
                            Id = i,
                            Name = "Solar radiation sensor #" + i,
                            Interval = (i + 1) * 150,
                            IsRunning = false,
                            MinValue = 0.5,
                            MaxValue = 7.0
                        };
                        sensors.Add(sensor);
                        break;
                    }
                    case 4:
                    {
                        // Generates values from -20 to 50 degrees C by default
                        var sensor = new TemperatureSensor
                        {
                            Id = i,
                            Name = "Temperature sensor #" + i,
                            Interval = (i + 1) * 200,
                            IsRunning = false,
                            MinValue = -20,
                            MaxValue = 50
                        };
                        sensors.Add(sensor);
                        break;
                    }
                }
            }
            return sensors;
        }

        /// <summary>
        /// Convert SensorData type to JSON for easier logging
        /// </summary>
        /// <param name="data">SensorData object to be converted</param>
        /// <returns>Data from a sensor converted to a serialized JSON string</returns>
        public static string SensorDataToJson(SensorData data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static string FormatSensorName(string sensorName)
        {
            return sensorName.Remove(0, 6).Insert(0, "");
        }
    }
}