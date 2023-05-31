using Microsoft.EntityFrameworkCore;

namespace MM.MinimalAPI.Delivery.Context
{
    public class DeliveryContext : DbContext
    {
        public DeliveryContext(DbContextOptions<DeliveryContext> options) : base(options) { }

        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Package> Packages => Set<Package>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasOne(p => p.Package)
                .WithMany(b => b.Locations)
                .HasPrincipalKey(p => p.PackageId)
                .IsRequired();
        }
    }
}
