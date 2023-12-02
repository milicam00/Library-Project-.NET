using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Rentals.CreateRental;
using OnlineLibary.Modules.Catalog.Application.Rentals.RateBook;
using OnlineLibary.Modules.Catalog.Application.Rentals.ReturnBooks;
using OnlineLibrary.API.Modules.Catalog.Rental.Controllers;
using OnlineLibrary.API.Modules.Catalog.Rental.Requests;
using OnlineLibrary.BuildingBlocks.Domain;
using System.Security.Claims;

namespace IntegrationTests
{
    [TestFixture]
    public class RentalReaderControllerTests
    {
        private RentalReaderController _rentalReaderController;
        private Mock<ICatalogModule> _catalogModuleMock;

        [SetUp]
        public void Setup()
        {
            _catalogModuleMock = new Mock<ICatalogModule>();
            _rentalReaderController = new RentalReaderController(_catalogModuleMock.Object);
        }

        [Test]
        public async Task RentalBook_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var request = new RentalRequest
            {
                UserId = Guid.NewGuid(),
                BookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };
            var username = "username";
            var readerId = Guid.NewGuid();
            var rentalResult = Result.Success;
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateRentalCommand>())).ReturnsAsync(rentalResult);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("readerId", readerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _rentalReaderController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            //Act
            var actionResult = await _rentalReaderController.RentalBookAsync(request) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task RentalBook_WithInvalidData_ReturnsBadRequestResult()
        {
            //Arrange
            var request = new RentalRequest
            {
                UserId = Guid.NewGuid(),
                BookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };
            var username = "username";
            var readerId = Guid.NewGuid();
            var errorMessage = "This reader does not exist";
            var rentalResult = Result.Failure(errorMessage);
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateRentalCommand>())).ReturnsAsync(rentalResult);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("readerId", readerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _rentalReaderController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            //Act
            var actionResult = await _rentalReaderController.RentalBookAsync(request) as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));

        }

        [Test]
        public async Task RateBook_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var rentalId = Guid.NewGuid();
            var username = "username";
            var readerId = Guid.NewGuid();
            var request = new RateRequest
            {
                Rate = 5,
                Comment = "TestComment"
            };
            var rentalResult = Result.Success;
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<RateBookCommand>())).ReturnsAsync(rentalResult);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("readerId", readerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _rentalReaderController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            //Act
            var actionResult = await _rentalReaderController.RateBookAsync(rentalId, request) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task RateBook_WithInvalidData_ReturnsBadRequestResult()
        {
            //Arrange
            var rentalId = Guid.NewGuid();
            var username = "username";
            var readerId = Guid.NewGuid();
            var request = new RateRequest
            {
                Rate = 5,
                Comment = "TestComment"
            };
            var errorMessage = "This reader does not exist";
            var rentalResult = Result.Failure(errorMessage);
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<RateBookCommand>())).ReturnsAsync(rentalResult);
            var claims = new[]
           {
                new Claim(ClaimTypes.Name, username),
                new Claim("readerId", readerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _rentalReaderController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            //Act
            var actionResult = await _rentalReaderController.RateBookAsync(rentalId, request) as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));

        }

        [Test]
        public async Task ReturnBooks_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var username = "username";
            var readerId = Guid.NewGuid();
            var result = Result.Success;
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<ReturnBooksCommand>())).ReturnsAsync(result);
            var claims = new[]
           {
                new Claim(ClaimTypes.Name, username),
                new Claim("readerId", readerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _rentalReaderController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            //Act
            var actionResult = await _rentalReaderController.ReturnBooksAsync() as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task ReturnBooks_WithInvalidData_ReturnsBadRequestResult()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var errorMessage = "This reader is returned all books.";
            var result = Result.Failure(errorMessage);
            var username = "username";
            var readerId = Guid.NewGuid();
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<ReturnBooksCommand>())).ReturnsAsync(result);
            var claims = new[]
           {
                new Claim(ClaimTypes.Name, username),
                new Claim("readerId", readerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _rentalReaderController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            //Act
            var actionResult = await _rentalReaderController.ReturnBooksAsync() as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));

        }

    }
}
