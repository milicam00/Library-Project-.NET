using Moq;
using OnlineLibrary.Modules.UserAccess.Application.ResetPassword;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class ResetPasswordCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidRequest_ResultSuccess()
        {
            //Arrange
            var code = 12345;
            string username = "username";
            var password = "Password@123";
            var user = new User();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            var handler = new ResetPasswordCommandHandler(userRepository.Object);
            var command = new ResetPasswordCommand(code, username, password);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_InvalidUser_ResultFailure()
        {
            //Arrange
            var code = 12345;
            string userName = "username";
            var password = "Password@123";
            var user = new User();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(userName)).Returns(Task.FromResult<User>(null));
            var handler = new ResetPasswordCommandHandler(userRepository.Object);
            var command = new ResetPasswordCommand(code, userName, password);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));

        }


    }
}
