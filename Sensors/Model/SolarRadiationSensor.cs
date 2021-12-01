using System;

namespace Model
{
    /// <summary>
    /// Class for solar insolation sensor. Measures solar radiation in W/m^2 per day.
    /// </summary>
    public class SolarRadiationSensor : ISensor
    {
        private const int Precision = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public bool IsRunning { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public SensorData GenerateSingleValue()
        {
            var rng = new Random();
            return new SensorData()
            {
                SensorId = Id,
                SensorType = GetType().ToString().Replace("Model.", ""),
                DateTime = DateTime.Now,
                Value = Math.Round(rng.NextDouble() * (MaxValue - MinValue) + MinValue, Precision),
                Unit = DataUnit.WattsPerMeterSquared
            };
        }
        
        public SensorData GenerateSingleValue(double value)
        {
            return new SensorData()
            {
                SensorId = Id,
                SensorType = GetType().ToString().Replace("Model.", ""),
                DateTime = DateTime.Now,
                Value = value,
                Unit = DataUnit.WattsPerMeterSquared
            };
        }
    }
}