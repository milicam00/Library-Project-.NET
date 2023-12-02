using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace OnlineLibrary.API.Configuration.Authorization
{
    public class AuthorizeRoleFilter : IAsyncAuthorizationFilter
    {
        private readonly string _role;
        public AuthorizeRoleFilter(string role)
        {
            _role = role;
        }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (roleClaim == null || roleClaim.Value != _role)
            {
                context.Result = new ForbidResult();
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
