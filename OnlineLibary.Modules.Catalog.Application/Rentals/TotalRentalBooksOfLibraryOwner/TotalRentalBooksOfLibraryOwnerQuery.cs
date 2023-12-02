using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentalBooksOfLibraryOwner
{
    public class TotalRentalBooksOfLibraryOwnerQuery : QueryBase<Result>
    {
        public TotalRentalBooksOfLibraryOwnerQuery(string ownerUsername, Guid libraryId, DateTime startDate, DateTime endDate)
        {
           OwnerUsername = ownerUsername;       
            LibraryId = libraryId;
            StartDate = startDate;
            EndDate = endDate;
        }
        public string OwnerUsername { get; set; }
        public Guid LibraryId { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
    }
}
