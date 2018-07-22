namespace Movies.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.CoverImageUrl)
                .IsRequired();

            builder.HasOne(x => x.Director)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.DirectorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Borrower)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.BorrowerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}