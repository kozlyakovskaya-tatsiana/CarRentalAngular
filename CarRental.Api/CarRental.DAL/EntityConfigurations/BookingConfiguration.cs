using System;
using System.Collections.Generic;
using System.Text;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfigurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<BookingInfo>
    {
        void IEntityTypeConfiguration<BookingInfo>.Configure(EntityTypeBuilder<BookingInfo> builder)
        {
            builder
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(b => b.Sum)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
