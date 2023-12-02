using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.ReverseAddReader;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ReverseAddReaderCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";

            //Act
            var command = new ReverseAddReaderCommand(username);

            //Assert
            Assert.That(command.Username, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var username = "username";
            Reader reader = new Reader
            {
                UserName = username
            };

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(reader);

            var command = new ReverseAddReaderCommand(username);
            var handler = new ReverseAddReaderCommandHandler(readerRepository.Object);

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
            readerRepository.Setup(repo => repo.GetByUsername(username)).Returns(Task.FromResult<Reader>(null));

            var command = new ReverseAddReaderCommand(username);
            var handler = new ReverseAddReaderCommandHandler(readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));
        }
    }
}
