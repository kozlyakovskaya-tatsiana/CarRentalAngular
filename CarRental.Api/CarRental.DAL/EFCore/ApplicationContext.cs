﻿using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using CarRental.DAL.EntityConfigurations;

namespace CarRental.DAL.EFCore
{
    public class ApplicationContext : ApplicationIdentityContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<ImageFile> ImageFiles { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CarConfiguration());

        }
    }
}
