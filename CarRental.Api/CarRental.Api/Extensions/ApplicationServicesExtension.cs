using CarRental.DAL;
using CarRental.DAL.Repositories;
using CarRental.DAL.Repositories.Realization;
using CarRental.Service.Helpers;
using CarRental.Service.Helpers.Realization;
using CarRental.Service.Identity;
using CarRental.Service.Identity.Services;
using CarRental.Service.Services;
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

            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddScoped<ICarHelper, CarHelper>();

            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<ITokenRepository, TokenRepositoryBase>();

            services.AddScoped<ICarRepository, CarRepositoryBase>();

            services.AddScoped<IRentalPointService, RentalPointService>();

            services.AddScoped<ICountryRepository, CountryRepositoryBase>();

            services.AddScoped<ICityRepository, CityRepositoryBase>();

            services.AddScoped<ILocationRepository, LocationRepositoryBase>();

            services.AddScoped<IRentalPointRepository, RentalPointRepositoryBase>();

            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IBookingHelper, BookingHelper>();

            services.AddScoped<IHubService, HubService>();
        }
    }
}
