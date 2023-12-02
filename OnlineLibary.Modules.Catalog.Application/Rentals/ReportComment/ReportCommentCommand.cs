using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.ReportComment
{
    public class ReportCommentCommand : CommandBase<Result>
    {
        public ReportCommentCommand(string ownerUsername,Guid rentalBookId)
        {
            OwnerUsername = ownerUsername;  
            RentalBookId = rentalBookId;
        }
        public string OwnerUsername { get; set; }   
        public Guid RentalBookId { get; set; }  
    }
}
