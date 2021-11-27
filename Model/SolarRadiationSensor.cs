using System;

namespace Model
{
    /// <summary>
    /// Class for solar insolation sensor. Measures solar radiation in W/m^2 per day.
    /// Generates values from 0.5 to 7.0 [W/m^2].
    /// </summary>
    public class SolarRadiationSensor : ISensor
    {
        private const double MinValue = 0.5;
        private const double MaxValue = 7.0;
        private const int Precision = 1;

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
                Value = Math.Round(rng.NextDouble() * (MaxValue - MinValue) + MinValue, Precision),
                Unit = DataUnit.WattsPerMeterSquared
            };
        }
    }
}