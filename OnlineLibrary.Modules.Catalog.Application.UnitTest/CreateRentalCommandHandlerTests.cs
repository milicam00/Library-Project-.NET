using Moq;
using OnlineLibary.Modules.Catalog.Application.Rentals.CreateRental;
using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class CreateRentaLCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            // Arrange
            Guid userId = Guid.NewGuid();           
            Guid bookId1 = Guid.NewGuid();
            Guid bookId2 = Guid.NewGuid();
            List<Guid> bookIds = new List<Guid> { bookId1, bookId2 };
  

            // Act
            var command = new CreateRentalCommand(userId, bookIds);

            // Assert
            Assert.That(command.UserId,Is.EqualTo(userId));
            Assert.That(command.BookIds,Is.EquivalentTo(bookIds));  
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Guid> bookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<Guid> libraryIds = new List<Guid> { Guid.NewGuid(),Guid.NewGuid() };
            Guid ownerId = Guid.NewGuid();

            var reader = new Reader
            {
                ReaderId = userId,
                UserName ="username",
                Email = "mileticmilica246@gmail.com",
                FirstName ="Firstname",
                LastName ="Lastname"
               
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

           
            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(r => r.GetById(userId)).ReturnsAsync(reader);

            OutboxMessage message = new OutboxMessage("Email", "Data");
            var outboxRepository = new Mock<IOutboxMessageRepository>();
            outboxRepository.Setup(repo => repo.AddAsync(message));


            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(r => r.GetByIdsAsync(bookIds)).ReturnsAsync(books);

            var rentalRepository = new Mock<IRentalRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(r => r.GetByIdsAsync(libraryIds)).ReturnsAsync(libraries);

           
            

            var handler = new CreateRentalCommandHandler(rentalRepository.Object, bookRepository.Object, libraryRepository.Object, readerRepository.Object, outboxRepository.Object);
            var command = new CreateRentalCommand(userId, bookIds);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            
        }
    }
}
