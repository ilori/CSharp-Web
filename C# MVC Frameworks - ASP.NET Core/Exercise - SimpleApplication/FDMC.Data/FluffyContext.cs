namespace FDMC.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FluffyContext : DbContext
    {
        public FluffyContext()
        {
        }

        public FluffyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cat>(x => x.HasKey(y => y.Id));
            builder.Entity<Cat>(x => x.Property(y => y.Breed).IsRequired());
            builder.Entity<Cat>(x => x.Property(y => y.Name).IsRequired());
            builder.Entity<Cat>(x => x.Property(y => y.ImageUrl).IsRequired());
        }
    }
}