using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveMostPopularOwners
{
    public class TopTenMostPopularOwnersQuery : QueryBase<Result>
    {
        public TopTenMostPopularOwnersQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
