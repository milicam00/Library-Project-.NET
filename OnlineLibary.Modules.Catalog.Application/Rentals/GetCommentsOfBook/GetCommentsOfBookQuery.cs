using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.GetCommentsOfBook
{
    public class GetCommentsOfBookQuery : QueryBase<Result>
    {
        public GetCommentsOfBookQuery(Guid bookId,int pageNumber,int pageSize,string? orderBy)
        {
            BookId = bookId;
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderBy;
            
        } 
        public Guid BookId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; } 
    }
}
