using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.UnblockUser
{
    public class UnblockUserCommand : CommandBase<Result>
    {
        public UnblockUserCommand(string username) 
        {
            Username = username;
        }
        public string Username { get; set; }
    }
}
