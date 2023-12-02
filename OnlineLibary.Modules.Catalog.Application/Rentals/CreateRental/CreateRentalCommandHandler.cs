using Newtonsoft.Json;
using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription;
using System.Text;
using IOutboxMessageRepository = OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription.IOutboxMessageRepository;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.CreateRental
{
    public class CreateRentalCommandHandler : ICommandHandler<CreateRentalCommand, Result>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IOutboxMessageRepository _outboxMessageRepository;
       

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IBookRepository bookRepository, ILibraryRepository libraryRepository, IReaderRepository readerRepository,IOutboxMessageRepository outboxMessageRepository)
        {
            _rentalRepository = rentalRepository;
            _bookRepository = bookRepository;
            _libraryRepository = libraryRepository;
            _readerRepository = readerRepository;
            _outboxMessageRepository = outboxMessageRepository;
           
        }
        public async Task<Result> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {

            var reader = await _readerRepository.GetById(request.UserId);
            if (reader == null)
            {
                return Result.Failure("This reader does not exist");
            }

            var bookIds = request.BookIds;

            var rentedBooks = await _bookRepository.GetByIdsAsync(bookIds);

            var unvailableBooks = rentedBooks.Where(book => book.IsDeleted || book.NumberOfCopies <= 0).ToList();
            if (unvailableBooks.Any())
            {
                var unvailableBookTitles = unvailableBooks.Select(book => book.Title);
                return Result.Failure($"The following books are deleted: {string.Join(", ", unvailableBooks)}.");
            }
            var rentedBookCounts = bookIds.GroupBy(id => id).ToDictionary(group => group.Key, group => group.Count());

            foreach (var bookId in rentedBookCounts.Keys)
            {
                var rentedBook = rentedBooks.SingleOrDefault(book => book.BookId == bookId);
                if (rentedBook == null)
                {
                    return Result.Failure($"Book with ID {bookId} is not selected.");
                }

                var requestedCount = rentedBookCounts[bookId];
                var availableCount = rentedBook.NumberOfCopies;

                if (requestedCount > availableCount)
                {
                    return Result.Failure($"Not enough copies available for book with ID {bookId}.");
                }
            }
            var rental = Rental.Create(request.UserId, bookIds);

            await _rentalRepository.AddAsync(rental);

            foreach (var rent in rentedBooks)
            {
                rent.NumberOfCopies--;
            }
            _bookRepository.UpdateBooks(rentedBooks);

            var bookInfo = rentedBooks.Select(book => new
            {
                Title = book.Title,
                Author = book.Author,
                LibraryId = book.LibraryId
            }).ToList();

            var libraryIds = bookInfo.Select(info => info.LibraryId).Distinct().ToList();
            var libraries = await _libraryRepository.GetByIdsAsync(libraryIds);


            var body = new StringBuilder();

            List<RentalDto> rentalDtos = new List<RentalDto>();
            foreach (var info in bookInfo)
            {
                var library = libraries.FirstOrDefault(lib => lib.LibraryId == info.LibraryId);
                body.AppendLine($"-Title: {info.Title}");
                body.AppendLine($"-Author: {info.Author}");
                body.AppendLine($"-Library: {library?.LibraryName ?? "N/A"}\n");
                var rentalDto = new RentalDto
                {
                    BookTitle = info.Title,
                    BookAuthor = info.Author,
                    Library = library.LibraryName
                };
                rentalDtos.Add(rentalDto);

            }

            body.AppendLine($"-Rental Date: {DateTime.Now}\n\n");
            body.AppendLine("Enjoy!");
            RentalResult rentalResult = new RentalResult
            {
                UserId = request.UserId,
                RentalDtos = rentalDtos
            };
            
            string data = JsonConvert.SerializeObject(new
            {
                Recipient = reader.Email,
                Subject = "Book rental",
                Body = "You have successfully rented the following books:\n\n" + body.ToString()
            });

            var outboxMessage = new OutboxMessage("Email", data);
            await _outboxMessageRepository.AddAsync(outboxMessage);
          
          

            return Result.Success(rentalResult);

        }
    }
}
