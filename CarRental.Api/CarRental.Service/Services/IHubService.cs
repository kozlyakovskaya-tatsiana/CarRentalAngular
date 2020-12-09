using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Service.Services
{
    public interface IHubService
    {
        Task ChangeCarStatus(Guid? carId, string newCarStatus);
    }
}
