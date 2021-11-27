using System;

namespace Model
{
    /// <summary>
    /// Class for temperature sensor. Measures air temperature in degrees Celsius.
    /// Generates values from -20 to 50 degrees C.
    /// </summary>
    public class TemperatureSensor : ISensor
    {
        private const int MinValue = -20;
        private const int MaxValue = 50;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public bool IsRunning { get; set; }

        public SensorData GenerateSingleValue()
        {
            var rng = new Random();
            return new SensorData()
            {
                SensorId = Id,
                DateTime = DateTime.Now,
                Value = rng.Next(MinValue, MaxValue),
                Unit = DataUnit.DegreesCelsius
            };
        }
    }
}