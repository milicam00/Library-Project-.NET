using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Users.BlockReader;
using OnlineLibary.Modules.Catalog.Application.Users.UnblockReader;
using OnlineLibrary.API.Modules.Catalog.Readers.Controllers;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.BlockUser;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Application.UnblockUser;

namespace IntegrationTests
{
    [TestFixture]
    public class ReaderControllerTests
    {
        private ReaderController _readerController;
        private Mock<ICatalogModule> _catalogModuleMock;
        private Mock<IUserAccessModule> _userAccessModuleMock;

        [SetUp]
        public void Setup()
        {
            _userAccessModuleMock = new Mock<IUserAccessModule>();
            _catalogModuleMock = new Mock<ICatalogModule>();
            _readerController = new ReaderController(_catalogModuleMock.Object, _userAccessModuleMock.Object);
        }


        [Test]
        public async Task BlockUser_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var username = "username";
            var result1 = Result.Success;
            var result2 = Result.Success;
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<BlockReaderCommand>())).ReturnsAsync(result1);
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<BlockUserCommand>())).ReturnsAsync(result2);

            //Act
            var actionResult = await _readerController.BlockUserAsync(username) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task UnblockUser_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var username = "username";
            var result1 = Result.Success;
            var result2 = Result.Success;
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<UnblockReaderCommand>())).ReturnsAsync(result1);
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<UnblockUserCommand>())).ReturnsAsync(result2);

            //Act
            var actionResult = await _readerController.UnblockUserAsync(username) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
    }
}
