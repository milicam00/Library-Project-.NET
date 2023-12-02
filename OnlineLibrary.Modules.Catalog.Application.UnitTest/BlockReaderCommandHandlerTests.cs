using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.BlockOwner;
using OnlineLibary.Modules.Catalog.Application.Users.BlockReader;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class BlockReaderCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";

            //Act
            var blockOwnerCommand = new BlockOwnerCommand(username);

            //Assert
            Assert.That(blockOwnerCommand.UserName, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var username = "username";
            var readerId = Guid.NewGuid();

            Reader reader = new Reader
            {
                ReaderId = readerId,
                UserName = username
            };

           

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(reader);

            var command = new BlockReaderCommand(username);
            var handler = new BlockReaderCommandHandler(readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_UserDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var username = "username";
            var readerId = Guid.NewGuid();

            Reader reader = new Reader
            {
                ReaderId = readerId,
                UserName = username
            };



            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(username)).Returns(Task.FromResult<Reader>(null));

            var command = new BlockReaderCommand(username);
            var handler = new BlockReaderCommandHandler(readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Reader does not exist."));
        }


        [Test]
        public async Task Handle_UserAlreadyBlocked_ReturnsFailureResult()
        {
            //Arrange
            var username = "username";
            var readerId = Guid.NewGuid();

            Reader reader = new Reader
            {
                ReaderId = readerId,
                UserName = username,
                IsBlocked = true
            };



            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(reader);

            var command = new BlockReaderCommand(username);
            var handler = new BlockReaderCommandHandler(readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Reader is already blocked."));
        }
    }
}
