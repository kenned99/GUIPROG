using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServerSide.Model
{
    public class GpsLocation
    {
        public int Id { get; set; }

        [Required]
        public float Longtitude { get; set; }

        [Required]
        public float Latitude { get; set; } 

        public decimal Accuracy { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;
    }
}
