using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using AutoMapper;
using CarRental.Api.Options;
using CarRental.Api.Validators.Authorize;
using CarRental.DAL;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.Service;
using CarRental.Service.Helpers;
using CarRental.Service.Identity;
using CarRental.Service.Identity.Options;
using CarRental.Service.Identity.Services;
using CarRental.Service.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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
            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>();

                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddNewtonsoftJson( options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opts =>
                 {
                     opts.Password.RequiredLength = 5;
                     opts.Password.RequireNonAlphanumeric = false;
                     opts.Password.RequireLowercase = false;
                     opts.Password.RequireUppercase = false;
                     opts.Password.RequireDigit = false;
                 })
                 .AddEntityFrameworkStores<ApplicationContext>()
                 .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.Configure<JwtOptions>(Configuration.GetSection(JwtOptions.SectionName));

            var jwtOptions = new JwtOptions();

            Configuration.GetSection(JwtOptions.SectionName).Bind(jwtOptions);

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

                // options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

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

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAuthorizeService, AuthorizeService>();

            services.AddScoped<IValuesService, ValuesService>();

            services.AddScoped<ICarService, CarService>();

            services.AddScoped(typeof(IRepository<>), typeof(EFGenericRepository<>));

            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddScoped<ICarHelper, CarHelper>();

            services.AddSingleton<DataStorage>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                    await db.Database.MigrateAsync();

                    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                    var rolesManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    await RoleInitializer.InitializeAsync(userManager, rolesManager);
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}

