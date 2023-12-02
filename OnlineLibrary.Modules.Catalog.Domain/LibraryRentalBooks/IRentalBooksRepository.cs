using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks
{
    public interface IRentalBooksRepository
    {
        Task AddAsync(RentalBook rentalBook);
        
        Task<RentalBook?> GetByIdAsync(Guid rentalBookId);

        Task<RentalBook?> GetByRentalIdAsync(Guid rentalId);

        Task<List<RentalBook>> GetRentalBooks(Guid rentalId);
        void UpdateRentalBook(RentalBook rentalBook);
        Task<Reader> GetReader(Guid rentalId);
        Task<Owner> GetOwner(Guid bookId);
    }
}
