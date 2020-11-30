namespace CarRental.Service.DTO.UserDtos
{
    public class UserForRead : UserBase
    {
        public string Role { get; set; }

        public string Email { get; set; }
    }
}
