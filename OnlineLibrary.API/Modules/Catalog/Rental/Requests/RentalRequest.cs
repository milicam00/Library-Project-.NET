namespace OnlineLibrary.API.Modules.Catalog.Rental.Requests
{
    public class RentalRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> BookIds { get; set; }

    }
}
