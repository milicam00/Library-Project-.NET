using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.ChangeUsername
{
    public class ChangeUsernameCommand : CommandBase<List<UserRole>>
    {
        public ChangeUsernameCommand(string userName,string oldUsername,string newUsername)
        {
            UserName = userName;
            OldUsername = oldUsername;
            NewUsername = newUsername;
        }
        public string UserName { get; set; }
        public string OldUsername { get; set; }
        public string NewUsername { get; set; }  
    }
}
