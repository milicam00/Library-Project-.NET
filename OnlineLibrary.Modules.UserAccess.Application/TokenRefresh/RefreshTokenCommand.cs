using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.TokenRefresh
{
    public class RefreshTokenCommand : CommandBase<Result>
    {
        public RefreshTokenCommand(string refreshToken)
        { 
            RefreshToken = refreshToken;
        }
        public string RefreshToken { get; set; }
    }
}
