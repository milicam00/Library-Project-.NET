namespace OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription
{
    public class RentalResult
    {
        public Guid UserId { get; set; }
        public List<RentalDto> RentalDtos { get; set; }
    }
}
