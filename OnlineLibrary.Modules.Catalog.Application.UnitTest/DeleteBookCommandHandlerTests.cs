using Moq;
using OnlineLibary.Modules.Catalog.Application.Books.DeleteBook;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class DeleteBookCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var bookId = Guid.NewGuid();
            var username = "username";

            //Act
            var deleteBookCommand = new DeleteBookCommand(username, bookId);

            //Assert
            Assert.That(deleteBookCommand.BookId, Is.EqualTo(bookId));
            Assert.That(deleteBookCommand.OwnerUsername, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var ownerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var username = "username";
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var deleteBookCommand = new DeleteBookCommand(username, bookId);
            var book = new Book("Title", "Description", "Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            var library = new Library("LibraryName", true, ownerId);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);
            var handler = new DeleteBookCommandHandler(bookRepository.Object, libraryRepository.Object,ownerRepository.Object);


            //Act
            var result = await handler.Handle(deleteBookCommand, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_BookDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var ownerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var username = "username";
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var deleteBookCommand = new DeleteBookCommand(username, bookId);
            var book = new Book("Title", "Description", "Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            var library = new Library("LibraryName", true, ownerId);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).Returns(Task.FromResult<Book>(null));
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);
            var handler = new DeleteBookCommandHandler(bookRepository.Object, libraryRepository.Object,ownerRepository.Object);


            //Act
            var result = await handler.Handle(deleteBookCommand, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("This book does not exist."));

        }

        [Test]
        public async Task Handle_NotOwnerOfLibrary_ReturnsFailureResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var ownerId = Guid.NewGuid();
            var username = "username";
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var ownerIdOfLibrary = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var deleteBookCommand = new DeleteBookCommand(username, bookId);
            var book = new Book("Title", "Description", "Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            var library = new Library("LibraryName", true, ownerIdOfLibrary);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);
            var handler = new DeleteBookCommandHandler(bookRepository.Object, libraryRepository.Object, ownerRepository.Object);


            //Act
            var result = await handler.Handle(deleteBookCommand, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("Only the library owner who added the book can delete it."));

        }

        [Test]
        public async Task Handle_OwnerDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();

            var ownerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var username = "username";

            var deleteBookCommand = new DeleteBookCommand(username, bookId);
            var book = new Book("Title", "Description", "Author", 200, Genres.Fantasy, 2020, 3, ownerId);
            var library = new Library("LibraryName", true, ownerId);
            bookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);
            ownerRepository.Setup(repo => repo.GetByUsername(username)).Returns(Task.FromResult<Owner>(null));
            var handler = new DeleteBookCommandHandler(bookRepository.Object, libraryRepository.Object, ownerRepository.Object);


            //Act
            var result = await handler.Handle(deleteBookCommand, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("This owner does not exist."));
        }
    }
}
