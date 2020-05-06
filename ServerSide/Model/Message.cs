using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServerSide.Model
{
    public class Message
    {
        public uint Id { get; set; }

        [Required]
        public uint PersonId { get; set; }

        [Required]
        public string MessageText { get; set; }

        public DateTime TimeSent { get; set; } = DateTime.Now;
    }
}
