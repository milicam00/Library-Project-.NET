using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Rentals
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(x => x.RentalId);

            
            builder.HasOne(r => r.Reader)
                .WithMany()
                .HasForeignKey(r => r.ReaderId);

        }
    }
}
