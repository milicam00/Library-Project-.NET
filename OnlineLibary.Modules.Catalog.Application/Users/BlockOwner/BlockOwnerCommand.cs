using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Users.BlockOwner
{
    public class BlockOwnerCommand : CommandBase<Result>
    {
        public BlockOwnerCommand(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
}
