using System.Collections.Generic;

namespace CarRental.BLL
{
    public interface IValuesService
    {
        IEnumerable<string> Values { get; }

        void AddValue(string value);
    }
}
