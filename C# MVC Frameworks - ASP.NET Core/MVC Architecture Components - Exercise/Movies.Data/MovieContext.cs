using Movies.Models;

namespace Movies.Data
{
    using EntityConfiguration;
    using Microsoft.EntityFrameworkCore;

    public class MovieContext : DbContext
    {
        public MovieContext()
        {
        }

        public MovieContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new DirectorConfiguration());
            modelBuilder.ApplyConfiguration(new BorrowerConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryConfiguration());
        }
    }
}