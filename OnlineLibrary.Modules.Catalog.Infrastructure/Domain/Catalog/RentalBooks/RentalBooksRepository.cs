using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;
using System.Reflection.Metadata.Ecma335;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.RentalBooks
{
    public class RentalBooksRepository : IRentalBooksRepository
    {
        private readonly CatalogContext _catalogContext;

        public RentalBooksRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task AddAsync(RentalBook rentalBook)
        {
            await _catalogContext.RentalBooks.AddAsync(rentalBook);
        }


        public async Task<RentalBook?> GetByIdAsync(Guid rentalBookId)
        {
            return await _catalogContext.RentalBooks.FirstOrDefaultAsync(x => x.RentalBookId == rentalBookId);
        }

        public async Task<RentalBook?> GetByRentalIdAsync(Guid rentalId)
        {
            return await _catalogContext.RentalBooks.Where(x=>x.RentalId == rentalId).FirstOrDefaultAsync();
        }

        public async Task<Owner?> GetOwner(Guid bookId)
        {
            return await _catalogContext.Books.Where(x => x.BookId == bookId).Select(x => x.Library.Owner).FirstOrDefaultAsync();
        }

        public async Task<Reader?> GetReader(Guid rentalId)
        {
            return await _catalogContext.Rentals.Where(x => x.RentalId == rentalId).Select(x => x.Reader).FirstOrDefaultAsync();
        }


        public async Task<List<RentalBook>> GetRentalBooks(Guid rentalId)
        {
            return await _catalogContext.RentalBooks.Where(x=>x.RentalId==rentalId).ToListAsync();
        }



        public void UpdateRentalBook(RentalBook rentalBook)
        {
            _catalogContext.RentalBooks.Update(rentalBook);
        }
    }
}
