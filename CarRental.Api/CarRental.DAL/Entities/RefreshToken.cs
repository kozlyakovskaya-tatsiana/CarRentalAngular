using System;

namespace CarRental.DAL.Entities
{
    public class RefreshToken : IEntity
    {
        public Guid Id { get; set; }

        public string RefreshTokenValue { get; set; }
    }
}
