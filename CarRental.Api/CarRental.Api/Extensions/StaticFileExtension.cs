using CarRental.Service.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.Extensions
{
    public static class StaticFileExtension
    {
        public static void ConfigureImagesStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StaticFilesOptions>(configuration.GetSection(nameof(StaticFilesOptions)));
        }
    }
}
