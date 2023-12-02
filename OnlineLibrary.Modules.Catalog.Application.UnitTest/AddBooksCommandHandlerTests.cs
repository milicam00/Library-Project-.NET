using Moq;
using OnlineLibary.Modules.Catalog.Application.Books.AddBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class AddBooksCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            //Arrange
            var ownerUsername = "username";
            var ownerId = Guid.NewGuid();
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = ownerUsername
            };
            List<Guid> libraryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<CreateBookDto> createBookDtos = new List<CreateBookDto>();

            CreateBookDto bookDto1 = new CreateBookDto
            {
                Title = "title1",
                Description = "description",
                Author = "author",
                Pages = 100,
                Genres = Genres.Science,
                PubblicationYear = 2020,
                NumberOfCopies = 100,
                LibraryId = libraryIds[0]
            };
            CreateBookDto bookDto2 = new CreateBookDto
            {
                Title = "title2",
                Description = "description",
                Author = "author",
                Pages = 100,
                Genres = Genres.Romance,
                PubblicationYear = 2020,
                NumberOfCopies = 100,
                LibraryId = libraryIds[1]
            };
            createBookDtos.Add(bookDto1);
            createBookDtos.Add(bookDto2);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(ownerUsername)).ReturnsAsync(owner);

            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetLibraryIdsByOwnerId(ownerId)).ReturnsAsync(libraryIds);

            var command = new AddBooksCommand(createBookDtos, ownerUsername);
            var handler = new AddBooksCommandHandler(bookRepository.Object, ownerRepository.Object, libraryRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        public async Task Handle_OwnerDoesNotExist_ReturnsFailureResult()
        {
            //Arrange
            var ownerUsername = "username";
            var ownerId = Guid.NewGuid();
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = ownerUsername
            };
            List<Guid> libraryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<CreateBookDto> createBookDtos = new List<CreateBookDto>();

            CreateBookDto bookDto1 = new CreateBookDto
            {
                Title = "title1",
                Description = "description",
                Author = "author",
                Pages = 100,
                Genres = Genres.Science,
                PubblicationYear = 2020,
                NumberOfCopies = 100,
                LibraryId = libraryIds[0]
            };
            CreateBookDto bookDto2 = new CreateBookDto
            {
                Title = "title2",
                Description = "description",
                Author = "author",
                Pages = 100,
                Genres = Genres.Romance,
                PubblicationYear = 2020,
                NumberOfCopies = 100,
                LibraryId = libraryIds[1]
            };
            createBookDtos.Add(bookDto1);
            createBookDtos.Add(bookDto2);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(ownerUsername)).Returns(Task.FromResult<Owner>(null));

            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetLibraryIdsByOwnerId(ownerId)).ReturnsAsync(libraryIds);

            var command = new AddBooksCommand(createBookDtos, ownerUsername);
            var handler = new AddBooksCommandHandler(bookRepository.Object, ownerRepository.Object, libraryRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("This owner does not exist."));
        }

        [Test]
        public async Task Handle_OnlyOwnerOfLibraryCanAddBooks_ReturnsFailureResult()
        {
            //Arrange
            var ownerUsername = "username";
            var ownerId = Guid.NewGuid();
            Owner owner = new Owner
            {
                OwnerId = ownerId,
                UserName = ownerUsername
            };
            List<Guid> libraryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<CreateBookDto> createBookDtos = new List<CreateBookDto>();

            CreateBookDto bookDto1 = new CreateBookDto
            {
                Title = "title1",
                Description = "description",
                Author = "author",
                Pages = 100,
                Genres = Genres.Science,
                PubblicationYear = 2020,
                NumberOfCopies = 100,
                LibraryId = Guid.NewGuid()
            };
            CreateBookDto bookDto2 = new CreateBookDto
            {
                Title = "title2",
                Description = "description",
                Author = "author",
                Pages = 100,
                Genres = Genres.Romance,
                PubblicationYear = 2020,
                NumberOfCopies = 100,
                LibraryId = Guid.NewGuid()
            };
            createBookDtos.Add(bookDto1);
            createBookDtos.Add(bookDto2);

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(repo => repo.GetByUsername(ownerUsername)).ReturnsAsync(owner);

            var bookRepository = new Mock<IBookRepository>();
            var libraryRepository = new Mock<ILibraryRepository>();
            libraryRepository.Setup(repo => repo.GetLibraryIdsByOwnerId(ownerId)).ReturnsAsync(libraryIds);

            var command = new AddBooksCommand(createBookDtos, ownerUsername);
            var handler = new AddBooksCommandHandler(bookRepository.Object, ownerRepository.Object, libraryRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("Only owner of library can add book."));
        }
    }
}
