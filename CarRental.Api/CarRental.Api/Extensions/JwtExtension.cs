using CarRental.Service.Identity.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CarRental.Api.Extensions
{
    public static class JwtExtension
    {
        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            var jwtOptions = new JwtOptions();

            configuration.GetSection(JwtOptions.SectionName).Bind(jwtOptions);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        ValidateAudience = false,

                        ValidIssuer = jwtOptions.Issuer,

                        IssuerSigningKey = jwtOptions.SymmetricSecurityKey,

                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
