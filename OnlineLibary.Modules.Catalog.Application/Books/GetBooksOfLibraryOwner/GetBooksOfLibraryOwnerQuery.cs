using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfLibraryOwner
{
    public class GetBooksOfLibraryOwnerQuery : QueryBase<Result>
    {
        public GetBooksOfLibraryOwnerQuery(string ownerUsername, string libraryName)
        {
            OwnerUsername = ownerUsername;
            LibraryName = libraryName;
        }    
        public string OwnerUsername { get; set; }   
        public string LibraryName { get; set; }
    }
}
