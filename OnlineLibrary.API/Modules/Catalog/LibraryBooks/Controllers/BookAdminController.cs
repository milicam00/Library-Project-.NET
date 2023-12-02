using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Books.GetAllBooks;
using OnlineLibary.Modules.Catalog.Application.Books.GetBookOfLibrary;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Requests;
using OnlineLibrary.BuildingBlocks.Domain;


namespace OnlineLibrary.API.Modules.Catalog.LibraryBooks.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookAdminController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;

        public BookAdminController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetBooksAsync([FromQuery] PaginationFilter filter)
        {
            var books = await _catalogModule.ExecuteCommandAsync(new GetAllBooks(filter));
            if (books != null)
            {
                return Ok(books);
            }

            return BadRequest("Failed.");


        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("books-of-library/{libraryId}")]
        public async Task<IActionResult> GetBooksOfLibraryAsync([FromRoute] Guid libraryId,[FromQuery] BookSearchRequest request)
        {

            var books = await _catalogModule.ExecuteQueryAsync(new GetBooksOfLibraryQuery(libraryId, request.Title, request.Author, request.PubblicationYear, request.Genres, request.Pages, request.Rate, request.PageNumber, request.PageSize,request.OrderBy));
            if (books != null)
            {
                return Ok(books);
            }

            return BadRequest("Failed.");

        }
    }
}
