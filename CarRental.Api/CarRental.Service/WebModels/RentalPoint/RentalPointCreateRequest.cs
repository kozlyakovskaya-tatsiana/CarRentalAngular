namespace CarRental.Service.WebModels.RentalPoint
{
    public class RentalPointCreateRequest
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
        
        public string Address { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}
