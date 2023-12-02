using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentals.OwnerRentalSubscription;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.OwnerRentals
{
    public class OwnerRentalConfiguration : IEntityTypeConfiguration<OwnerRental>
    {
        public void Configure(EntityTypeBuilder<OwnerRental> builder)
        {
            builder.HasKey(x => x.OwnerRentalId);

            builder.HasOne(o => o.Owner)
                .WithMany()
                .HasForeignKey(O => O.OwnerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
