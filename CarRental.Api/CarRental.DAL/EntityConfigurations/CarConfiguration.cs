﻿using CarRental.DAL.Entities;
using CarRental.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRental.DAL.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .Property(car => car.Carcase)
                .HasConversion(new EnumToStringConverter<CarcaseType>());

            builder
                .Property(car => car.FuelType)
                .HasConversion(new EnumToStringConverter<FuelType>());

            builder
                .Property(car => car.Transmission)
                .HasConversion(new EnumToStringConverter<TransmissionType>());

            builder
                .HasMany(car => car.Documents)
                .WithOne(doc => doc.Car)
                .HasForeignKey(doc => doc.CarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
