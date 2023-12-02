using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions
{
    public class BookId : TypedIdValueBase
    {
        public BookId(Guid value) : base(value)
        {
        }
    }
}
