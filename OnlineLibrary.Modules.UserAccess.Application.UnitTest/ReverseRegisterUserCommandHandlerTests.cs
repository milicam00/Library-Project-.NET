using Moq;
using OnlineLibrary.Modules.UserAccess.Application.ReverseRegisterReader;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class ReverseRegisterUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var username = "testUsername";
            var user = new User();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.GetByUsernameAsync(username)).ReturnsAsync(user);
            var handler = new ReverseRegisterUserCommandHandler(userRepository.Object);
            var command = new ReverseRegisterUserCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_InvalidUser_ReturnsFailureResult()
        {
            //Arrange
            var username = "testUsername";
            
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.GetByUsernameAsync(username)).Returns(Task.FromResult<User>(null));
            var handler = new ReverseRegisterUserCommandHandler(userRepository.Object);
            var command = new ReverseRegisterUserCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));

        }
    }
}
