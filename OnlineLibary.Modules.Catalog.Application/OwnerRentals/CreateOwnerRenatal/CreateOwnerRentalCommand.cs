using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.OwnerRentals.CreateOwnerRenatal
{
    public class CreateOwnerRentalCommand : CommandBase<Result>
    {
        public CreateOwnerRentalCommand(string username, List<Guid> bookIds)
        {
            Username = username;
            BookIds = bookIds;
        }

        public string Username { get; set; }
        public List<Guid> BookIds { get; set; }
    }
}
