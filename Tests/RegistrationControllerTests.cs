using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Users.AddOwner;
using OnlineLibary.Modules.Catalog.Application.Users.AddReader;
using OnlineLibrary.API.Modules.UserAccess.Controllers;
using OnlineLibrary.API.Modules.UserAccess.Requests;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.AddOwner;
using OnlineLibrary.Modules.UserAccess.Application.AddReader;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace IntegrationTests
{
    [TestFixture]
    public class RegistrationControllerTests
    {
        private RegistrationController _registrationController;
        private Mock<IUserAccessModule> _userAccessModuleMock;
        private Mock<ICatalogModule> _catalogModuleMock;

        [SetUp]
        public void Setup()
        {
            _userAccessModuleMock = new Mock<IUserAccessModule>();
            _catalogModuleMock = new Mock<ICatalogModule>();
            _registrationController = new RegistrationController(_userAccessModuleMock.Object, _catalogModuleMock.Object);
        }

        [Test]
        public async Task OwnerRegistration_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var registerRequest = new RegisterRequest
            {
                Username = "username",
                Password = "password",
                Email = "user@example.com",
                FirstName = "FirstName",
                LastName = "LastName"

            };
            var result1 = Result.Success;
            var result2 = Result.Success;
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<RegisterOwnerCommand>())).ReturnsAsync(result1);
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<AddOwnerCommand>())).ReturnsAsync(result2);

            //Act
            var actionResult = await _registrationController.OwnerRegistrationAsync(registerRequest) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

       

        [Test]
        public async Task ReaderRegistration_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var registerRequest = new RegisterRequest
            {
                Username = "username",
                Password = "password",
                Email = "user@example.com",
                FirstName = "FirstName",
                LastName = "LastName"

            };
            var result1 = Result.Success;
            var result2 = Result.Success;
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<RegisterReaderCommand>())).ReturnsAsync(result1);
            _catalogModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<AddReaderCommand>())).ReturnsAsync(result2);

            //Act
            var actionResult = await _registrationController.ReaderRegistrationAsync(registerRequest) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
    }
}
