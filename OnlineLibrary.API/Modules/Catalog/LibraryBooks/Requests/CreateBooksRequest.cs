using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace OnlineLibrary.API.Modules.Catalog.LibraryBooks.Requests
{
    public class CreateBooksRequest
    {
        public List<CreateBookDto> Books { get; set; }
    }
}
