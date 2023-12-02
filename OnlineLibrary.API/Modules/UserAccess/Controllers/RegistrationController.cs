using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Users.ReverseAddOwner;
using OnlineLibary.Modules.Catalog.Application.Users.ReverseAddReader;
using OnlineLibrary.API.Modules.UserAccess.Requests;
using OnlineLibrary.Modules.UserAccess.Application.AddOwner;
using OnlineLibrary.Modules.UserAccess.Application.AddReader;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Application.ReverseRegisterReader;


namespace OnlineLibrary.API.Modules.UserAccess.Controllers
{
    [Route("api/userAccess/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;
        private readonly ICatalogModule _catalogModule;

        public RegistrationController(IUserAccessModule userAccessModule, ICatalogModule catalogModule)
        {
            _userAccessModule = userAccessModule;
            _catalogModule = catalogModule;
        }

        [HttpPost("owner-registration")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> OwnerRegistrationAsync([FromBody] RegisterRequest request)
        {
            var firstTransaction = await _userAccessModule.ExecuteCommandAsync(new RegisterOwnerCommand(
                        request.Username,
                        request.Password,
                        request.Email,
                        request.FirstName,
                        request.LastName

                        ));

            if (!firstTransaction.IsSuccess)
            {
                await _catalogModule.ExecuteCommandAsync(new ReverseAddOwnerCommand(request.Username));
            }

            var secondTransaction = await _catalogModule.ExecuteCommandAsync(new OnlineLibary.Modules.Catalog.Application.Users.AddOwner.AddOwnerCommand(
                         request.Username,
                         request.Email,
                         request.FirstName,
                         request.LastName
                         ));

            if (!secondTransaction.IsSuccess)
            {
                await _userAccessModule.ExecuteCommandAsync(new ReverseRegisterUserCommand(request.Username));
            }

            if (firstTransaction.IsSuccess && secondTransaction.IsSuccess)
            {
                return Ok(firstTransaction);
            }

            return BadRequest("Registration failed.");


        }

        [HttpPost("reader-registration")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReaderRegistrationAsync([FromBody] RegisterRequest request)
        {
            var firstTransaction = await _userAccessModule.ExecuteCommandAsync(new RegisterReaderCommand(
                        request.Username,
                        request.Password,
                        request.Email,
                        request.FirstName,
                        request.LastName

                        ));

            if (!firstTransaction.IsSuccess)
            {
                await _catalogModule.ExecuteCommandAsync(new ReverseAddReaderCommand(request.Username));
            }

            var secondTransaction = await _catalogModule.ExecuteCommandAsync(new OnlineLibary.Modules.Catalog.Application.Users.AddReader.AddReaderCommand(
                         request.Username,
                         request.Email,
                         request.FirstName,
                         request.LastName
                         ));

            if (!secondTransaction.IsSuccess)
            {
                await _userAccessModule.ExecuteCommandAsync(new ReverseRegisterUserCommand(request.Username));
            }

            if (firstTransaction.IsSuccess && secondTransaction.IsSuccess)
            {
                return Ok(firstTransaction);
            }

            return BadRequest("Registration failed.");


        }


    }
}
