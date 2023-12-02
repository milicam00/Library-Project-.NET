using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.ChangeOwnerUsername;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ChangeOwnerUsernameCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Assert
            var oldUsername = "oldUsername";
            var newUsername = "newUsername";

            //Act
            var command = new ChangeOwnerUsernameCommand(oldUsername, newUsername);

            //Assert
            Assert.That(command.OldUsername, Is.EqualTo(oldUsername));
            Assert.That(command.NewUsername, Is.EqualTo(newUsername));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var oldUsername = "oldUsername";
            var newUsername = "newUsername";

            Owner owner = new Owner
            {
                UserName = oldUsername,
            };
                
            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(oldUsername)).ReturnsAsync(owner);
            ownerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Owner>(null));

            var readerRepository =  new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Reader>(null));

            var command = new ChangeOwnerUsernameCommand(oldUsername, newUsername);
            var handler = new ChangeOwnerUsernameCommandHandler(ownerRepository.Object, readerRepository.Object);

            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        public async Task Handle_OwnerDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var oldUsername = "oldUsername";
            var newUsername = "newUsername";

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(oldUsername)).Returns(Task.FromResult<Owner>(null));
            ownerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Owner>(null));

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Reader>(null));

            var command = new ChangeOwnerUsernameCommand(oldUsername, newUsername);
            var handler = new ChangeOwnerUsernameCommandHandler(ownerRepository.Object, readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Owner does  not exist."));
        }


        [Test]
        public async Task Handle_UserWithSameUsername_ReturnsFailureResult()
        {
            //Arrange
            var oldUsername = "oldUsername";
            var newUsername = "newUsername";

            Owner owner = new Owner
            {
                UserName = oldUsername,
            };

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(oldUsername)).ReturnsAsync(owner);
            ownerRepository.Setup(repo => repo.GetByUsername(newUsername)).ReturnsAsync(owner);

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(repo => repo.GetByUsername(newUsername)).Returns(Task.FromResult<Reader>(null));

            var command = new ChangeOwnerUsernameCommand(oldUsername, newUsername);
            var handler = new ChangeOwnerUsernameCommandHandler(ownerRepository.Object, readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Owner with this username already exist."));
        }
    }
}
