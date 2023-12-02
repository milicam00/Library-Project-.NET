using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.ApproveComment
{
    public class ApproveCommentCommand : CommandBase<Result>
    {
        public ApproveCommentCommand(Guid rentalBookId) 
        {
            RentalBookId = rentalBookId;
        }
        public Guid RentalBookId { get; set; }  
    }
}
