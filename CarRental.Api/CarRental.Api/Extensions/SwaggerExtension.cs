using CarRental.Api.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CarRental.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerDocumentOptions = new SwaggerDocumentOptions();

            configuration.GetSection(SwaggerDocumentOptions.SectionName).Bind(swaggerDocumentOptions);

            var securityDefinitionOptions = new SecurityDefinitionOptions();

            configuration.GetSection(SecurityDefinitionOptions.SectionName).Bind(securityDefinitionOptions);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    swaggerDocumentOptions.DocumentName,
                    new OpenApiInfo
                    {
                        Title = swaggerDocumentOptions.Title,
                        Description = swaggerDocumentOptions.Description,
                        Version = swaggerDocumentOptions.Version
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition(securityDefinitionOptions.SecurityDefinitionName, new OpenApiSecurityScheme
                {
                    Description = securityDefinitionOptions.Description,
                    Name = securityDefinitionOptions.Name,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = securityDefinitionOptions.Scheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }
    }
}
