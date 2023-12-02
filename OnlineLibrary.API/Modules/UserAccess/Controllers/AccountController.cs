using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Users.ChangeOwnerUsername;
using OnlineLibary.Modules.Catalog.Application.Users.ChangeReaderUsername;
using OnlineLibrary.API.Modules.UserAccess.Requests;
using OnlineLibrary.Modules.UserAccess.Application.ChangePassword;
using OnlineLibrary.Modules.UserAccess.Application.ChangeUsername;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Application.CreateAPIKey;
using OnlineLibrary.Modules.UserAccess.Application.Logout;
using OnlineLibrary.Modules.UserAccess.Application.ResetPassword;
using OnlineLibrary.Modules.UserAccess.Application.ResetPasswordRequest;
using OnlineLibrary.Modules.UserAccess.Application.TokenRefresh;

namespace OnlineLibrary.API.Modules.UserAccess.Controllers
{
    [Route("api/userAccess")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;
        private readonly ICatalogModule _catalogModule;
        
        public AccountController(IUserAccessModule userAccessModule, ICatalogModule catalogModule)
        {
            _userAccessModule = userAccessModule;
            _catalogModule = catalogModule;  
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            var username = User.Identity.Name;
            
            var result = await _userAccessModule.ExecuteCommandAsync(new ChangePasswordCommand(username, request.OldPassword, request.NewPassword));
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);

        }


        [HttpPost("reset-password-request")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            var result = await _userAccessModule.ExecuteCommandAsync(new ResetPasswordRequestCommand(request.Username));
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);

        }


        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] NewPasswordRequest request)
        {
            var result = await _userAccessModule.ExecuteCommandAsync(new ResetPasswordCommand(request.Code, request.Token, request.Password));
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);

        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefrehTokenAsync([FromBody] RefreshTokenRequest request)
        {
            var token = await _userAccessModule.ExecuteCommandAsync(new RefreshTokenCommand(request.RefreshToken));
            if (token.IsSuccess)
            {
                return Ok(token);
            }

            return BadRequest(token.ErrorMessage);

        }


        [Authorize]
        [HttpPut("change-username")]
        public async Task<IActionResult> ChangeUsernameAsync([FromBody] ChangeUsernameRequest request)
        {
            var username = User.Identity.Name;
           
            var result = await _userAccessModule.ExecuteCommandAsync(new ChangeUsernameCommand(username, request.OldUsername, request.NewUsername));
            List<string> roles = new List<string>();
            foreach (var item in result)
            {
                roles.Add(item.Value.ToString());
            }


            if (roles.Contains("Reader"))
            {
                await _catalogModule.ExecuteCommandAsync(new ChangeReaderUsernameCommand(request.OldUsername, request.NewUsername));
            }

            if (roles.Contains("Owner"))
            {
                await _catalogModule.ExecuteCommandAsync(new ChangeOwnerUsernameCommand(request.OldUsername, request.NewUsername));
            }


            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] LogoutRequest logoutRequest)
        {
            var result = await _userAccessModule.ExecuteCommandAsync(new LogoutCommand(logoutRequest.RefreshToken));
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);

        }

       
    }
}
