using CarRental.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CarRental.BLL.Services
{
    public class AccountService
    {
        private readonly DataStorage _dataStorage;

        public AccountService()
        {
            _dataStorage = DataStorage.GetDataStorage();
        }

    }
}
