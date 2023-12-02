using Moq;
using OnlineLibrary.Modules.UserAccess.Application.EditAPIKey;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class EditAPIKeyCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";
            var name = "name";

            //Act
            var command = new EditAPIKeyCommand(username, name);

            //Assert
            Assert.That(command.Username, Is.EqualTo(username));
            Assert.That(command.NewName, Is.EqualTo(name)); 
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var username = "username";
            var name = "name";
            APIKey apiKey = new APIKey(username, name);

            var apiKeyRepository = new Mock<IAPIKeyRepository>();
            apiKeyRepository.Setup(repo => repo.GetAsync(username)).ReturnsAsync(apiKey);

            var command = new EditAPIKeyCommand(username, name);
            var handler = new EditAPIKeyCommandHandler(apiKeyRepository.Object);

            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]  
        public async Task Handle_InvalidApiKey_ReturnsFailureResult()
        {
            //Arrange
            var username = "username";
            var name = "name";
            APIKey apiKey = new APIKey(username, name);

            var apiKeyRepository = new Mock<IAPIKeyRepository>();
            apiKeyRepository.Setup(repo => repo.GetAsync(username)).Returns(Task.FromResult<APIKey>(null));

            var command = new EditAPIKeyCommand(username, name);
            var handler = new EditAPIKeyCommandHandler(apiKeyRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("Invalid API key."));
        }
    }
}
