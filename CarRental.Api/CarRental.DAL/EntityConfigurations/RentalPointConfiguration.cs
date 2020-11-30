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
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(point => point.Location)
                .WithOne(loc => loc.RentalPoint)
                .HasForeignKey<RentalPoint>(point => point.LocationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
