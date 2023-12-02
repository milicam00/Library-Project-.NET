using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.ChangePassword
{
    public class ChangePasswordCommand : CommandBase<Result>
    {
        public ChangePasswordCommand(string username, string oldPassword, string newPassword)
        {
            Username = username;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
