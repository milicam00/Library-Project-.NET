using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.ReturnBooks
{
    public class ReturnBooksCommandHandler : ICommandHandler<ReturnBooksCommand, Result>
    {

        private readonly IRentalRepository _rentalRepository;
        private readonly IRentalBooksRepository _rentalBooksRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        public ReturnBooksCommandHandler(IRentalRepository rentalRepository, IRentalBooksRepository rentalBooksRepository, IBookRepository bookRepository,IReaderRepository readerRepository)
        {
            _rentalRepository = rentalRepository;
            _rentalBooksRepository = rentalBooksRepository;
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }
        public async Task<Result> Handle(ReturnBooksCommand request, CancellationToken cancellationToken)
        {
            Reader reader =  await _readerRepository.GetByUsername(request.ReaderUsername);
            if(reader == null)
            {
                return Result.Failure("This reader does not exist.");
            }
            Rental rental = await _rentalRepository.GeyByUserIdAsync(reader.ReaderId);

            if (rental == null)
            {
                return Result.Failure("This rental does not exist.");
            }

            if (rental.Returned == true)
            {
                return Result.Failure("This reader has returned all books.");
            }

            List<RentalBook> rentalBooks = await _rentalBooksRepository.GetRentalBooks(rental.RentalId);

            foreach (var rentalBook in rentalBooks)
            {
                Book book = await _bookRepository.GetByIdAsync(rentalBook.BookId);
                book.NumberOfCopies++;
                _bookRepository.UpdateBook(book);
            }

            rental.ReturnBooks();
            _rentalRepository.Update(rental);

            var returnDto = new ReturnDto
            {
                RentalId = rental.RentalId,
                ReaderId = rental.ReaderId,
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate
            };

            return Result.Success(returnDto);
        }
    }
}
