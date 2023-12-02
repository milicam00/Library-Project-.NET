using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.Logout
{
    public class LogoutCommand : CommandBase<Result>
    {
        public LogoutCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
        public string RefreshToken { get; set; }
    }
}
 