using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Users.BlockReader;
using OnlineLibary.Modules.Catalog.Application.Users.UnblockReader;
using OnlineLibrary.Modules.UserAccess.Application.BlockUser;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Application.UnblockUser;

namespace OnlineLibrary.API.Modules.Catalog.Readers.Controllers
{
    [Route("api/readers")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
        private readonly IUserAccessModule _userAccessModule;
        public ReaderController(ICatalogModule catalogModule, IUserAccessModule userAccessModule)
        {
            _catalogModule = catalogModule;
            _userAccessModule = userAccessModule;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{username}/block-reader")]
        public async Task<IActionResult> BlockUserAsync([FromRoute] string username)
        {
            var firstTransaction = await _catalogModule.ExecuteCommandAsync(new BlockReaderCommand(username));
            if (!firstTransaction.IsSuccess)
            {
                await _userAccessModule.ExecuteCommandAsync(new UnblockUserCommand(username));
            }

            var secondTransaction = await _userAccessModule.ExecuteCommandAsync(new BlockUserCommand(username));
            if (!secondTransaction.IsSuccess)
            {
                await _catalogModule.ExecuteCommandAsync(new UnblockReaderCommand(username));
            }
            if (firstTransaction.IsSuccess && secondTransaction.IsSuccess)
            {
                return Ok(secondTransaction);
            }

            return BadRequest("Blocking failed.");

        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{username}/unblock-reader")]
        public async Task<IActionResult> UnblockUserAsync([FromRoute] string username)
        {
            var firstTransaction = await _catalogModule.ExecuteCommandAsync(new UnblockReaderCommand(username));
            if (!firstTransaction.IsSuccess)
            {
                await _userAccessModule.ExecuteCommandAsync(new BlockUserCommand(username));
            }

            var secondTransaction = await _userAccessModule.ExecuteCommandAsync(new UnblockUserCommand(username));
            if (!secondTransaction.IsSuccess)
            {
                await _catalogModule.ExecuteCommandAsync(new BlockReaderCommand(username));
            }
            if (firstTransaction.IsSuccess && secondTransaction.IsSuccess)
            {
                return Ok(secondTransaction);
            }

            return BadRequest("Unblocking failed.");


        }

    }
}
