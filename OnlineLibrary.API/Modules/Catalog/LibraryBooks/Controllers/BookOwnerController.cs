using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Books.AddBooks;
using OnlineLibary.Modules.Catalog.Application.Books.ChangeBook;
using OnlineLibary.Modules.Catalog.Application.Books.CreateBook;
using OnlineLibary.Modules.Catalog.Application.Books.DeleteBook;
using OnlineLibary.Modules.Catalog.Application.Books.ImportBooks;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.API.Configuration.Authentication;
using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Requests;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.API.Modules.Catalog.LibraryBooks.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookOwnerController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
       

        public BookOwnerController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
          
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("add-book")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBookAsync([FromBody] CreateBookRequest request)
        {

            var result = await _catalogModule.ExecuteCommandAsync(new CreateBookCommand(

            request.Title,
            request.Description,
            request.Author,
            request.Pages,
            request.Genres,
            request.PubblicationYear,
            request.NumberOfCopies,
            request.Library
            ));

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);

        }

        [Authorize(Roles = "Owner")]
        [HttpPut("update-book/{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditBookAsync([FromRoute] Guid bookId, [FromBody] ChangeBookRequest request)
        {
            var username = User.Identity.Name;

            var result = await _catalogModule.ExecuteCommandAsync(new ChangeBookCommand(
            bookId,
            request.Title,
            request.Description,
            request.Author,
            request.Pages,
            request.PubblicationYear,
            request.UserRating,
            request.NumOfCopies,
            username));

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);


        }

        [Authorize(Roles = "Owner")]
        [HttpPut("{bookId}/remove-book")]
        public async Task<IActionResult> RemoveBookAsync([FromRoute] Guid bookId)
        {
            var username = User.Identity.Name;

            var result = await _catalogModule.ExecuteCommandAsync(new DeleteBookCommand(username, bookId));
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);

        }

       

        
    }
}
