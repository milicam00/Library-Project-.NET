using Moq;
using OnlineLibrary.Modules.UserAccess.Application.CreateAPIKey;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class CreateAPIKeyCommandTests 
    {
        [Test]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            //Arrange
            var username = "testuser";
            var keyName = "keyName";
            User user = new User();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo=>repo.GetByUsernameAsync(username)).ReturnsAsync(user);

            var apiKeyRepository = new Mock<IAPIKeyRepository>();

            var handler = new CreateAPIKeyCommandHandler(apiKeyRepository.Object, userRepository.Object);
            var command = new CreateAPIKeyCommand(username,keyName);

            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]  
        public async Task Handle_UserDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var username = "testuser";
            var keyName = "keyName";


            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.GetByUsernameAsync(username)).Returns(Task.FromResult<User>(null));

            var apiKeyRepository = new Mock<IAPIKeyRepository>();

            var handler = new CreateAPIKeyCommandHandler(apiKeyRepository.Object, userRepository.Object);
            var command = new CreateAPIKeyCommand(username, keyName);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("This user does not exist."));

        }
    }
}
