using System;
using System.Collections.Generic;
using System.Text;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfigurations
{
    public class RentalPointConfiguration : IEntityTypeConfiguration<RentalPoint>
    {
        public void Configure(EntityTypeBuilder<RentalPoint> builder)
        {
            builder
                .HasMany(point => point.Cars)
                .WithOne(car => car.RentalPoint)
                .HasForeignKey(car => car.RentalPointId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(point => point.Location)
                .WithOne(loc => loc.RentalPoint)
                .HasForeignKey<Location>(loc => loc.RentalPointId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
