using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibrary.Modules.UserAccess.Application.GetAllAPIKeys
{
    public class GetAllAPIKeysQuery : QueryBase<Result>
    {
        public GetAllAPIKeysQuery(int pageNumber, int pageSize,string? name)
        {
            PageNumber = pageNumber;    
            PageSize = pageSize;
            Name = name;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }   
        public string? Name { get; set; }
    }
}
