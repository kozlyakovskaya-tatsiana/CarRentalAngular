using System.Collections.Generic;

namespace CarRental.Service
{
    public interface IValuesService
    {
        IEnumerable<string> Values { get; }

        void AddValue(string value);
    }
}
