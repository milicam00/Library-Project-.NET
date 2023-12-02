using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace OnlineLibary.Modules.Catalog.Application.Books.GetAllBooks
{
    public class GetAllBooksHandler : ICommandHandler<GetAllBooks, PaginationResult<Book>>
    {
       private readonly IBookRepository _bookRepository;

        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<PaginationResult<Book>> Handle(GetAllBooks request, CancellationToken cancellationToken)
        {
            return await _bookRepository.Get(request.PaginationFilter);
        }
    }
}
