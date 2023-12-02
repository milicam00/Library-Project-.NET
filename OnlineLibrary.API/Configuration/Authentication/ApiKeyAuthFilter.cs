using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Modules.UserAccess.Infrastructure;
using System.Security.Claims;

namespace OnlineLibrary.API.Configuration.Authentication
{
    public class ApiKeyAuthFilter : IAsyncAuthorizationFilter
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ApiKeyAuthFilter(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("APIKey", out var apiKeyValues))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult("API key does not exist.") { StatusCode = 401 };
                return;
            }

            string apiKey = apiKeyValues.ToString();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<UserAccessContext>();
                var userApiKey = await dbContext.APIKeys.FirstOrDefaultAsync(x => x.Key == apiKey);
                if (userApiKey == null)
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult("Invalid API key.") { StatusCode = 401 };
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userApiKey.Username)

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "APIKey");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    context.HttpContext.User = claimsPrincipal;
                }
            }
        }
    }
}
