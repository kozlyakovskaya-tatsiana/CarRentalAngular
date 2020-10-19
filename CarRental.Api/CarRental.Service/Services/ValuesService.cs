using System.Collections.Generic;
using CarRental.DAL;

namespace CarRental.Service.Services
{
    public class ValuesService : IValuesService
    {
        private readonly DataStorage _dataStorage;

        public IEnumerable<string> Values => _dataStorage.Values; 

        public ValuesService(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public void AddValue(string value)
        {
            _dataStorage.Values.Add(value);
        }
    }
}
