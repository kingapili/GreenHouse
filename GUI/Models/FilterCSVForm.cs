using System;
using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class FilterCSVForm
    {
        public int sensorId { get; set; }
        public string sensorType { get; set; }
        [DataType(DataType.DateTime), Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddTHH:mm:ss.sssZ}")]
        public DateTime dateTime { get; set; }
        


    }
}