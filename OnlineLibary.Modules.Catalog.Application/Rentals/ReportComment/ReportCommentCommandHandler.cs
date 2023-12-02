using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.ReportComment
{
    public class ReportCommentCommandHandler : ICommandHandler<ReportCommentCommand, Result>
    {
        private readonly IRentalBooksRepository _rentalBookRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IOwnerRepository _ownerRepository;
        public ReportCommentCommandHandler(IRentalBooksRepository rentalBookRepository, IBookRepository bookRepository, ILibraryRepository libraryRepository,IOwnerRepository ownerRepository)
        {
            _rentalBookRepository = rentalBookRepository;
            _bookRepository = bookRepository;
            _libraryRepository = libraryRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<Result> Handle(ReportCommentCommand request, CancellationToken cancellationToken)
        {
            RentalBook? rentalBook = await _rentalBookRepository.GetByIdAsync(request.RentalBookId);
            if (rentalBook == null)           
                return Result.Failure("This rental does not exist.");
            
            Book book = await _bookRepository.GetByIdAsync(rentalBook.BookId);
            if (book == null)           
                return Result.Failure("This book does not exist.");
            
            Library? library = await _libraryRepository.GetByIdAsync(book.LibraryId);
            if (library == null)          
                return Result.Failure("This library does not exist.");
            Owner owner = await _ownerRepository.GetByUsername(request.OwnerUsername);  
            if(owner==null)
            {
                return Result.Failure("This owner does not exist.");
            }
            if (library.OwnerId != owner.OwnerId)           
                return Result.Failure("Only owner of library can report comment.");
            
            if (rentalBook.TextualComment == null)            
                return Result.Failure("This book rental has not been commented.");
            
            if(rentalBook.IsCommentReported == true)           
                return Result.Failure("This comment is already reported.");
            
            rentalBook.ReportComment();
            _rentalBookRepository.UpdateRentalBook(rentalBook);


            ReportCommentResult result = new ReportCommentResult
            {
                RentalBookId = rentalBook.RentalBookId,
                BookTitle = book.Title,
                Rate = rentalBook.RatedRating,
                TextualComment = rentalBook.TextualComment,
                IsCommentReported = rentalBook.IsCommentReported
            };
            return Result.Success(result);
        }
    }
}
