using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.ReverseRegisterReader
{
    public class ReverseRegisterUserCommand : CommandBase<Result>
    {
        public string Username { get; set; }
        public ReverseRegisterUserCommand(string username) 
        {
            Username = username;
        }
    }
}
