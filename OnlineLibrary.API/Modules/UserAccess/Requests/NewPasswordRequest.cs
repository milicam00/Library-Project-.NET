namespace OnlineLibrary.API.Modules.UserAccess.Requests
{
    public class NewPasswordRequest
    {
        public string Password { get; set; }
        public string Token { get; set; }   
        public int Code { get; set; }
    }
}
