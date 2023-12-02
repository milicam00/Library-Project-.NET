using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.Events;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentalBooks.OwnerRentalBookSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions
{
    [Flags]
    public enum Genres
    {
        None = 0,
        Mystery = 1,
        Fantasy = 2,
        Thriller = 4,
        Horror = 8,
        Biography = 16,
        Science = 32,
        Drama = 64,
        Comedy = 128,
        Travel = 256,
        Romance = 512,
        Poetry = 1024,
        Adventure = 2048
    }

    public class Book : Entity, IAggregateRoot
    {
        public Guid BookId { get;  set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public Genres Genres { get; set; }
        public int PubblicationYear { get; set; }
        public double UserRating { get; set; }
        public int NumberOfCopies { get; set; }
        public bool IsDeleted { get; set; }
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
        public int NumberOfRatings { get; set; }
        public List<RentalBook> RentalBooks { get; set; }
        public List<OwnerRentalBook> OwnerRentalBooks { get; set; } 
   
        public Book()
        {
            BookId = Guid.NewGuid();
            RentalBooks = new List<RentalBook>();
            OwnerRentalBooks = new List<OwnerRentalBook>();
        }

        public Book(string title, string description, string author, int pages, Genres genres, int pubblicationYear, int numCopies, Guid libraryId)
        {
            BookId = Guid.NewGuid();
            Title = title;
            Description = description;
            Author = author;
            Pages = pages;
            Genres = genres;
            PubblicationYear = pubblicationYear;
            NumberOfCopies = numCopies;
            LibraryId = libraryId;
            IsDeleted = false;

            this.AddDomainEvent(new BookCreatedDomainEvent(this.BookId));
        }
        public static Book Create(string title, string description, string author, int pages, Genres genres, int publicationYear, int numCopies, Guid libraryId)
        {
            return new Book(title, description, author, pages, genres, publicationYear, numCopies, libraryId);
        }


        public void EditBook(string title, string description, string author, int pages, int pubb, double userRating, int numOfCopies)
        {
            Title = title;
            Description = description;
            Author = author;
            Pages = pages;
            PubblicationYear = pubb;
            UserRating = userRating;
            NumberOfCopies = numOfCopies;
        }


        public void DeleteBook()
        {
            IsDeleted = true;
        }
    }
}
