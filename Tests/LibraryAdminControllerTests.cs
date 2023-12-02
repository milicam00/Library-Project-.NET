using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Libraries.DeactivateLibrary;
using OnlineLibary.Modules.Catalog.Application.Libraries.GetLibrariesQuery;
using OnlineLibrary.API.Modules.Catalog.Libraries.Controllers;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;

namespace IntegrationTests
{
    [TestFixture]
    public class LibraryAdminControllerTests
    {
        private LibraryAdminController _libraryAdminController;
        private Mock<ICatalogModule> _catalogModuleMock;

        [SetUp]
        public void Setup()
        {
            _catalogModuleMock = new Mock<ICatalogModule>();
            _libraryAdminController = new LibraryAdminController(_catalogModuleMock.Object);

        }

        [Test]
        public async Task EditActivate_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var libraryId = Guid.NewGuid();
            var commandResult = Result.Success("Library is blocked");
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<DeactivateLibraryCommand>())).ReturnsAsync(commandResult);

            // Act
            var actionResult = await _libraryAdminController.EditActivateAsync(libraryId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(actionResult.Value, Is.EqualTo("Library is blocked."));
        }

        [Test]
        public async Task GetLibaries_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var libraries = new List<LibraryDto>
            {
                new LibraryDto
                {
                    LibraryId = Guid.NewGuid(),
                    Name = "Test 1",
                    IsActive = true
                },
                new LibraryDto
                {
                    LibraryId = Guid.NewGuid(),
                    Name = "Test 2",
                    IsActive = true
                }
            };

            _catalogModuleMock.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetLibrariesQuery>())).ReturnsAsync(libraries);

            //Act
            var actionResult = await _libraryAdminController.GetLibrariesAsync() as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.IsNotNull(actionResult.Value);
            Assert.That(actionResult.Value, Is.EqualTo(libraries));
        }


        [Test]
        public async Task GetLibraries_WithNoData_ReturnsBadRequesResult()
        {
            //Arrange
            _catalogModuleMock.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetLibrariesQuery>())).ReturnsAsync((List<LibraryDto>)null);

            //Act
            var actionResult = await _libraryAdminController.GetLibrariesAsync() as BadRequestResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }
    }
}

