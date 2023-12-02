using Moq;
using OnlineLibary.Modules.Catalog.Application.Libraries.DeactivateLibrary;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class DeactivateLibraryCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var libraryId = Guid.NewGuid();

            //Act
            var deactivateLibraryCommand = new DeactivateLibraryCommand(libraryId);

            //Assert
            Assert.That(deactivateLibraryCommand.LibraryId, Is.EqualTo(libraryId));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange 
            var libraryRepository = new Mock<ILibraryRepository>();
            var libraryId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var deactivateLibraryCommand = new DeactivateLibraryCommand(libraryId);
            var library = new Library("LibraryName", true, ownerId);
            libraryRepository.Setup(repo=>repo.GetByIdAsync(libraryId)).ReturnsAsync(library);
            var handler = new DeactivateLibraryCommandHandler(libraryRepository.Object);

            //Act
            var result = await handler.Handle(deactivateLibraryCommand, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);

        }

        [Test]
        public async Task Handle_NonExistentLibrary_ReturnsFailure()
        {
            // Arrange
            Guid libraryId = Guid.NewGuid();
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(r => r.GetByIdAsync(libraryId)).ReturnsAsync((Library)null);

            var handler = new DeactivateLibraryCommandHandler(libraryRepository.Object);
            var command = new DeactivateLibraryCommand(libraryId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This library does not exist."));
        }

        [Test]
        public async Task Handle_AlreadyDeactivatedLibrary_ReturnsFailure()
        {
            // Arrange
            Guid libraryId = Guid.NewGuid();
            var libraryRepository = new Mock<ILibraryRepository>();
            var ownerId = Guid.NewGuid();
            var library = new Library
            {
                LibraryId = libraryId,
                LibraryName = "Library Name",
                IsActive = false,
                OwnerId = ownerId
            };

            libraryRepository.Setup(r => r.GetByIdAsync(libraryId)).ReturnsAsync(library);

            var handler = new DeactivateLibraryCommandHandler(libraryRepository.Object);
            var command = new DeactivateLibraryCommand(libraryId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Library is already deactivated."));
        }
    }
}
