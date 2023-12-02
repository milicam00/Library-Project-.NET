using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Application.ICsvGeneration;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.BuildingBlocks.Domain.Results;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBooksInLibraryCsv
{
    public class ImportBooksInLibraryCsvCommandHandler : ICommandHandler<ImportBooksInLibraryCsvCommand, Result>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICsvGenerationService _csvGenerationService;
        private readonly ILibraryRepository _libraryRepository; 
        public ImportBooksInLibraryCsvCommandHandler(IBookRepository bookRepository,IOwnerRepository ownerRepository,ICsvGenerationService csvGenerationService,ILibraryRepository libraryRepository)
        {
            _bookRepository = bookRepository;
            _ownerRepository = ownerRepository;
            _csvGenerationService = csvGenerationService;
            _libraryRepository = libraryRepository;
        }
        public async Task<Result> Handle(ImportBooksInLibraryCsvCommand request, CancellationToken cancellationToken)
        {
            Owner owner = await _ownerRepository.GetByUsername(request.UserName);
            if (owner == null)
            {
                return Result.Failure("This owner does not exist.");
            }

            List<Guid> libraryIds = await _libraryRepository.GetLibraryIdsByOwnerId(owner.OwnerId);
            if(!libraryIds.Contains(request.LibraryId))
            {
                return Result.Failure("Only owner of library can add book.");
            }           

            List<Book> books = new List<Book>();
            List<ImportedBookForOneLibraryDto> importedBooks = await _csvGenerationService.DeserializeBooksFromCsvForOneLibrary(request.FileStream);
            foreach(var bookDto in importedBooks)
            {
                var book = Book.Create(
                       bookDto.Title,
                       bookDto.Description,
                       bookDto.Author,
                       bookDto.Pages,
                       (Genres)bookDto.Genres,
                       bookDto.PubblicationYear,
                       bookDto.NumberOfCopies,
                       request.LibraryId
                       );
                books.Add( book );
            }
            await _bookRepository.AddBooksAsync(books);
            return Result.Success(books);
        }
    }
}
