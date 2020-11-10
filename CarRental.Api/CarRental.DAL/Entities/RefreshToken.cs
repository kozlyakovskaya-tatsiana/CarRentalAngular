using System;

namespace CarRental.DAL.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid Id { get; set; }

        public string RefreshTokenValue { get; set; }
    }
}
