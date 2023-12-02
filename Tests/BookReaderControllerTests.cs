using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Books.GetBook;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Controllers;
using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Requests;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace IntegrationTests
{
    [TestFixture]
    public class BookReaderControllerTests
    {
        private BookReaderController _bookReaderController;
        private Mock<ICatalogModule> _catalogModuleMock;

        [SetUp]
        public void Setup()
        {
            _catalogModuleMock = new Mock<ICatalogModule>();
            _bookReaderController = new BookReaderController(_catalogModuleMock.Object);
        }

        [Test]
        public async Task SearchBooks_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var request = new BookSearchRequest
            {
               
                Author = "Test",
                PageNumber = 1,
                PageSize = 10
            };

            var books = new List<BookDto>
            {
                new BookDto
                {
                    BookId = Guid.NewGuid(),
                    Title = "Test1",
                    Description = "Test",
                    Author = "Test",
                    Pages = 100,
                    Genres = Genres.Science,
                    PubblicationYear = 2020,
                    UserRating = 4,
                    NumberOfCopies = 100
                },
                 new BookDto
                {
                    BookId = Guid.NewGuid(),
                    Title = "Test2",
                    Description = "Test",
                    Author = "Test",
                    Pages = 100,
                    Genres = Genres.Science,
                    PubblicationYear = 2020,
                     UserRating = 4,
                     NumberOfCopies = 200
                 },
            };
            var result = Result.Success(books);
            _catalogModuleMock.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetBookQuery>())).ReturnsAsync(result);

            //Act
            var actionResult = await _bookReaderController.SearchBooksAsync(request) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }     
        
    }
}
