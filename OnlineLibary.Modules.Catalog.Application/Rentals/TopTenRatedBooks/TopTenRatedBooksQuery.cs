using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveRatedBooks
{
    public class TopTenRatedBooksQuery : QueryBase<Result>
    {
        public TopTenRatedBooksQuery(DateTime startDate,DateTime endDate)
        { 
            StartDate = startDate;
            EndDate = endDate;
        }
       
        public DateTime StartDate { get; set; }    
        public DateTime EndDate { get; set;}
    }
}
