using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CarRental.Api.Options;
using CarRental.Api.Services;
using CarRental.BLL.Services;
using CarRental.DAL;
using CarRental.Identity.Options;
using CarRental.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CarRental.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("jwtoptions.json")
                .AddJsonFile("swaggeroptions.json")
                .AddConfiguration(configuration);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            var swaggerDocumentOptions = new SwaggerDocumentOptions();

            Configuration.GetSection(SwaggerDocumentOptions.SectionName).Bind(swaggerDocumentOptions);

            var securityDefinitionOptions = new SecurityDefinitionOptions();

            Configuration.GetSection(SecurityDefinitionOptions.SectionName).Bind(securityDefinitionOptions);


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerDocumentOptions.DocumentName,
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

            services.AddScoped<TokenService>();

            services.AddScoped<AccountService>();

            services.Configure<JwtOptions>(Configuration.GetSection(JwtOptions.SectionName));


            var jwtOptions = new JwtOptions();

            Configuration.GetSection(JwtOptions.SectionName).Bind(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,

                        ValidateAudience = true,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtOptions.Issuer,

                        ValidAudience = jwtOptions.Audience,

                        IssuerSigningKey = jwtOptions.SymmetricSecurityKey,

                        ClockSkew = TimeSpan.Zero
                    };
                });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

