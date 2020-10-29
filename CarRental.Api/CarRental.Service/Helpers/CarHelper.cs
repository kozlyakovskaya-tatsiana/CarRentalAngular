using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRental.DAL.Enums;
using CarRental.Service.Services;

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
    }
}
