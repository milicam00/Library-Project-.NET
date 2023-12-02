using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.DeleteAPIKey
{
    public class DeleteApiKeyCommand : CommandBase<Result>
    {
        public DeleteApiKeyCommand(string username)
        {
            Username = username;
        }

        public string Username { get; set; }    
    }
}
