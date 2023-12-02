using Microsoft.AspNetCore.Mvc;

namespace OnlineLibrary.API.Configuration.Authorization
{
    public class AuthorizeRoleAttribute : TypeFilterAttribute
    {
        public AuthorizeRoleAttribute(string role) : base(typeof(AuthorizeRoleFilter))
        {
            Arguments = new object[] { role };
        }
    }
}
