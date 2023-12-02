using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveRatedBooksOwner
{
    public class TopFiveRatedBooksOwnerQuery : QueryBase<Result>
    {
        public TopFiveRatedBooksOwnerQuery(string ownerUsername, DateTime startDate, DateTime endDate)
        {
            OwnerUsername = ownerUsername;
            StartDate = startDate;
            EndDate = endDate;
        }
        public string OwnerUsername { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
