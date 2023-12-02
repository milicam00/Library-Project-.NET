using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibrary.API.Modules.UserAccess.Controllers;
using OnlineLibrary.API.Modules.UserAccess.Requests;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Authentication.Authenticate;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace IntegrationTests
{
    [TestFixture]
    public class LoginControllerTests
    {
        private LoginController _loginController;
        private Mock<IUserAccessModule> _userAccessModule;

        [SetUp]
        public void Setup()
        {
            _userAccessModule = new Mock<IUserAccessModule>();  
            _loginController = new LoginController(_userAccessModule.Object);   
        }

        [Test]
        public async Task Login_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var loginRequest = new LoginRequest
            {
                UserName = "username",
                Password = "password",
            };
            var result = Result.Success;
            _userAccessModule.Setup(x => x.ExecuteCommandAsync(It.IsAny<AuthenticateCommand>())).ReturnsAsync(result);

            //Act
            var actionResult = await _loginController.LoginAsync(loginRequest) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
    }
}
