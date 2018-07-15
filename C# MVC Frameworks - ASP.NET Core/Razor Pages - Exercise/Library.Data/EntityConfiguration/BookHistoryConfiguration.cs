namespace Library.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BookHistoryConfiguration : IEntityTypeConfiguration<BookHistory>
    {
        public void Configure(EntityTypeBuilder<BookHistory> builder)
        {
            builder.HasKey(x => x.Id);

            //TODO Fix Cascade

            builder.HasOne(x => x.Book)
                .WithMany(x => x.BookHistories)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}