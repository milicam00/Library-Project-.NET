using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.UnblockReader;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class UnblockReaderCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";

            //Act
            var command = new UnblockReaderCommand(username);

            //Assert
            Assert.That(command.UserName, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var username = "username";
            Reader reader = new Reader
            {
                UserName = username,
                IsBlocked = true
            };

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(x=>x.GetByUsername(username)).ReturnsAsync(reader);

            var command = new UnblockReaderCommand(username);   
            var handler = new UnblockReaderCommandHandler(readerRepository.Object);

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

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(x => x.GetByUsername(username)).Returns(Task.FromResult<Reader>(null));

            var command = new UnblockReaderCommand(username);
            var handler = new UnblockReaderCommandHandler(readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));
        }

        [Test]
        public async Task Handle_UserAlreadyUnblocked_ReturnsFailureResult()
        {
            //Arrange
            var username = "username";
            Reader reader = new Reader
            {
                UserName = username,
                IsBlocked = false
            };

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(x => x.GetByUsername(username)).ReturnsAsync(reader);

            var command = new UnblockReaderCommand(username);
            var handler = new UnblockReaderCommandHandler(readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User is already unblocked."));
        }

    }
    
}
