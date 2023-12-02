using Moq;
using OnlineLibary.Modules.Catalog.Application.Rentals.ReportComment;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class ReportCommentCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";
            var rentalBookId = Guid.NewGuid();

            //Act
            var reportCommentCommand = new ReportCommentCommand(username, rentalBookId);

            //Assert
            Assert.That(reportCommentCommand.RentalBookId, Is.EqualTo(rentalBookId));
            Assert.That(reportCommentCommand.OwnerUsername, Is.EqualTo(username));
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var username = "username";

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
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

            var book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(repo => repo.GetByIdAsync(rentalBook.BookId)).ReturnsAsync(book);

            var library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo=>repo.GetByUsername(username)).ReturnsAsync(owner);

            var command = new ReportCommentCommand(username, rentalBookId);
            var handler = new ReportCommentCommandHandler(rentalBookRepository.Object, bookRepository.Object, libraryRepository.Object,ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        public async Task Handle_RentalDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var username = "username";

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
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

            var book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(repo => repo.GetByIdAsync(rentalBook.BookId)).ReturnsAsync(book);

            var library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);

            var command = new ReportCommentCommand(username, rentalBookId);
            var handler = new ReportCommentCommandHandler(rentalBookRepository.Object, bookRepository.Object, libraryRepository.Object, ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This rental does not exist."));

        }



        [Test]
        public async Task Handle_CommentAlreadyReported_ReturnsFailureResult()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var username = "username";

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var rentalBook = new RentalBook
            {
                RentalBookId = rentalBookId,
                TextualComment = "testComment",
                IsCommentReported = true,
                BookId = bookId

            };

            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(repo => repo.GetByIdAsync(rentalBookId)).ReturnsAsync(rentalBook);

            var book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(repo => repo.GetByIdAsync(rentalBook.BookId)).ReturnsAsync(book);

            var library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);


            var command = new ReportCommentCommand(username, rentalBookId);
            var handler = new ReportCommentCommandHandler(rentalBookRepository.Object, bookRepository.Object, libraryRepository.Object,ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This comment is already reported."));

        }


        [Test]
        public async Task Handle_NotCommented_ReturnsFailureResult()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var username = "username";

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
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

            var book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(repo => repo.GetByIdAsync(rentalBook.BookId)).ReturnsAsync(book);

            var library = new Library
            {
                LibraryId = libraryId,
                OwnerId = ownerId
            };
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);

            var command = new ReportCommentCommand(username, rentalBookId);
            var handler = new ReportCommentCommandHandler(rentalBookRepository.Object, bookRepository.Object, libraryRepository.Object, ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This book rental has not been commented."));

        }

        [Test]
        public async Task Handle_NotOwnerOfLibrary_ReturnsFailureResult()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var rentalBookId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var libraryId = Guid.NewGuid();
            var username = "username";
            var rentalBook = new RentalBook
            {
                RentalBookId = rentalBookId,
                TextualComment = "testComment",
                IsCommentReported = true,
                BookId = bookId

            };

            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = username
            };
            var rentalBookRepository = new Mock<IRentalBooksRepository>();
            rentalBookRepository.Setup(repo => repo.GetByIdAsync(rentalBookId)).ReturnsAsync(rentalBook);

            var book = new Book
            {
                BookId = bookId,
                LibraryId = libraryId
            };
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(repo => repo.GetByIdAsync(rentalBook.BookId)).ReturnsAsync(book);

            var library = new Library
            {
                LibraryId = libraryId,
                OwnerId = Guid.NewGuid()
            };
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetByIdAsync(book.LibraryId)).ReturnsAsync(library);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(username)).ReturnsAsync(owner);


            var command = new ReportCommentCommand(username, rentalBookId);
            var handler = new ReportCommentCommandHandler(rentalBookRepository.Object, bookRepository.Object, libraryRepository.Object,ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Only owner of library can report comment."));

        }
    }
}
