using System;
using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class FilterJsonForm
    {
        public int sensorId { get; set; }
        public string sensorType { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddTHH:mm:ss.sssZ}")]
        public DateTime startDatetime { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddTHH:mm:ss.sssZ}")]
        public DateTime endDatetime { get; set; }
        


    }
}