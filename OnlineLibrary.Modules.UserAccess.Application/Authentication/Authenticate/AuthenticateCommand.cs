using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticateCommand : CommandBase<Result>
    {
        public AuthenticateCommand(string username,string password)
        {
            UserName = username;
            Password = password;
        }
        public string UserName { get; }
        public string Password { get; }
    }
}
