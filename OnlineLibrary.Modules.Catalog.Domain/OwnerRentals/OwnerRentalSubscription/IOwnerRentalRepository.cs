namespace OnlineLibrary.Modules.Catalog.Domain.OwnerRentals.OwnerRentalSubscription
{
    public interface IOwnerRentalRepository
    {
        Task AddAsync(OwnerRental ownerRental);
    }
}
