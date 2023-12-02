using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.CreateRental
{
    public class CreateRentalCommand : CommandBase<Result>
    {
        public CreateRentalCommand(
            Guid userId,
            List<Guid> bookIds
            
            ) 
        {
            UserId = userId;
            BookIds = bookIds;
        }

        public Guid UserId { get; set; }
        public List<Guid> BookIds { get; set; }
        
    }
}
