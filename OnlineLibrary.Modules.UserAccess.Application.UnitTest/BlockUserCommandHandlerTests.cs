using Moq;
using OnlineLibrary.Modules.UserAccess.Application.BlockUser;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class BlockUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidRequest_BlockUserSuccessfully()
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

            var handler = new BlockUserCommandHandler(userRepository.Object);
            var command = new BlockUserCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        public async Task Handle_UserAlreadyBlocked_BlockUserFailureResult()
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

            var handler = new BlockUserCommandHandler(userRepository.Object);
            var command = new BlockUserCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
        }


        [Test]
        public async Task Handle_UserInvalid_BlockUserFailureResult()
        {
            //Arrange
            var username = "testUsername";
            
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).Returns(Task.FromResult<User>(null));

            var handler = new BlockUserCommandHandler(userRepository.Object);
            var command = new BlockUserCommand(username);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));
        }
    }
}
