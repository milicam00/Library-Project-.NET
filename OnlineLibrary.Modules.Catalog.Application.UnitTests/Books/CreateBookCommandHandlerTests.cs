using Moq;
using OnlineLibary.Modules.Catalog.Application.Libraries.CreateLibrary;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTests.Books
{
    [TestFixture]
    public class CreateBookCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            var handler = new CreateLibraryCommandHandler
        }
    }
}
