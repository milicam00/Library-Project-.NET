using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.GetRentalBooks
{
    public class GetRentalBooksQuery : QueryBase<List<Guid>>
    {
        public GetRentalBooksQuery(Guid rentalId) 
        { 
            RentalId = rentalId;
        }
        public Guid RentalId { get; set; }
    }
}
