namespace OnlineLibrary.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticationResult
    {
        public AuthenticationResult(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
