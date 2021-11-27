using System;

namespace Model
{
    /// <summary>
    /// Class for air humidity sensor.
    /// Generates humidity values in 0% - 100% range. 
    /// </summary>
    public class HumiditySensor : ISensor
    {
        private const int MinValue = 0;
        private const int MaxValue = 100;
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
                SensorType = GetType().ToString(),
                DateTime = DateTime.Now,
                Value = rng.Next(MinValue, MaxValue),
                Unit = DataUnit.Percent
            };
        }
    }
}