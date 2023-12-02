using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.DeleteBook
{
    public class DeleteBookCommand : CommandBase<Result>
    {
        public DeleteBookCommand(string ownerUsername, Guid bookId)
        {
            BookId = bookId;
            OwnerUsername = ownerUsername;
        }
        public Guid BookId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
