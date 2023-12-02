namespace OnlineLibrary.API.Modules.UserAccess.Requests
{
    public class ChangeUsernameRequest
    {       
        public string OldUsername { get; set; }
        public string NewUsername { get; set; }
    }
}
