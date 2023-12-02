using Moq;
using OnlineLibrary.Modules.UserAccess.Application.UnblockUser;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class UnblockUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidRequest_UnblockUserSuccessfully()
        {
            //Arrange
            var username = "testUsername";
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = username,
                IsActive = false
            };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);

            var handler = new UnblockUserCommandHandler(userRepository.Object);
            var command = new UnblockUserCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_UserAlreadyUnblocked_FailureResult()
        {
            //Arrange
            var username = "testUsername";
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = username,
                IsActive = true
            };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);

            var handler = new UnblockUserCommandHandler(userRepository.Object);
            var command = new UnblockUserCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
