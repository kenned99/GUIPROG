using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServerSide.Model
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public GpsLocation LastKnownLocation { get; set; }
        public int? LastKnownLocationId { get; set; }
        public Byte[] salt { get; set; }
    }
}
