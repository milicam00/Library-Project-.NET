
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.EditAPIKey
{
    public class EditAPIKeyCommand : CommandBase<Result>
    {
        public EditAPIKeyCommand(string username,string newName)
        {
            Username = username;
            NewName = newName;
        }
        public string Username { get; set; }
        public string NewName { get; set; }
    }
}
