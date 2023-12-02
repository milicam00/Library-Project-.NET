using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveRatedBooksOfLibrary
{
    public class TopFiveRatedBooksOfLibraryQuery : QueryBase<Result>
    {
        public TopFiveRatedBooksOfLibraryQuery(Guid libraryId, DateTime startDate, DateTime endDate)
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
