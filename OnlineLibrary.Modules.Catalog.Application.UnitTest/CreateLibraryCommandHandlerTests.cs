using Moq;
using OnlineLibary.Modules.Catalog.Application.Libraries.CreateLibrary;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class CreateLibraryCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";
            var libraryName = "LibraryName";
            var isActive = true;

            //Act
            var addLibraryCommand = new CreateLibraryCommand(libraryName, isActive, username);

            //Assert
            Assert.That(addLibraryCommand.LibraryName, Is.EqualTo(libraryName));
            Assert.That(addLibraryCommand.IsActive, Is.EqualTo(isActive));
            Assert.That(addLibraryCommand.Username, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            // Arrange
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var username = "username";
            var createLibraryCommand = new CreateLibraryCommand("LibraryName", true, username);
            Owner owner = new Owner();
            libraryRepository.Setup(repo => repo.GetByName(createLibraryCommand.LibraryName)).ReturnsAsync((Library)null);
            libraryRepository.Setup(repo => repo.AddAsync(It.IsAny<Library>())).Returns(Task.CompletedTask);
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);
            var handler = new CreateLibraryCommandHandler(libraryRepository.Object,ownerRepository.Object);

            // Act
            var result = await handler.Handle(createLibraryCommand, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_DuplicateLibraryName_ReturnsFailureResult()
        {
            // Arrange
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerRepository = new Mock<IOwnerRepository>();
            var username = "username";
            var ownerId = Guid.NewGuid();
            var libraryName = "LibraryName";
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };

            var createLibraryCommand = new CreateLibraryCommand(libraryName, true, username);
            var existingLibrary = new Library(libraryName, true, ownerId);

            libraryRepository.Setup(repo => repo.GetByName(createLibraryCommand.LibraryName)).ReturnsAsync(existingLibrary);
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);
            var handler = new CreateLibraryCommandHandler(libraryRepository.Object,ownerRepository.Object);

            // Act
            var result = await handler.Handle(createLibraryCommand, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Library with same name already exist."));
        }
    }


}
