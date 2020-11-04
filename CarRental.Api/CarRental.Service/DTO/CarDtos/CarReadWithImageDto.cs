namespace CarRental.Service.DTO.CarDtos
{
    public class CarReadWithImageDto: CarDtoBase
    {
        public string PathToMainImage { get; set; }

        public string ImageDataUrl { get; set; }
    }
}
