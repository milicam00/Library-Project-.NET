using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.Events;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription
{
    public class Library : Entity, IAggregateRoot
    {
        public Guid LibraryId { get; set; }
        public string LibraryName { get; set; }
        public bool IsActive { get; set; }
        public List<Book> Books { get; set; }
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }

        public Library()
        {
            LibraryId = Guid.NewGuid();
        }

        public Library(string libraryName, bool isActive, Guid ownerId)
        {
            LibraryId = Guid.NewGuid();
            LibraryName = libraryName;
            IsActive = isActive;
            OwnerId = ownerId;

            this.AddDomainEvent(new LibraryCreatedDomainEvent(this.LibraryId));
        }

        public static Library Create(string libraryName, bool isActive, Guid ownerId)
        {
            return new Library(libraryName, isActive, ownerId);
        }

        public void EditActivate(bool isActive)
        {
            IsActive = isActive;
        }
        public void ActivateLibrary()
        {
            IsActive = true;
        }
        public void DeactivateLibrary()
        {
            IsActive = false;
        }
    }
}
