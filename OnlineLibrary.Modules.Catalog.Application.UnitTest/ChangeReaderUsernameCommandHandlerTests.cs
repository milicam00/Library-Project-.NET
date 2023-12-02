using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.BlockReader;
using OnlineLibary.Modules.Catalog.Application.Users.ChangeReaderUsername;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ChangeReaderUsernameCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";

            //Act
            var blockReaderCommand = new BlockReaderCommand(username);

            //Assert
            Assert.That(blockReaderCommand.UserName, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var oldUsername = "oldUsername";
            var newUsername = "newUsername";

            Reader reader = new Reader
            {
                UserName = oldUsername,
            };

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(oldUsername)).ReturnsAsync(reader);
            readerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Reader>(null));

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Owner>(null));

            var command = new ChangeReaderUsernameCommand(oldUsername, newUsername);
            var handler = new ChangeReaderUsernameCommandHandler(readerRepository.Object, ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_ReaderDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var oldUsername = "oldUsername";
            var newUsername = "newUsername";

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(oldUsername)).Returns(Task.FromResult<Reader>(null));
            readerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Reader>(null));

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Owner>(null));

            var command = new ChangeReaderUsernameCommand(oldUsername, newUsername);
            var handler = new ChangeReaderUsernameCommandHandler(readerRepository.Object, ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Reader does  not exist."));
        }
    }
}
