using CarRental.Api.Security;
using CarRental.DAL.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.Extensions
{
    public static class AuthorizationExtension
    {
        public static void AddAuthorizationService(this IServiceCollection services)
        {
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(Policy.ForAdminOnly, policy =>
                    policy.RequireRole(Role.Admin.GetRoleName()));

                opts.AddPolicy(Policy.ForUserOnly, policy =>
                    policy.RequireRole(Role.User.GetRoleName()));

                opts.AddPolicy(Policy.ForUsersAdmins, policy =>
                    policy.RequireRole(Role.User.GetRoleName(), Role.Admin.GetRoleName()));

                opts.AddPolicy(Policy.ForManagerOnly, policy =>
                    policy.RequireRole(Role.Manager.GetRoleName()));

                opts.AddPolicy(Policy.ForManagersAdmins, policy =>
                    policy.RequireRole(Role.Manager.GetRoleName(), Role.Admin.GetRoleName()));
            });
        }
    }
}
