using Moq;
using OnlineLibrary.Modules.UserAccess.Application.TokenRefresh;
using OnlineLibrary.Modules.UserAccess.Domain.RefreshTokens;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class ReFreshTokenCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidRequest_RefreshToken_ResultSuccess()
        {
            // Arrange
            var refreshTokenValue = "valid_refresh_token";
            var userId = Guid.NewGuid();
            var user = new User(userId, "testuser", "password", "testuser@example.com", "Test", "Test", UserRole.Owner);
            var refreshToken = new RefreshToken(refreshTokenValue, userId);

            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            refreshTokenRepository.Setup(r => r.GetByToken(refreshTokenValue)).ReturnsAsync(refreshToken);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            var handler = new RefreshTokenCommandHandler(refreshTokenRepository.Object, userRepository.Object);
            var command = new RefreshTokenCommand(refreshTokenValue);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

    }
}
