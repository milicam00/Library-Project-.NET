using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentalsBookOfLibrary
{
    public class TotalRentalBooksOfLibraryQuery : QueryBase<Result>
    {
        public TotalRentalBooksOfLibraryQuery(Guid libraryId, DateTime startDate, DateTime endDate)
        {
            LibraryId = libraryId;
            StartDate = startDate;
            EndDate = endDate;
        }
        public Guid LibraryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
