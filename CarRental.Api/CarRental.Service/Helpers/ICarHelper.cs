using System.Collections.Generic;

namespace CarRental.Service.Helpers
{
    public interface ICarHelper
    {
        IEnumerable<string> GetCarcasesTypes();

        IEnumerable<string> GetFuelTypes();

        IEnumerable<string> GetTransmissionTypes();

        IEnumerable<string> GetStatusTypes();
    }
}
