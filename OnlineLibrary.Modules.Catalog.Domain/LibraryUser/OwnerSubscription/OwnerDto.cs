namespace OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription
{
    public class OwnerDto
    {
        public Guid OwnerId { get;  set; }
        public string UserName { get;  set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LibraryName { get; set; }
    }
}
