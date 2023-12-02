using Moq;
using OnlineLibrary.Modules.UserAccess.Application.Logout;
using OnlineLibrary.Modules.UserAccess.Domain.RefreshTokens;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{

    public class LogoutCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var refreshToken = "valid refresh token";

            //Act 
            var logoutCommand = new LogoutCommand(refreshToken);

            //Assert
            Assert.That(logoutCommand.RefreshToken, Is.EqualTo(refreshToken));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var tokens = new List<string> { "valid token 1", "valid token 2" };
            var userId = Guid.NewGuid();
            var refreshTokens = new List<RefreshToken>
            {
                new RefreshToken
                {
                    TokenId = Guid.NewGuid(),
                    Token = tokens[0],
                    UserId = userId
                },
                new RefreshToken()
                {
                    TokenId = Guid.NewGuid(),
                    Token = tokens[1],
                    UserId = userId
                }
            };

            var user = new User();
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            refreshTokenRepository.Setup(r => r.GetByToken(tokens[0])).ReturnsAsync(refreshTokens[0]);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(u => u.GetByIdAsync(userId)).ReturnsAsync(user);

            refreshTokenRepository.Setup(x => x.GetRefreshTokensByUser(userId)).ReturnsAsync(refreshTokens);

            var handler = new LogoutCommandHandler(refreshTokenRepository.Object, userRepository.Object);
            var command = new LogoutCommand(tokens[0]);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Data, Is.EqualTo("Successfully logout"));
        }

        [Test]
        public async Task Handle_UserDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var tokens = new List<string> { "valid token 1", "valid token 2" };
            var userId = Guid.NewGuid();
            var refreshTokens = new List<RefreshToken>
            {
                new RefreshToken
                {
                    TokenId = Guid.NewGuid(),
                    Token = tokens[0],
                    UserId = userId
                },
                new RefreshToken()
                {
                    TokenId = Guid.NewGuid(),
                    Token = tokens[1],
                    UserId = userId
                }
            };

            var user = new User();
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            refreshTokenRepository.Setup(r => r.GetByToken(tokens[0])).ReturnsAsync(refreshTokens[0]);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(u => u.GetByIdAsync(userId)).Returns(Task.FromResult<User>(null));

            refreshTokenRepository.Setup(x => x.GetRefreshTokensByUser(userId)).ReturnsAsync(refreshTokens);

            var handler = new LogoutCommandHandler(refreshTokenRepository.Object, userRepository.Object);
            var command = new LogoutCommand(tokens[0]);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));

        }

        [Test]
        public async Task Handle_RefreshTokenDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var tokens = new List<string> { "valid token 1", "valid token 2" };
            var userId = Guid.NewGuid();
            var refreshTokens = new List<RefreshToken>
            {
                new RefreshToken
                {
                    TokenId = Guid.NewGuid(),
                    Token = tokens[0],
                    UserId = userId
                },
                new RefreshToken()
                {
                    TokenId = Guid.NewGuid(),
                    Token = tokens[1],
                    UserId = userId
                }
            };

            var user = new User();
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            refreshTokenRepository.Setup(r => r.GetByToken(tokens[0])).Returns(Task.FromResult<RefreshToken>(null));

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(u => u.GetByIdAsync(userId)).ReturnsAsync(user);

            refreshTokenRepository.Setup(x => x.GetRefreshTokensByUser(userId)).ReturnsAsync(refreshTokens);

            var handler = new LogoutCommandHandler(refreshTokenRepository.Object, userRepository.Object);
            var command = new LogoutCommand(tokens[0]);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Refresh token does not exist."));

        }
    }
}
