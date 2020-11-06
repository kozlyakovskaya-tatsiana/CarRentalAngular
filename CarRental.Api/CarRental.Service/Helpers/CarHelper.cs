using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.DAL.Enums;

namespace CarRental.Service.Helpers
{
    public class CarHelper : ICarHelper
    {
        public IEnumerable<string> GetCarcasesTypes()
        {
            var carcases = Enum.GetValues(typeof(CarcaseType)).Cast<CarcaseType>().Select(type => type.ToString());

            return carcases;
        }

        public IEnumerable<string> GetFuelTypes()
        {
            var fuelTypes = Enum.GetValues(typeof(FuelType)).Cast<FuelType>().Select(type => type.ToString());

            return fuelTypes;
        }

        public IEnumerable<string> GetTransmissionTypes()
        {
            var transmissionTypes = Enum.GetValues(typeof(TransmissionType)).Cast<TransmissionType>().Select(type => type.ToString());

            return transmissionTypes;
        }

        public IEnumerable<string> GetStatusTypes()
        {
            var statusTypes = Enum.GetValues(typeof(Status)).Cast<Status>().Select(status => status.ToString());

            return statusTypes;
        }
    }
}
