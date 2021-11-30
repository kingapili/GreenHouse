using System;

namespace Model
{
    /// <summary>
    /// Class for data read from the sensors. Contains date and time of the reading and its value.
    /// </summary>
    public class SensorData
    {
        public int SensorId { get; set; }
        
        public string SensorType { get; set; }
        public DataUnit Unit { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}