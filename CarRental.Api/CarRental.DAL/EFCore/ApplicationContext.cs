using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CarRental.DAL.EntityConfigurations;
using CarRental.DAL.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRental.DAL.EFCore
{
    public class ApplicationContext : ApplicationIdentityContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CarConfiguration());

        }
    }
}
