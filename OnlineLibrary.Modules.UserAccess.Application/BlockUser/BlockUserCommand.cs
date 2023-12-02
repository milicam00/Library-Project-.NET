using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.BlockUser
{
    public class BlockUserCommand : CommandBase<Result>
    {
        public BlockUserCommand(string username)
        {
           Username = username;
        }
       
        public string Username { get; set; }
    }

    
}
