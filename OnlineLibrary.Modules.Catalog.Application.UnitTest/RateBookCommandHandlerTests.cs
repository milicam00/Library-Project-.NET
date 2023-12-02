using Moq;
using OnlineLibary.Modules.Catalog.Application.Rentals.RateBook;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class RateBookCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            Guid rentalBookId = Guid.NewGuid();
            int rate = 3;
            string text = "comment";

            //Act
            var command = new RateBookCommand(rentalBookId, rate, text);

            //Assert
            Assert.That(command.RentalBookId, Is.EqualTo(rentalBookId));
            Assert.That(command.Rate, Is.EqualTo(rate));
            Assert.That(command.Text, Is.EqualTo(text));
        }

        [Test]
        public async Task Handle_ValidRequest_RatesBookAndReturnsSuccessResult()
        {
            //Arrange
            var rentalId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var command = new RateBookCommand(rentalId, 4, "Good book!");
            var rental = new RentalBook { RentalBookId = rentalId, BookId = bookId };
            var book = new Book { BookId = bookId, Title = "Test Book", UserRating = 3.5, NumberOfRatings = 10 };

            var rentalRepository = new Mock<IRentalBooksRepository>();
            rentalRepository.Setup(r => r.GetByIdAsync(rentalId)).ReturnsAsync(rental);

            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);

            var handler = new RateBookCommandHandler(rentalRepository.Object, bookRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_RentalDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var rentalId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var command = new RateBookCommand(rentalId, 4, "Good book!");
            var rental = new RentalBook { RentalBookId = rentalId, BookId = bookId };
            var book = new Book { BookId = bookId, Title = "Test Book", UserRating = 3.5, NumberOfRatings = 10 };

            var rentalRepository = new Mock<IRentalBooksRepository>();
            rentalRepository.Setup(r => r.GetByIdAsync(rentalId)).Returns(Task.FromResult<RentalBook>(null));
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);

            var handler = new RateBookCommandHandler(rentalRepository.Object, bookRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This rental does not exist"));
        }


    }
}
