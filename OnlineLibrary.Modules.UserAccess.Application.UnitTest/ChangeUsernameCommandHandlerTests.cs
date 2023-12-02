using Moq;
using OnlineLibrary.Modules.UserAccess.Application.Authentication;
using OnlineLibrary.Modules.UserAccess.Application.ChangeUsername;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class ChangeUsernameCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var userName = "testuser";
            var newUsername = "newUsername";
            var password = "Password@123";
            var email = "emailtest";
            var firstName = "Test";
            var lastName = "Test";
            var user = new User(userId, userName, PasswordManager.HashPassword(password), email, firstName, lastName, UserRole.Owner);
            user.IsActive = true;
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.GetByUsernameAsync(userName)).ReturnsAsync(user);
            userRepository.Setup(r => r.GetByUsernameAsync(newUsername)).Returns(Task.FromResult<User>(null));

            var handler = new ChangeUsernameCommandHandler(userRepository.Object);
            var command = new ChangeUsernameCommand(userName, userName, newUsername);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
        }

    }
}
