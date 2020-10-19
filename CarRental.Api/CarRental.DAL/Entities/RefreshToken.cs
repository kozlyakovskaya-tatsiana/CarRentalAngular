namespace CarRental.DAL.Entities
{
    public class RefreshToken : IEntity
    {
        public int Id { get; set; }

        public string RefreshTokenValue { get; set; }
    }
}
