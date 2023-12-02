using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Books.GetBook;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Requests;

namespace OnlineLibrary.API.Modules.Catalog.LibraryBooks.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookReaderController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;

        public BookReaderController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooksAsync([FromQuery] BookSearchRequest request)
        {
            var query = new GetBookQuery(request.Title, request.Author, request.PubblicationYear, request.Genres, request.Pages, request.Rate, request.PageNumber, request.PageSize,request.OrderBy);
            var books = await _catalogModule.ExecuteQueryAsync(query);
            if (books != null)
            {
                return Ok(books);
            }

            return BadRequest("Failed");
        }
    }
}
