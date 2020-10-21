using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.Models
{
    public class TokenPair
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
