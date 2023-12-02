using Moq;
using OnlineLibary.Modules.Catalog.Application.Books.ChangeBook;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ChangeBookCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var username = "username";

            // Act
            var changeBookCommand = new ChangeBookCommand(bookId, "New Title", "New Description", "New Author", 300, 2022, 4.5, 10, username);

            // Assert
            Assert.That(changeBookCommand.BookId, Is.EqualTo(bookId));
            Assert.That(changeBookCommand.Title, Is.EqualTo("New Title"));
            Assert.That(changeBookCommand.Description, Is.EqualTo("New Description"));
            Assert.That(changeBookCommand.Author, Is.EqualTo("New Author"));
            Assert.That(changeBookCommand.Pages, Is.EqualTo(300));
            Assert.That(changeBookCommand.PubblicationYear, Is.EqualTo(2022));
            Assert.That(changeBookCommand.UserRating, Is.EqualTo(4.5));
            Assert.That(changeBookCommand.NumberOfCopies, Is.EqualTo(10));
            Assert.That(changeBookCommand.Username, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>(); 
            var bookId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();   
            var username = "username";
            var changeBookCommand = new ChangeBookCommand(
                bookId,
                "New Title",
                "New Description",
                "New Author",
                23,
                2023,
                3,
                333,
                username);
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var book = new Book("Old Title", "Old Description", "Old Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(new Library("LibraryName", true, ownerId));
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);

            var handler = new ChangeBookCommandHandler(bookRepository.Object, libraryRepository.Object,ownerRepository.Object);

            //Act
            var result = await handler.Handle(changeBookCommand, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_InvalidBook_ReturnsFailureResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var bookId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var username = "username";
            var changeBookCommand = new ChangeBookCommand(
                bookId,
                "New Title",
                "New Description",
                "New Author",
                23,
                2023,
                3,
                333,
                username);

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var book = new Book("Old Title", "Old Description", "Old Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).Returns(Task.FromResult<Book>(null));
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(new Library("LibraryName", true, ownerId));
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);

            var handler = new ChangeBookCommandHandler(bookRepository.Object, libraryRepository.Object, ownerRepository.Object);

           
            //Act
            var result = await handler.Handle(changeBookCommand, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("This book does not exist."));

        }

        [Test]
        public async Task Handle_NotOwnerOfLibrary_ReturnsFailureResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var bookId = Guid.NewGuid();
            var ownerIdOfLibrary = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var username = "username";
            var changeBookCommand = new ChangeBookCommand(
                bookId,
                "New Title",
                "New Description",
                "New Author",
                23,
                2023,
                3,
                333,
                username);
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var book = new Book("Old Title", "Old Description", "Old Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(new Library("LibraryName", true, ownerIdOfLibrary));
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);
            var handler = new ChangeBookCommandHandler(bookRepository.Object, libraryRepository.Object, ownerRepository.Object);


            //Act
            var result = await handler.Handle(changeBookCommand, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("Only owner of library can change the book."));

        }


        [Test]  
        public async Task Handle_LibraryDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var bookId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var username = "username";
            var changeBookCommand = new ChangeBookCommand(
                bookId,
                "New Title",
                "New Description",
                "New Author",
                23,
                2023,
                3,
                333,
                username);
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var book = new Book("Old Title", "Old Description", "Old Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).Returns(Task.FromResult<Library>(null));
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);

            var handler = new ChangeBookCommandHandler(bookRepository.Object, libraryRepository.Object, ownerRepository.Object);

            //Act
            var result = await handler.Handle(changeBookCommand, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("Library does not exist."));
        }


    }
}
