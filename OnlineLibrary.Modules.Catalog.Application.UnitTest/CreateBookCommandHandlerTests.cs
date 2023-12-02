using Moq;
using OnlineLibary.Modules.Catalog.Application.Books.CreateBook;
using OnlineLibary.Modules.Catalog.Application.LibraryBooks.CreateBook;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class CreateBookCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var title = "Title";
            var description = "Description";
            var author = "Author";
            var pages = 200;
            var genres = Genres.Science;
            var pubblicationYear = 2022;
            var numCopies = 400;
            var libraryId = Guid.NewGuid();

            //Act
            var createBookCommand = new CreateBookCommand(title, description, author, pages, genres, pubblicationYear, numCopies, libraryId);

            //Assert
            Assert.That(createBookCommand.Title, Is.EqualTo(title));
            Assert.That(createBookCommand.Description, Is.EqualTo(description));
            Assert.That(createBookCommand.Author, Is.EqualTo(author));
            Assert.That(createBookCommand.Pages, Is.EqualTo(pages));
            Assert.That(createBookCommand.PubblicationYear, Is.EqualTo(pubblicationYear));
            Assert.That(createBookCommand.Genres, Is.EqualTo(genres));
            Assert.That(createBookCommand.NumberOfCopies, Is.EqualTo(numCopies));
            Assert.That(createBookCommand.LibraryId, Is.EqualTo(libraryId));
           
        }
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();

            var handler = new CreateBookCommandHandler(bookRepository.Object, libraryRepository.Object);

            var command = new CreateBookCommand(
                "Sample Book",
                "Sample Description",
                "Sample Author",
                200,
                Genres.Travel,
                2022,
                5,
                Guid.NewGuid());

            libraryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Library());
            bookRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Asert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_InvalidLibrary_ReturnsFailureResult()
        {
            // Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();

            var handler = new CreateBookCommandHandler(bookRepository.Object, libraryRepository.Object);

            var command = new CreateBookCommand(
                "Sample Book",
                "Sample Description",
                "Sample Author",
                200,
                Genres.Fantasy,
                2022,
                5,
                Guid.NewGuid());


            libraryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Library)null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This library does not exist."));
        }
    }
}