using CarRental.DAL;
using CarRental.DAL.Repositories;
using CarRental.DAL.Repositories.Realization;
using CarRental.Service.Helpers;
using CarRental.Service.Identity;
using CarRental.Service.Identity.Services;
using CarRental.Service.Services;
using CarRental.Service.Services.Models;
using CarRental.Service.Services.Realization;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAuthorizeService, AuthorizeService>();

            services.AddScoped<ICarService, CarService>();

            services.AddScoped(typeof(IRepository<>), typeof(EfGenericRepository<>));

            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddScoped<ICarHelper, CarHelper>();

            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddScoped<ICarRepository, CarRepository>();

            services.AddScoped<IRentalPointService, RentalPointService>();

            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IRentalPointRepository, RentalPointRepository>();
        }
    }
}
