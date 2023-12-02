using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace OnlineLibary.Modules.Catalog.Application.Books.AddBooks
{
    public class AddBooksCommand : CommandBase<Result>
    {
        public AddBooksCommand(List<CreateBookDto> bookList, string ownerUsername)
        {
            BookList = bookList;
            OwnerUsername = ownerUsername;
        }
        public string OwnerUsername { get; set; }
        public List<CreateBookDto> BookList { get; set; }
    }
}
