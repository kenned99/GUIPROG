using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServerSide.Model
{
    public class GpsLocation
    {
        public uint Id { get; set; }

        [Required]
        public decimal Longtitude { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Accuracy { get; set; }

        public int Altitude { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;
    }
}
