using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.GetReportedComments
{
    public class GetReportedCommentsQuery : QueryBase<Result>
    {
        public GetReportedCommentsQuery(int pageNumber, int pageSize, string? orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderBy;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }    
    }
}
