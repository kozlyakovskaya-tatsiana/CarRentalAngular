﻿namespace CarRental.Service.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string UserEmail { get; set; }

        public string UserRole { get; set; }
    }
}