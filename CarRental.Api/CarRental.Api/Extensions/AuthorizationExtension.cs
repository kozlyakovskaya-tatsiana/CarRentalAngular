using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.Extensions
{
    public static class AuthorizationExtension
    {
        public static void AddAuthorizationService(this IServiceCollection services)
        {
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("ForAdminOnly", policy =>
                    policy.RequireRole("admin"));

                opts.AddPolicy("ForUserOnly", policy =>
                    policy.RequireRole("user"));

                opts.AddPolicy("ForUsersAdmins", policy =>
                    policy.RequireRole("admin", "user"));

                opts.AddPolicy("ForManagerOnly", policy =>
                    policy.RequireRole("manager"));

                opts.AddPolicy("ForManagersAdmins", policy =>
                    policy.RequireRole("manager", "admin"));
            });
        }
    }
}
