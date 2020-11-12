using System;

namespace CarRental.DAL.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string RefreshTokenValue { get; set; }
    }
}
