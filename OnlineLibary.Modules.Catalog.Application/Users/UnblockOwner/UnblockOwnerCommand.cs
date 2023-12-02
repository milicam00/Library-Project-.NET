using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Users.UnblockOwner
{
    public class UnblockOwnerCommand : CommandBase<Result>
    {
        public UnblockOwnerCommand(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
}
