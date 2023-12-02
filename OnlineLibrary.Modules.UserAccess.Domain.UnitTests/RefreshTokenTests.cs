using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Domain.UnitTests
{
    [TestFixture]
    public class RefreshTokenTests
    {
        [Test]
        public void RefreshToken_Should_Be_Active_When_Not_Expired_Or_Revoked()
        {
            //Arrange
            var token = "token123";
            var userId = Guid.NewGuid();

            //Act
            var refreshToken = RefreshToken.Create(token, userId);

            //Assert
            Assert.IsNotNull(refreshToken);
            Assert.That(refreshToken.Token, Is.EqualTo(token));
            Assert.That(refreshToken.UserId, Is.EqualTo(userId));
            Assert.IsTrue(refreshToken.IsActive);
            Assert.IsFalse(refreshToken.IsExpired);
            Assert.IsFalse(refreshToken.IsRevoked);

        }

        [Test]
        public void RefreshToken_Should_Be_Expired_When_Expired()
        {
            //Arrange
            var token = "token123";
            var userId = Guid.NewGuid();
            var expiredRefreshToken = new RefreshToken(token, userId)
            {
                Expires = DateTime.UtcNow.AddMinutes(-1) 
            };

            //Act
            var isExpired = expiredRefreshToken.IsExpired;

            //Assert
            Assert.IsTrue(isExpired);
        }

        [Test]
        public void RefreshToken_Should_Not_Be_Active_When_Expired()
        {
            // Arrange
            var token = "expiredToken";
            var userId = Guid.NewGuid();
            var expiredRefreshToken = new RefreshToken(token, userId)
            {
                Expires = DateTime.UtcNow.AddMinutes(-1)
            };

            // Act
            var isActive = expiredRefreshToken.IsActive;

            // Assert
            Assert.IsFalse(isActive);
        }

        [Test]
        public void RefreshToken_Should_Not_Be_Active_When_Revoked()
        {
            // Arrange
            var token = "revokedToken";
            var userId = Guid.NewGuid();
            var revokedRefreshToken = new RefreshToken(token, userId)
            {
                Revoked = DateTime.UtcNow 
            };

            // Act
            var isActive = revokedRefreshToken.IsActive;

            // Assert
            Assert.IsFalse(isActive);
        }
    }
}
