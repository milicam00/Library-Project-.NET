//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using OnlineLibary.Modules.Catalog.Application.Books.ChangeBook;
//using OnlineLibary.Modules.Catalog.Application.Books.CreateBook;
//using OnlineLibary.Modules.Catalog.Application.Books.DeleteBook;
//using OnlineLibary.Modules.Catalog.Application.Contracts;
//using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Controllers;
//using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Requests;
//using OnlineLibrary.BuildingBlocks.Domain;
//using System.Security.Claims;

//namespace IntegrationTests
//{
//    [TestFixture]
//    public class BookOwnerControllerTests
//    {
//        private BookOwnerController _bookOwnerController;
//        private Mock<ICatalogModule> _catalogModuleMock;

//        [SetUp]
//        public void Setup()
//        {
//            _catalogModuleMock = new Mock<ICatalogModule>();
//            _bookOwnerController = new BookOwnerController(_catalogModuleMock.Object);
//        }

//        [Test]
//        public async Task AddBook_WithValidData_ReturnsOkResult()
//        {
//            //Arrange
//            var request = new CreateBookRequest
//            {
//                Title = "Title",
//                Description = "Description",
//                Author = "Author",
//                Pages = 100,
//                Genres = OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions.Genres.Romance,
//                PubblicationYear = 2022,
//                NumberOfCopies = 100,
//                Library = Guid.NewGuid()
//            };


//            var bookResult = Result.Success;
//            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateBookCommand>())).ReturnsAsync(bookResult);

//            //Act
//            var actionResult = await _bookOwnerController.AddBookAsync(request) as OkObjectResult;

//            //Assert
//            Assert.IsNotNull(actionResult);
//            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
//        }

//        [Test]
//        public async Task AddBook_WithInvalidData_ReturnsBadRequestResult()
//        {
//            //Arrange
//            var request = new CreateBookRequest
//            {
//                Title = "Title",
//                Description = "Description",
//                Author = "Author",
//                Pages = 100,
//                Genres = OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions.Genres.Romance,
//                PubblicationYear = 2022,
//                NumberOfCopies = 100,
//                Library = Guid.NewGuid()
//            };

//            var errorMessage = "This library does not exist.";
//            var bookResult = Result.Failure(errorMessage);
//            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateBookCommand>())).ReturnsAsync(bookResult);

//            //Act
//            var actionResult = await _bookOwnerController.AddBookAsync(request) as BadRequestObjectResult;

//            //Assert
//            Assert.IsNotNull(actionResult);
//            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
//        }

//        [Test]
//        public async Task EditBook_WithValidData_ReturnsOkResult()
//        {
//            // Arrange
//            var bookId = Guid.NewGuid();
//            var username = "username";
//            var ownerId = Guid.NewGuid();
//            var changeBookRequest = new ChangeBookRequest
//            {
//                Title = "Updated Title",
//                Description = "Updated Description",
//                Author = "Updated Author",
//                Pages = 150,
//                PubblicationYear = 2023,
//                UserRating = 5,
//                NumOfCopies = 200
//            };

//            var bookResult = Result.Success;          

//            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<ChangeBookCommand>()))
//                .ReturnsAsync(bookResult);


//            var claims = new[]
//            {
//                new Claim(ClaimTypes.Name, username),
//                new Claim("ownerId", ownerId.ToString())
//            };

//            var identity = new ClaimsIdentity(claims, "TestAuthType");
//            var user = new ClaimsPrincipal(identity);

//            var httpContext = new DefaultHttpContext
//            {
//                User = user
//            };

//            _bookOwnerController.ControllerContext = new ControllerContext
//            {
//                HttpContext = httpContext
//            };

//            // Act
//            var actionResult = await _bookOwnerController.EditBookAsync(bookId, changeBookRequest) as OkObjectResult;

//            // Assert
//            Assert.IsNotNull(actionResult);
//            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
//        }


//        [Test]
//        public async Task UpdateBook_WithInvalidData_ReturnsBadRequestResult()
//        {
//            //Arrange
//            var bookId = Guid.NewGuid();
//            var username = "username";
//            var ownerId = Guid.NewGuid();
//            var changeBookRequest = new ChangeBookRequest
//            {
//                Title = "Updated Title",
//                Description = "Updated Description",
//                Author = "Updated Author",
//                Pages = 150,
//                PubblicationYear = 2023,
//                UserRating = 5,
//                NumOfCopies = 200
//            };


//            var claims = new[]
//            {
//                new Claim(ClaimTypes.Name, username),
//                new Claim("ownerId", ownerId.ToString())
//            };

//            var identity = new ClaimsIdentity(claims, "TestAuthType");
//            var user = new ClaimsPrincipal(identity);

//            var httpContext = new DefaultHttpContext
//            {
//                User = user
//            };

//            _bookOwnerController.ControllerContext = new ControllerContext
//            {
//                HttpContext = httpContext
//            };
//            var errorMessage = "Only owner of library can change the book.";
//            var bookResult = Result.Failure(errorMessage);
//            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<ChangeBookCommand>())).ReturnsAsync(bookResult);

//            //Act
//            var actionResult = await _bookOwnerController.EditBookAsync(bookId, changeBookRequest) as BadRequestObjectResult;

//            //Assert
//            Assert.IsNotNull(actionResult);
//            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
//        }

//        [Test]
//        public async Task RemoveBook_WithValidData_ReturnsOkResult()
//        {
//            //Arrange
//            var bookId = Guid.NewGuid();
//            var username = "username";
//            var ownerId = Guid.NewGuid();
//            var result = Result.Success;


//            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<DeleteBookCommand>()))
//                .ReturnsAsync(result);
//            var claims = new[]
//           {
//                new Claim(ClaimTypes.Name, username),
//                new Claim("ownerId", ownerId.ToString())
//            };

//            var identity = new ClaimsIdentity(claims, "TestAuthType");
//            var user = new ClaimsPrincipal(identity);

//            var httpContext = new DefaultHttpContext
//            {
//                User = user
//            };

//            _bookOwnerController.ControllerContext = new ControllerContext
//            {
//                HttpContext = httpContext
//            };

//            //Act
//            var actionResult = await _bookOwnerController.RemoveBookAsync(bookId) as OkObjectResult;

//            //Assert
//            Assert.IsNotNull(actionResult);
//            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
//        }


//        [Test]
//        public async Task RemoveBook_WithInalidData_ReturnsBadRequestResult()
//        {
//            //Arrange
//            var bookId = Guid.NewGuid();
//            var username = "username";
//            var ownerId = Guid.NewGuid();



//            var errorMessage = "Only the library owner who added the book can delete it.";
//            var bookResult = Result.Failure(errorMessage);
//            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<DeleteBookCommand>()))
//                .ReturnsAsync(bookResult);
//            var claims = new[]
//           {
//                new Claim(ClaimTypes.Name, username),
//                new Claim("ownerId", ownerId.ToString())
//            };

//            var identity = new ClaimsIdentity(claims, "TestAuthType");
//            var user = new ClaimsPrincipal(identity);

//            var httpContext = new DefaultHttpContext
//            {
//                User = user
//            };

//            _bookOwnerController.ControllerContext = new ControllerContext
//            {
//                HttpContext = httpContext
//            };

//            //Act
//            var actionResult = await _bookOwnerController.RemoveBookAsync(bookId) as BadRequestObjectResult;

//            //Assert
//            Assert.IsNotNull(actionResult);
//            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
//        }
//    }
//}
