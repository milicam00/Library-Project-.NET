using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.CreateAPIKey
{
    public class CreateAPIKeyCommand : CommandBase<Result>
    {
        public CreateAPIKeyCommand(string username, string name)
        {
            Username = username;        
            KeyName = name; 
        }
        public string Username { get; set; }
        public string KeyName { get; set; }     
    }
}
