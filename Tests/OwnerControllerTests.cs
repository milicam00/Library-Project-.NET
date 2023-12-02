using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Users.BlockOwner;
using OnlineLibary.Modules.Catalog.Application.Users.UnblockOwner;
using OnlineLibrary.API.Modules.Catalog.Owners;
using OnlineLibrary.BuildingBlocks.Application.ICsvGeneration;
using OnlineLibrary.BuildingBlocks.Application.IXlsxGeneration;
using OnlineLibrary.BuildingBlocks.Application.XmlGeneration;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.BlockUser;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Application.UnblockUser;

namespace IntegrationTests
{
    [TestFixture]
    public class OwnerControllerTests
    {
        private OwnerController _ownerController;
        private Mock<ICatalogModule> _catalogModuleMock;
        private Mock<IUserAccessModule> _userAccessModuleMock;
        private Mock<IXmlGenerationService> _xmlGenerationServiceMock;
        private Mock<ICsvGenerationService> _csvGenerationServiceMock;
        private Mock<IXlsxGenerationService> _xlsxGenerationServiceMock;

        [SetUp]
        public void Setup()
        {
            _userAccessModuleMock = new Mock<IUserAccessModule>();
            _catalogModuleMock = new Mock<ICatalogModule>();
            _xmlGenerationServiceMock = new Mock<IXmlGenerationService>();
            _csvGenerationServiceMock = new Mock<ICsvGenerationService>();  
            _xlsxGenerationServiceMock = new Mock<IXlsxGenerationService>();
            _ownerController = new OwnerController(_catalogModuleMock.Object, _userAccessModuleMock.Object,_xmlGenerationServiceMock.Object,_csvGenerationServiceMock.Object,_xlsxGenerationServiceMock.Object);
        }

        [Test]
        public async Task BlockUser_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var username = "username";
            var result1 = Result.Success;
            var result2 = Result.Success;
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<BlockOwnerCommand>())).ReturnsAsync(result1);
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<BlockUserCommand>())).ReturnsAsync(result2);

            //Act
            var actionResult = await _ownerController.BlockUserAsync(username) as OkObjectResult;

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
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<UnblockOwnerCommand>())).ReturnsAsync(result1);
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<UnblockUserCommand>())).ReturnsAsync(result2);

            //Act
            var actionResult = await _ownerController.UnblockUserAsync(username) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
    }
}
