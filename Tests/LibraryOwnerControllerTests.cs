using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Libraries.CreateLibrary;
using OnlineLibrary.API.Modules.Catalog.Libraries.Controllers;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using System.Security.Claims;

namespace IntegrationTests
{
    [TestFixture]
    public class LibraryOwnerControllerTests
    {
        private LibraryOwnerController _libraryOwnerController;
        private Mock<ICatalogModule> _catalogModuleMock;

        [SetUp]
        public void Setup()
        {
            _catalogModuleMock = new Mock<ICatalogModule>();
            _libraryOwnerController = new LibraryOwnerController(_catalogModuleMock.Object);
        }

        [Test]
        public async Task AddLibrary_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var request = new AddLibraryRequest
            {
                LibraryName = "LibraryName",
                IsActive = true
               
            };
            var username = "username";
            var ownerId = Guid.NewGuid();
            var libraryResult = Result.Success;
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateLibraryCommand>())).ReturnsAsync(libraryResult);
            var claims = new[]
           {
                new Claim(ClaimTypes.Name, username),
                new Claim("ownerId", ownerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _libraryOwnerController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            // Act
            var actionResult = await _libraryOwnerController.AddLibraryAsync(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }


        [Test]
        public async Task AddLibrary_LibraryWithSameNameAlreadyExists_ReturnFailureResult()
        {
            //Arrange
            var request = new AddLibraryRequest
            {
                LibraryName = "LibraryName",
                IsActive = true
            };
            var username = "username";
            var ownerId = Guid.NewGuid();

            var errorMessage = "Library with same name already exist.";
            var libraryResult = Result.Failure(errorMessage);
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateLibraryCommand>())).ReturnsAsync(libraryResult);
            var claims = new[]
          {
                new Claim(ClaimTypes.Name, username),
                new Claim("ownerId", ownerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            _libraryOwnerController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            // Act
            var actionResult = await _libraryOwnerController.AddLibraryAsync(request) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(actionResult.Value, Is.EqualTo(errorMessage));
        }
    }
}
