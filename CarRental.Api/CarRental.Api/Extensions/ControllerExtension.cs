using CarRental.Api.Validators.Authorize;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.Extensions
{
    public static class ControllerExtension
    {
        public static void AddControllerService(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>();

                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }
    }
}
