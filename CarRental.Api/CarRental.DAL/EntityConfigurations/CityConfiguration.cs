using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .HasOne(city => city.Country)
                .WithMany(cnt => cnt.Cities)
                .HasForeignKey(city => city.CountryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
