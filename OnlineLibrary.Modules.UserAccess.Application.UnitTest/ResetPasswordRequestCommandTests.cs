using Moq;
using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.Modules.UserAccess.Application.ResetPasswordRequest;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class ResetPasswordRequestCommandTests
    {
        [Test]
        public async Task Handle_ValidRequest_SendsEmailWithResetCode()
        {
            //Arrange
            var username = "username";
            var user = new User();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            var emailService = new Mock<IEmailService>();

            var handler = new ResetPasswordRequestCommandHandler(userRepository.Object, emailService.Object);
            var command = new ResetPasswordRequestCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);


        }


        [Test]
        public async Task Handle_InvalidUser_ResultFailure()
        {
            //Arrange
            var username = "username";
            var user = new User();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).Returns(Task.FromResult<User>(null));
            var emailService = new Mock<IEmailService>();

            var handler = new ResetPasswordRequestCommandHandler(userRepository.Object, emailService.Object);
            var command = new ResetPasswordRequestCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));

        }


    }
}
