using System;
using Model;

namespace GUI.Models
{
    public class SensorData
    {
        public int sensorId { get; set; }
        public string sensorType { get; set; }
        public DataUnit unit { get; set; }
        public DateTime dateTime { get; set; }
        public double value { get; set; }
    }
}