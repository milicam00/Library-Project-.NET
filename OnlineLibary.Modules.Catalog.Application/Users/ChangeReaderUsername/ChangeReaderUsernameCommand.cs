using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Users.ChangeReaderUsername
{
    public class ChangeReaderUsernameCommand : CommandBase<Result>
    {
        public ChangeReaderUsernameCommand(string oldUsername, string newUsername) 
        {
            OldUsername = oldUsername;
            NewUsername = newUsername;
        }
        public string OldUsername { get; set; }
        public string NewUsername { get; set; }
    }
}
