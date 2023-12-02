using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.ReverseAddOwner;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ReverseAddOwnerCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";  

            //Act
            var command = new ReverseAddOwnerCommand(username); 

            //Assert
            Assert.That(command.Username, Is.EqualTo(username));                    
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var username = "username";
            Owner owner = new Owner
            {
                UserName = username
            };

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);

            var command = new ReverseAddOwnerCommand(username);
            var handler = new ReverseAddOwnerCommandHandler(ownerRepository.Object);
        
            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_UserDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var username = "username";

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).Returns(Task.FromResult<Owner>(null));

            var command = new ReverseAddOwnerCommand(username);
            var handler = new ReverseAddOwnerCommandHandler(ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));
        }
    }
}
