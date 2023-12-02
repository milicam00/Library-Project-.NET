using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Users.ReverseAddOwner
{
    public class ReverseAddOwnerCommand : CommandBase<Result>
    {
        public string Username { get; set; }
        public ReverseAddOwnerCommand(string username)
        {
            Username = username;
        }
    }
}
