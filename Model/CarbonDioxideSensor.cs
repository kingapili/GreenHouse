using System;

namespace Model
{
    /// <summary>
    /// Class for carbon dioxide sensor. Measures carbon dioxide level in PPM (parts per million).
    /// Generates values from 0 ppm to 2000 ppm.
    /// </summary>
    public class CarbonDioxideSensor : ISensor
    {
        private const int MinValue = 0;
        private const int MaxValue = 2000;

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
                Unit = DataUnit.Ppm
            };
        }
    }
}