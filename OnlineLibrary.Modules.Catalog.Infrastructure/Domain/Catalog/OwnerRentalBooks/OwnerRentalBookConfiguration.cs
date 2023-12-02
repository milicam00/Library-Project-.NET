using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentalBooks.OwnerRentalBookSubscription;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.OwnerRentalBooks
{
    public class OwnerRentalBookConfiguration : IEntityTypeConfiguration<OwnerRentalBook>
    {
        public void Configure(EntityTypeBuilder<OwnerRentalBook> builder)
        {
            builder.HasKey(x => x.OwnerRentalBookId);

            builder.HasOne(rb => rb.Book)
                .WithMany(b => b.OwnerRentalBooks)
                .HasForeignKey(rb => rb.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(rb => rb.OwnerRental)
                .WithMany(r => r.OwnerRentalBooks)
                .HasForeignKey(rb => rb.OwnerRentalId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
