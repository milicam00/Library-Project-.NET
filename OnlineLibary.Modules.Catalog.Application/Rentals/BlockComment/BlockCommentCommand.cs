using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.BlockComment
{
    public class BlockCommentCommand : CommandBase<Result>
    {
        public BlockCommentCommand(Guid rentalBookId) 
        {
            RentalBookId = rentalBookId;
        }
        public Guid RentalBookId { get; set; }  
    }
}
