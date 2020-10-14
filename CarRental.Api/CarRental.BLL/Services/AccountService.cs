using CarRental.DAL;
using CarRental.DAL.Entities;
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

        public IEnumerable<User> Users => _dataStorage.Users;

        public AccountService(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
    }
}
