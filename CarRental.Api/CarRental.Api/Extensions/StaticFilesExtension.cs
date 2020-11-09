﻿using System.Linq;
using CarRental.Api.Options;
using CarRental.Service.Identity.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.Extensions
{
    public static class StaticFilesExtension
    {
        public static void ConfigureImagesStore(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.Configure<StaticFilesOptions>(opt =>
                opt.ImagesStore = environment.WebRootPath + configuration.GetSection("ImagesStore").Value);
        }
    }
}
