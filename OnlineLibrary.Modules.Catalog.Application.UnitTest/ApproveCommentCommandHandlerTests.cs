using Moq;
using OnlineLibary.Modules.Catalog.Application.Rentals.ApproveComment;
using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ApproveCommentCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var rentalBookId = Guid.NewGuid();

            //Act
            var approveCommentCommand = new ApproveCommentCommand(rentalBookId);

            //Assert
            Assert.That(approveCommentCommand.RentalBookId, Is.EqualTo(rentalBookId));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();

            Owner owner = new Owner
            {
                OwnerId = ownerId
            };

            Library library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };

            Book book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };

            var rentalBook = new RentalBook
            {
                RentalBookId = rentalBookId,
                TextualComment = "testComment",
                IsCommentApproved = false,
                BookId = bookId
            };

            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(repo => repo.GetByIdAsync(rentalBookId)).ReturnsAsync(rentalBook);           
            rentalBookRepository.Setup(repo => repo.GetOwner(bookId)).ReturnsAsync(owner);
            OutboxMessage outbox = new OutboxMessage("Email", "Data");
            var outboxRepository = new Mock<IOutboxMessageRepository>();
            outboxRepository.Setup(repo => repo.AddAsync(outbox));

            var command = new ApproveCommentCommand(rentalBookId);
            var handler = new ApproveCommentCommandHandler(rentalBookRepository.Object, outboxRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_RentalBookDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();

            Owner owner = new Owner
            {
                OwnerId = ownerId
            };

            Library library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };

            Book book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };

            var rentalBook = new RentalBook
            {
                RentalBookId = rentalBookId,
                TextualComment = "testComment",
                IsCommentApproved = false,
                BookId = bookId
            };

            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(repo => repo.GetByIdAsync(rentalBookId)).Returns(Task.FromResult<RentalBook>(null));
            rentalBookRepository.Setup(repo => repo.GetOwner(bookId)).ReturnsAsync(owner);

            OutboxMessage outbox = new OutboxMessage("Email", "Data");
            var outboxRepository = new Mock<IOutboxMessageRepository>();
            outboxRepository.Setup(repo => repo.AddAsync(outbox));

            var command = new ApproveCommentCommand(rentalBookId);
            var handler = new ApproveCommentCommandHandler(rentalBookRepository.Object, outboxRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This rental does not exist."));
        }

        [Test]
        public async Task Handle_AlreadyApproved_ReturnsFailureResult()
        {
            //Arrange
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();

            Owner owner = new Owner
            {
                OwnerId = ownerId
            };

            Library library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };

            Book book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };

            var rentalBook = new RentalBook
            {
                RentalBookId = rentalBookId,
                TextualComment = "testComment",
                IsCommentApproved = true,
                BookId = bookId
            };

            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(repo => repo.GetByIdAsync(rentalBookId)).ReturnsAsync(rentalBook);
            rentalBookRepository.Setup(repo => repo.GetOwner(bookId)).ReturnsAsync(owner);

            OutboxMessage outbox = new OutboxMessage("Email", "Data");
            var outboxRepository = new Mock<IOutboxMessageRepository>();
            outboxRepository.Setup(repo => repo.AddAsync(outbox));

            var command = new ApproveCommentCommand(rentalBookId);
            var handler = new ApproveCommentCommandHandler(rentalBookRepository.Object, outboxRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This comment already is approved."));
        }

        [Test]
        public async Task Handle_NotCommented_ReturnsFailureResult()
        {  //Arrange
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();

            Owner owner = new Owner
            {
                OwnerId = ownerId
            };

            Library library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };

            Book book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };

            var rentalBook = new RentalBook
            {
                RentalBookId = rentalBookId,
                TextualComment = null,
                IsCommentApproved = false,
                BookId = bookId
            };

            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(repo => repo.GetByIdAsync(rentalBookId)).ReturnsAsync(rentalBook);
            rentalBookRepository.Setup(repo => repo.GetOwner(bookId)).ReturnsAsync(owner);

            OutboxMessage outbox = new OutboxMessage("Email", "Data");
            var outboxRepository = new Mock<IOutboxMessageRepository>();
            outboxRepository.Setup(repo => repo.AddAsync(outbox));

            var command = new ApproveCommentCommand(rentalBookId);
            var handler = new ApproveCommentCommandHandler(rentalBookRepository.Object, outboxRepository.Object);
            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This rental is not commented."));
        }

    }
}
