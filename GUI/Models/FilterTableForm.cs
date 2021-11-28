using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GUI.Models
{
    public class FilterTableForm
    {
        public int sensorId { get; set; }
        public string sensorType { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddTHH:mm:ss.sssZ}")]
        public DateTime dateTime { get; set; }
        


    }
}