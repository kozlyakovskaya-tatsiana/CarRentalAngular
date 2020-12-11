using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.Hubs.Models
{
    public class Message
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Group { get; set; }

        public string Text { get; set; }

        public DateTime SendDateTime { get; set; }
    }
}
