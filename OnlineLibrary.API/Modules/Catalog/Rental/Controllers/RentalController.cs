using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetCommentsOfBook;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetRentalBooks;
using OnlineLibrary.API.Modules.Catalog.Rental.Requests;

namespace OnlineLibrary.API.Modules.Catalog.Rental.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
        public RentalController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }


        [HttpGet("{rentalId}/rental-books")]
        public async Task<IActionResult> GetRentalBooksAsync([FromRoute] Guid rentalId)
        {

            var list = await _catalogModule.ExecuteQueryAsync(new GetRentalBooksQuery(rentalId));
            if (list != null)
            {
                return Ok(list);
            }

            return BadRequest("Failed");

        }

        [HttpGet("{bookId}/comments-of-book")]
        public async Task<IActionResult> GetCommentsOfBookAsync([FromRoute] Guid bookId, [FromQuery] CommentsOfBookSearchRequest request)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new GetCommentsOfBookQuery(bookId,request.PageNumber,request.PageSize,request.OrderBy));
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
