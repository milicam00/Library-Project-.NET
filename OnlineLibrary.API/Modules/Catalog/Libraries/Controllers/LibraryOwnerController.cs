using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Libraries.CreateLibrary;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;

namespace OnlineLibrary.API.Modules.Catalog.Libraries.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibraryOwnerController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
       

        public LibraryOwnerController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
 
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("add-library")]
        public async Task<IActionResult> AddLibraryAsync([FromBody] AddLibraryRequest request)
        {
            var username = User.Identity.Name;
           
            var library = await _catalogModule.ExecuteCommandAsync(new CreateLibraryCommand(
                request.LibraryName,
                request.IsActive,
                username
                ));

            if (library.IsSuccess)
            {
                return Ok(library);
            }

            return BadRequest(library.ErrorMessage);


        }

    }
}
