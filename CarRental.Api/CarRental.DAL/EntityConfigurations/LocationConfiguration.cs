using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfigurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
                .HasOne(loc => loc.City)
                .WithMany(c => c.Locations)
                .HasForeignKey(loc => loc.CityId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
