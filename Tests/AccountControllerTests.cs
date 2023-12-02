using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.API.Modules.UserAccess.Controllers;
using OnlineLibrary.API.Modules.UserAccess.Requests;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.ChangePassword;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Application.Logout;
using OnlineLibrary.Modules.UserAccess.Application.ResetPassword;
using OnlineLibrary.Modules.UserAccess.Application.ResetPasswordRequest;
using OnlineLibrary.Modules.UserAccess.Application.TokenRefresh;
using OnlineLibrary.Modules.UserAccess.Domain.Users;
using System.Linq.Expressions;

namespace Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _accountController;
        private Mock<IUserAccessModule> _userAccessModuleMock;
        private Mock<ICatalogModule> _catalogModuleMock;
        
        [SetUp]
        public void Setup()
        {
            _userAccessModuleMock = new Mock<IUserAccessModule>();
            _catalogModuleMock = new Mock<ICatalogModule>();
            _accountController = new AccountController(_userAccessModuleMock.Object, _catalogModuleMock.Object);
        }

        [Test]
        public async Task ResetPasswordRequest_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var username = "username";
            var result = Result.Success;
            var request = new ResetPasswordRequest
            {
                Username= username
            };
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<ResetPasswordRequestCommand>())).ReturnsAsync(result);

            //Act
            var actionResult = await _accountController.ForgotPasswordAsync(request) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task ResetPassword_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var username = "username";
            var request = new NewPasswordRequest
            {
                Password = "password",
                Code = 12345
            };
            var result = Result.Success;
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<ResetPasswordCommand>())).ReturnsAsync(result);

            //Act
            var actionResult = await _accountController.ResetPasswordAsync(request) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task RefreshToken_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var request = new RefreshTokenRequest
            {
                RefreshToken = "refresh_token"
            };
            var result = Result.Success;
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<RefreshTokenCommand>())).ReturnsAsync(result);

            //Act
            var actionResult = await _accountController.RefrehTokenAsync(request) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task Logout_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var request = new LogoutRequest
            {
                RefreshToken = "refreshToken"
            };
            var result = Result.Success;
            _userAccessModuleMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<LogoutCommand>())).ReturnsAsync(result);

            //Act
            var actionResult = await _accountController.LogoutAsync(request) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
    }
}