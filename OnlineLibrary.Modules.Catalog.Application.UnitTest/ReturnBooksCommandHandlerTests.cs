using Moq;
using OnlineLibary.Modules.Catalog.Application.Rentals.ReturnBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ReturnBooksCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var readerId = Guid.NewGuid();
            var rentalId = Guid.NewGuid();
            var readerUsername = "username";
            List<Guid> rentalBookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<Guid> bookIds= new List<Guid> { Guid.NewGuid(),Guid.NewGuid() };
            List<Guid> libraryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            Guid ownerId = Guid.NewGuid();
            Reader reader = new Reader
            {
                ReaderId = readerId,
                UserName = readerUsername
            };
            var rental = new Rental
            {
                RentalId = rentalId,
                ReaderId = readerId,
                Returned = false
            };
            var rentalBooks = new List<RentalBook>
            {
                new RentalBook
                {
                    RentalBookId = rentalBookIds[0],
                    RentalId = rentalId,
                    BookId = bookIds[0]
                },
                new RentalBook
                {
                    RentalBookId = rentalBookIds[1],
                    RentalId=rentalId,
                    BookId = bookIds[1]
                }
            };
            var books = new List<Book>
            {
                new Book {
                    BookId = bookIds[0],
                    Title = "title",
                    Description = "description",
                    Author = "author",
                    Pages = 3,
                    Genres = Genres.Romance,
                    PubblicationYear = 2023,
                    NumberOfCopies = 2,
                    LibraryId = libraryIds[0]
                    },
                new Book {
                    BookId = bookIds[1],
                    Title = "title",
                    Description = "description",
                    Author = "author",
                    Pages = 5,
                    Genres = Genres.Science,
                    PubblicationYear = 2022,
                    NumberOfCopies = 3,
                    LibraryId = libraryIds[1]
                }
            };
            var libraries = new List<Library>
            {
                new Library
                {
                    LibraryId = libraryIds[0],
                    LibraryName="name",
                    IsActive = true,
                    OwnerId = ownerId
                },
                new Library
                {
                    LibraryId = libraryIds[1],
                    LibraryName="name",
                    IsActive = true,
                    OwnerId = ownerId
                }
            };

            var rentalRepository = new Mock<IRentalRepository>();
            rentalRepository.Setup(r => r.GeyByUserIdAsync(readerId)).ReturnsAsync(rental);

            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(r => r.GetRentalBooks(rentalId)).ReturnsAsync(rentalBooks);

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(r => r.GetByUsername(readerUsername)).ReturnsAsync(reader);
           
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(r => r.GetByIdAsync(bookIds[0])).ReturnsAsync(books[0]);
            bookRepository.Setup(r => r.GetByIdAsync(bookIds[1])).ReturnsAsync(books[1]);

            var handler = new ReturnBooksCommandHandler(rentalRepository.Object, rentalBookRepository.Object, bookRepository.Object,readerRepository.Object);
            var command = new ReturnBooksCommand(readerUsername);

            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            var returnDto = (ReturnDto)result.Data;
            Assert.That(returnDto.RentalId, Is.EqualTo(rentalId));
            Assert.That(returnDto.ReaderId, Is.EqualTo(readerId));
            
        }

        [Test]
        public async Task Handle_AlreadyReturnedBooks_ReturnsFailureResult()
        {
            //Arrange
            //Arrange
            var readerId = Guid.NewGuid();
            var rentalId = Guid.NewGuid();
            var readerUsername = "username";
            List<Guid> rentalBookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<Guid> bookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<Guid> libraryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            Guid ownerId = Guid.NewGuid();
            Reader reader = new Reader
            {
                ReaderId = readerId,
                UserName = readerUsername
            };
            var rental = new Rental
            {
                RentalId = rentalId,
                ReaderId = readerId,
                Returned = true
            };
            var rentalBooks = new List<RentalBook>
            {
                new RentalBook
                {
                    RentalBookId = rentalBookIds[0],
                    RentalId = rentalId,
                    BookId = bookIds[0]
                },
                new RentalBook
                {
                    RentalBookId = rentalBookIds[1],
                    RentalId=rentalId,
                    BookId = bookIds[1]
                }
            };
            var books = new List<Book>
            {
                new Book {
                    BookId = bookIds[0],
                    Title = "title",
                    Description = "description",
                    Author = "author",
                    Pages = 3,
                    Genres = Genres.Romance,
                    PubblicationYear = 2023,
                    NumberOfCopies = 2,
                    LibraryId = libraryIds[0]
                    },
                new Book {
                    BookId = bookIds[1],
                    Title = "title",
                    Description = "description",
                    Author = "author",
                    Pages = 5,
                    Genres = Genres.Science,
                    PubblicationYear = 2022,
                    NumberOfCopies = 3,
                    LibraryId = libraryIds[1]
                }
            };
            var libraries = new List<Library>
            {
                new Library
                {
                    LibraryId = libraryIds[0],
                    LibraryName="name",
                    IsActive = true,
                    OwnerId = ownerId
                },
                new Library
                {
                    LibraryId = libraryIds[1],
                    LibraryName="name",
                    IsActive = true,
                    OwnerId = ownerId
                }
            };

            var rentalRepository = new Mock<IRentalRepository>();
            rentalRepository.Setup(r => r.GeyByUserIdAsync(readerId)).ReturnsAsync(rental);

            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(r => r.GetRentalBooks(rentalId)).ReturnsAsync(rentalBooks);

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(r => r.GetByUsername(readerUsername)).ReturnsAsync(reader);

            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(r => r.GetByIdAsync(bookIds[0])).ReturnsAsync(books[0]);
            bookRepository.Setup(r => r.GetByIdAsync(bookIds[1])).ReturnsAsync(books[1]);

            var handler = new ReturnBooksCommandHandler(rentalRepository.Object, rentalBookRepository.Object, bookRepository.Object, readerRepository.Object);
            var command = new ReturnBooksCommand(readerUsername);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);

        }
    }
}
