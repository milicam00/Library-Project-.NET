using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.GetPreviousRentalsReader
{
    public class GetPreviousRentalsReaderQuery : QueryBase<Result>
    {
        public GetPreviousRentalsReaderQuery(string readerUsername,string? title, int pageNumber, int pageSize, string? orderBy) 
        {
            ReaderUsername = readerUsername;
            Title = title;
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderBy;  
        }
        public string ReaderUsername { get; set; }  
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Title { get; set; }
        public string? OrderBy { get; set; }    
    }
}
