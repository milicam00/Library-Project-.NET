using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        void UpdateBook(Book book);
        void UpdateBooks(List<Book> books);
        Task<Book> GetByIdAsync(Guid bookId);
        Task<PaginationResult<Book>> Get(PaginationFilter filter);
        Task<List<Book>> GetByIdsAsync(List<Guid> bookIds);
        Task AddBooksAsync(List<Book> books);   
    }
}
