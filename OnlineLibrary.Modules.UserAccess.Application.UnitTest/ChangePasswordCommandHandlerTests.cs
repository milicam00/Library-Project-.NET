using Moq;
using OnlineLibrary.Modules.UserAccess.Application.Authentication;
using OnlineLibrary.Modules.UserAccess.Application.ChangePassword;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class ChangePasswordCommandHandlerTests
    {
        [Test]
        public async Task Task_Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var userName = "testuser";
            var password = "Password@123";
            var email = "emailtest";
            var firstName = "Test";
            var lastName = "Test";
            var newPassword = "NewPassword@123";
            var user = new User(userId, userName, PasswordManager.HashPassword(password), email, firstName, lastName, UserRole.Owner);
            user.IsActive = true;
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.GetByUsernameAsync(userName)).ReturnsAsync(user);

            var handler = new ChangePasswordCommandHandler(userRepository.Object);
            var command = new ChangePasswordCommand(userName, password, newPassword);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Task_Handle_InvalidUser_ReturnsFailureResult()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var userName = "testuser";
            var password = "Password@123";
            var email = "emailtest";
            var firstName = "Test";
            var lastName = "Test";
            var newPassword = "NewPassword@123";
            var user = new User(userId, userName, PasswordManager.HashPassword(password), email, firstName, lastName, UserRole.Owner);
            user.IsActive = true;
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.GetByUsernameAsync(userName)).Returns(Task.FromResult<User>(null));

            var handler = new ChangePasswordCommandHandler(userRepository.Object);
            var command = new ChangePasswordCommand(userName, password, newPassword);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist"));
        }


    }
}
