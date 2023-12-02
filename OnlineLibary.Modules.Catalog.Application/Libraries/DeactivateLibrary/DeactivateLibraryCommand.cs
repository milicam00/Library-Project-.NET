using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Libraries.DeactivateLibrary
{
    public class DeactivateLibraryCommand : CommandBase<Result>
    {
        

        public DeactivateLibraryCommand( Guid libraryId)
        {
            LibraryId = libraryId;
        }

        public Guid LibraryId { get; set; }
    
    }
}
