using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GUI.Models
{
    public class FilterTableForm
    {
        public int sensorId { get; set; }
        public string sensorType { get; set; }
        [DisplayName("Start date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddTHH:mm:ss.sssZ}")]
        public DateTime startDatetime { get; set; }
        [DisplayName("End date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddTHH:mm:ss.sssZ}")]
        public DateTime endDatetime { get; set; }
        


    }
}