using System;
using Model;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    [BsonIgnoreExtraElements]
    public class SensorData
    {
        public SensorData(Model.SensorData dto)
        {
            SensorId = dto.SensorId;
            SensorType = dto.SensorType;
            Unit = dto.Unit;
            DateTime = dto.DateTime;
            Value = dto.Value;
        }

        public int SensorId { get; set; }

        public string SensorType { get; set; }
        public DataUnit Unit { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public Model.SensorData ToDto()
        {
            return new Model.SensorData()
                {DateTime = DateTime, SensorId = SensorId, SensorType = SensorType, Unit = Unit, Value = Value};
        }
    }
}