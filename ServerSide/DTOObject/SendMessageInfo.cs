using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.DTOObject
{
    public class SendMessageInfo
    {
        public int SenderID { get; set; }
        public int RecipientID { get; set; }
        public string MessageText { get; set; }
    }
}
