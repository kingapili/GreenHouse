using System;

namespace GUI.Model
{
    /// <summary>
    /// Class for data read from the sensors. Contains date and time of the reading and its value.
    /// </summary>
    public class SensorData
    {
        public int sensorId { get; set; }
        
        public string sensorType { get; set; }
        
        public DataUnit unit { get; set; }
        
        public DateTime dateTime { get; set; }
        
        public double value { get; set; }
    }
}