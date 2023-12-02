using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.RentalBooks
{
    public class RentalBookConfiguration : IEntityTypeConfiguration<RentalBook>
    {
        public void Configure(EntityTypeBuilder<RentalBook> builder)
        {
            builder.HasKey(x => x.RentalBookId);

            builder.HasOne(rb => rb.Book)
                .WithMany(b => b.RentalBooks)
                .HasForeignKey(rb => rb.BookId);

            builder.HasOne(rb => rb.Rental)
               .WithMany(r => r.RentalBooks)
               .HasForeignKey(rb => rb.RentalId);
        }
    }
}
