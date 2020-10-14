using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using CarRental.Identity.Entities;
using CarRental.Identity.Models;

namespace CarRental.Identity
{
    public interface IAuthorizeService
    {
        ClaimsIdentity GetIdentity(string userName, string userRole);
    }
}
