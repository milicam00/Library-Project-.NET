using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Books
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.BookId);

            builder.Property(b=> b.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a=>a.Author)
                .HasMaxLength (100)
                .IsRequired();

            builder.Property(b => b.Pages)
               .IsRequired()
               .HasDefaultValue(0);

            builder.HasOne(b => b.Library)
            .WithMany(l => l.Books)
            .HasForeignKey(b => b.LibraryId);

           
            

        }
    }

}
