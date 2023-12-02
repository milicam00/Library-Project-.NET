using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.RateBook
{
    public class RateBookCommandHandler : ICommandHandler<RateBookCommand, Result>
    {
        public readonly IRentalBooksRepository _rentalRepository;
        public readonly IBookRepository _bookRepository;
        public RateBookCommandHandler(IRentalBooksRepository rentalRepository, IBookRepository bookRepository)
        {
            _rentalRepository = rentalRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Result> Handle(RateBookCommand command, CancellationToken cancellationToken)
        {

            RentalBook? rental = await _rentalRepository.GetByIdAsync(command.RentalBookId);
            if (rental != null)
            {
                rental.RateBook(command.Rate, command.Text);
                Book book = await _bookRepository.GetByIdAsync(rental.BookId);
                book.NumberOfRatings++;
                double newAverageRating = ((book.UserRating * (book.NumberOfRatings - 1)) + command.Rate) / (book.NumberOfRatings);
                book.UserRating = newAverageRating;
                _bookRepository.UpdateBook(book);

                var retedBookDto = new RatedBookDto
                {
                    BookTitle = book.Title,
                    Rate = command.Rate,
                    Text = command.Text
                };

                return Result.Success(retedBookDto);
            }


            return Result.Failure("This rental does not exist");

        }
    }

}
