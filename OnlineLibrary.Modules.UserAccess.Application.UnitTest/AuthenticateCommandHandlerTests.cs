using Moq;
using OnlineLibrary.Modules.UserAccess.Application.Authentication;
using OnlineLibrary.Modules.UserAccess.Application.Authentication.Authenticate;
using OnlineLibrary.Modules.UserAccess.Domain.RefreshTokens;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    public class AuthenticateCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";
            var password = "Password@123";

            //Act
            var authenticateCommand = new AuthenticateCommand(username, password);

            //Assert
            Assert.That(authenticateCommand.UserName, Is.EqualTo(username));
            Assert.That(authenticateCommand.Password, Is.EqualTo(password));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var userName = "testuser";
            var password = "Password@123";
            var email = "emailtest";
            var firstName = "Test";
            var lastName = "Test";

            var user = new User(Guid.NewGuid(), userName, PasswordManager.HashPassword(password), email, firstName, lastName, UserRole.Owner);
            user.IsActive = true;

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(userName)).ReturnsAsync(user);

            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            refreshTokenRepository.Setup(r => r.AddAsync(It.IsAny<RefreshToken>()));

            var handler = new AuthenticateCommandHandler(userRepository.Object, refreshTokenRepository.Object);
            var command = new AuthenticateCommand(userName, password);

            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);


        }

        [Test]
        public async Task Handle_InvalidUser_ReturnsFailureResult()
        {
            //Arrange
            var userName = "testuser";
            var password = "Password@123";
            var email = "emailtest";
            var firstName = "Test";
            var lastName = "Test";

            var user = new User(Guid.NewGuid(), userName, PasswordManager.HashPassword(password), email, firstName, lastName, UserRole.Owner);
            user.IsActive = true;

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(userName)).Returns(Task.FromResult<User>(null));

            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            refreshTokenRepository.Setup(r => r.AddAsync(It.IsAny<RefreshToken>()));

            var handler = new AuthenticateCommandHandler(userRepository.Object, refreshTokenRepository.Object);
            var command = new AuthenticateCommand(userName, password);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("This user does not exist."));


        }

        [Test]
        public async Task Handle_UserIsNotActive_ReturnsFailureResult()
        {
            //Arrange
            var userName = "testuser";
            var password = "Password@123";
            var email = "emailtest";
            var firstName = "Test";
            var lastName = "Test";

            var user = new User(Guid.NewGuid(), userName, PasswordManager.HashPassword(password), email, firstName, lastName, UserRole.Owner);
            user.IsActive = false;

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(userName)).ReturnsAsync(user);

            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            refreshTokenRepository.Setup(r => r.AddAsync(It.IsAny<RefreshToken>()));

            var handler = new AuthenticateCommandHandler(userRepository.Object, refreshTokenRepository.Object);
            var command = new AuthenticateCommand(userName, password);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("User is not active!"));


        }

    }
}