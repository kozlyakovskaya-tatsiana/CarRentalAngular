using System;
using AutoMapper;
using CarRental.Api.Extensions;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.Service.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddControllerService();

            services.AddDataAccessServices(Configuration.GetConnectionString("DefaultConnection"));

            services.ConfigureAuthentication(Configuration);

            services.AddAuthorizationService();

            services.AddSwagger(Configuration);

            services.AddApplicationServices();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();

            services.ConfigureImagesStore(Configuration);

            services.AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
            });
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

            const string cacheMaxAge = "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append(
                        "Cache-Control", $"public, max-age={cacheMaxAge}");
                }
            });

            app.UseCustomSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(Configuration.GetSection("AngularOriginDomain").Value)
                    .AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<CarBookingHub>("/carstatus");

                endpoints.MapHub<ChatHub>("/chat");
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