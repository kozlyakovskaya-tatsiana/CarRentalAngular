using CarRental.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Service.WebModels.Car
{
    public class CarFilterPagingRequest
    {
        public Guid? CountryId { get; set; }

        public Guid? CityId { get; set; }

        public IEnumerable<string> Marks { get; set; }

        public IEnumerable<TransmissionType> Transmissions { get; set; }

        public IEnumerable<CarcaseType> Carcases { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
