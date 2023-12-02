using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace OnlineLibary.Modules.Catalog.Application.Books.GetAllBooks
{
    public class GetAllBooks : CommandBase<PaginationResult<Book>>
    {
        public PaginationFilter PaginationFilter { get; set; }
        public GetAllBooks(PaginationFilter paginationFilter)         
        {        
            PaginationFilter = paginationFilter;
        }
    }
}
