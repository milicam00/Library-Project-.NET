using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription
{
    public class Rental : Entity, IAggregateRoot
    {
        public Guid RentalId { get; set; }
        public Reader Reader { get; set; }
        public Guid ReaderId { get; set; }
        public List<RentalBook> RentalBooks { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? Returned { get; set; }
        public Rental()
        {
            RentalId = Guid.NewGuid();
            RentalBooks = new List<RentalBook>();
        }


        public Rental(Guid readerId, List<Guid> bookId)
        {
            RentalId = Guid.NewGuid();
            ReaderId = readerId;
            RentalBooks = new List<RentalBook>();

            foreach (var id in bookId)
            {
                RentalBooks.Add(new RentalBook(id));
            }
            RentalDate = DateTime.UtcNow;
            Returned = false;
        }
        public static Rental Create(Guid readerId,List<Guid> bookId )
        {
            return new Rental(readerId, bookId);
        }

        
        public void ReturnBooks()
        {
            ReturnDate = DateTime.UtcNow;
            Returned = true;
        }
        
    }
}
