using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.ResetPasswordRequest
{
    public class ResetPasswordRequestCommand : CommandBase<Result>
    {
        public ResetPasswordRequestCommand(string username) 
        {
            Username = username;    
        }
        public string Username { get; set; }
        

    }
}
