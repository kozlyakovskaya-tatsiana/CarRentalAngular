using System;

namespace CarRental.Service.Hubs.Models
{
    public class Message
    {
        public string From { get; set; }

        public string Group { get; set; }

        public string Text { get; set; }

        public DateTime SendDateTime { get; set; }
    }
}
