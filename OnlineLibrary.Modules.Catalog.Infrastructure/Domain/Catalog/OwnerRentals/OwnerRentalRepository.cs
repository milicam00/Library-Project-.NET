using OnlineLibrary.Modules.Catalog.Domain.OwnerRentals.OwnerRentalSubscription;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.OwnerRentals
{
    public class OwnerRentalRepository : IOwnerRentalRepository
    {
        private readonly CatalogContext _catalogContext;
        public OwnerRentalRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task AddAsync(OwnerRental ownerRental)
        {
            await _catalogContext.OwnerRentals.AddAsync(ownerRental);
        }
    }
}
