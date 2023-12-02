using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentals.OwnerRentalSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.OwnerRentalBooks.OwnerRentalBookSubscription
{
    public class OwnerRentalBook : Entity, IAggregateRoot
    {
        public Guid OwnerRentalBookId { get; set; }
        public Guid OwnerRentalId { get; set; }
        public Guid BookId { get; set; }
        public OwnerRental OwnerRental { get; set; }
        public Book Book { get; set; }
        public OwnerRentalBook()
        {
            OwnerRentalBookId = Guid.NewGuid();
        }
        
        public OwnerRentalBook(Guid bookId)
        {
            BookId = bookId;
        }

    }
}
