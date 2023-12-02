using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.BlockOwner;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class BlockOwnerCommandHandlerTests
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
            var ownerId = Guid.NewGuid();

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };

            Library library1 = new Library
            {
                OwnerId = ownerId
            };
            Library library2 = new Library
            {
                OwnerId = ownerId
            };
            List<Library> libraries = new List<Library>();           
            libraries.Add(library1);
            libraries.Add(library2);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);    

            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByOwnerId(ownerId)).ReturnsAsync(libraries);

            var command = new BlockOwnerCommand(username);
            var handler = new BlockOwnerCommandHandler(ownerRepository.Object,libraryRepository.Object);

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
            var ownerId = Guid.NewGuid();

            
            Library library1 = new Library
            {
                OwnerId = ownerId
            };
            Library library2 = new Library
            {
                OwnerId = ownerId
            };
            List<Library> libraries = new List<Library>();
            libraries.Add(library1);
            libraries.Add(library2);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).Returns(Task.FromResult<Owner>(null));

            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByOwnerId(ownerId)).ReturnsAsync(libraries);

            var command = new BlockOwnerCommand(username);
            var handler = new BlockOwnerCommandHandler(ownerRepository.Object, libraryRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User does not exist."));
        }


        [Test]
        public async Task Handle_UserAlreadyBlocked_ReturnsFailureResult()
        {
            //Arrange
            var username = "username";
            var ownerId = Guid.NewGuid();

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username,
                IsBlocked = true
            };

            Library library1 = new Library
            {
                OwnerId = ownerId
            };
            Library library2 = new Library
            {
                OwnerId = ownerId
            };
            List<Library> libraries = new List<Library>();
            libraries.Add(library1);
            libraries.Add(library2);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);

            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByOwnerId(ownerId)).ReturnsAsync(libraries);

            var command = new BlockOwnerCommand(username);
            var handler = new BlockOwnerCommandHandler(ownerRepository.Object, libraryRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User is already blocked."));
        }
    }
}
