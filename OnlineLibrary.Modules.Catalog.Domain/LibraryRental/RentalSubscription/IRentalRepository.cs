using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription
{
    public interface IRentalRepository
    {
        Task AddAsync(Rental rental);
        Task<Rental> GetByIdAsync(Guid rentalId);
        Task<Rental> GeyByUserIdAsync(Guid userId);
        Task<List<Rental>> GetOverdueRentals(DateTime overdueDate);
        void Update(Rental rental);
      
        
    }
}
