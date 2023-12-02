using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfOwner
{
    public class GetBooksOfLibraryOwnerQuery : QueryBase<Result>
    {
        public GetBooksOfLibraryOwnerQuery(string ownerUsername)
        {
            OwnerUsername = ownerUsername;
        }
        public string OwnerUsername { get; set; }
    }
}
