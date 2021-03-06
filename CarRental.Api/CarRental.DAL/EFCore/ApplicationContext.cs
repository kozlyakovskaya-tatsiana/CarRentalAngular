using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using CarRental.DAL.EntityConfigurations;

namespace CarRental.DAL.EFCore
{
    public class ApplicationContext : ApplicationIdentityContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {}

        public DbSet<Car> Cars { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<RentalPoint> RentalPoints { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<BookingInfo> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CarConfiguration());

            builder.ApplyConfiguration(new CityConfiguration());

            builder.ApplyConfiguration(new LocationConfiguration());

            builder.ApplyConfiguration(new RentalPointConfiguration());

            builder.ApplyConfiguration(new BookingConfiguration());
        }
    }
}
