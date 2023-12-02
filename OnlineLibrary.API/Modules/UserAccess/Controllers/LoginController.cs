using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.API.Modules.UserAccess.Requests;
using OnlineLibrary.Modules.UserAccess.Application.Authentication.Authenticate;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;


namespace OnlineLibrary.API.Modules.UserAccess.Controllers
{
    [Route("api/userAccess/authenticate")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public LoginController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var token = await _userAccessModule.ExecuteCommandAsync(new AuthenticateCommand(request.UserName, request.Password));
            if (token.IsSuccess)
            {
                return Ok(new { token });
            }

            return BadRequest(token.ErrorMessage);

        }

    }
}
