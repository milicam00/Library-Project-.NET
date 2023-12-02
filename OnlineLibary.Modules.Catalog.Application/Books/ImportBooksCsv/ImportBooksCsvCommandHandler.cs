using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Application.ICsvGeneration;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using BookDto = OnlineLibrary.BuildingBlocks.Domain.Results.BookDto;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBooks
{
    public class ImportBooksCsvCommandHandler : ICommandHandler<ImportBooksCsvCommand, Result>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICsvGenerationService _csvGenerationService;
        private readonly ILibraryRepository _libraryRepository;
        public ImportBooksCsvCommandHandler(IBookRepository bookRepository, ICsvGenerationService csvGenerationService, ILibraryRepository libraryRepository,IOwnerRepository ownerRepository)
        {
            _bookRepository = bookRepository;
            _csvGenerationService = csvGenerationService;
            _libraryRepository = libraryRepository;
            _ownerRepository = ownerRepository;
        }      
        public async Task<Result> Handle(ImportBooksCsvCommand request, CancellationToken cancellationToken)
        {
            Owner owner = await _ownerRepository.GetByUsername(request.Username);
            if (owner == null)
            {
                return Result.Failure("This owner does not exist.");
            }
            List<Book> books = new List<Book>();
            List<Guid> libraryIds = await _libraryRepository.GetLibraryIdsByOwnerId(owner.OwnerId);
            List<BookDto> importedBooks = await _csvGenerationService.DeserializeBooksFromCsv(request.FileStream);
            foreach (var bookDto in importedBooks)
            {
                if (libraryIds.Contains(bookDto.LibraryId))
                {
                    var book = Book.Create(
                        bookDto.Title,
                        bookDto.Description,
                        bookDto.Author,
                        bookDto.Pages,
                        (Genres)bookDto.Genres,
                        bookDto.PubblicationYear,
                        bookDto.NumberOfCopies,
                        bookDto.LibraryId
                        );

                    books.Add(book);
                }
                else
                {
                    return Result.Failure("Only owner of library can add book.");
                }
            }
            await _bookRepository.AddBooksAsync(books);
            return Result.Success(books);   
        }
    }
}
