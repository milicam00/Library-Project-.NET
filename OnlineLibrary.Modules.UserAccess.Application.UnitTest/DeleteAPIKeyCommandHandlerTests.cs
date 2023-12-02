using Moq;
using OnlineLibrary.Modules.UserAccess.Application.DeleteAPIKey;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class DeleteAPIKeyCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";

            //Act
            var command = new DeleteApiKeyCommand(username);

            //Assert
            Assert.That(command.Username, Is.EqualTo(username));
        }

        [Test]  
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var username = "username";
            var name = "name";
            APIKey apiKey = new APIKey(username, name); 
           
            var apiKeyRepository = new Mock<IAPIKeyRepository>();   
            apiKeyRepository.Setup(repo=>repo.GetAsync(username)).ReturnsAsync(apiKey);

            var command = new DeleteApiKeyCommand(username);
            var handler = new DeleteApiKeyCommandHandler(apiKeyRepository.Object);  

            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_UserHasNoKey_ReturnsFailureResult()
        {
            //Arrange
            var username = "username";

            var apiKeyRepository = new Mock<IAPIKeyRepository>();
            apiKeyRepository.Setup(repo => repo.GetAsync(username)).Returns(Task.FromResult<APIKey>(null));

            var command = new DeleteApiKeyCommand(username);
            var handler = new DeleteApiKeyCommandHandler(apiKeyRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("This user has no key."));
        }
    }
}
