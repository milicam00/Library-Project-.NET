using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.OwnerRentals.CreateOwnerRenatal;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetComments;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetPreviousRentalsOwner;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetUnreturnedBooks;
using OnlineLibary.Modules.Catalog.Application.Rentals.ReportComment;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveRatedBooksOfLibraryOwner;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveRatedBooksOwner;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopTenMostPopularBooksOfLIbraryOwner;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopTenMostPopularBooksOwner;
using OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentalBooksOfLibraryOwner;
using OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentals;
using OnlineLibrary.API.Configuration.Authentication;
using OnlineLibrary.API.Modules.Catalog.Rental.Requests;

namespace OnlineLibrary.API.Modules.Catalog.Rental.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalOwnerController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
       
        public RentalOwnerController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("previous-rentals-owner")]
        public async Task<IActionResult> GetPreviousRentalsOwnerAsync([FromQuery] SearchRentalOwnerRequest request)
        {
            var username = User.Identity.Name;
          
            var result = await _catalogModule.ExecuteQueryAsync(new GetPreviousRentalsOwnerQuery(username, request.LibraryName, request.IsReturned, request.Username, request.PageNumber, request.PageSize,request.OrderBy));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("unreturned-books")]
        public async Task<IActionResult> GetUnreturnedBooksAsync([FromQuery] SearchUnreturnedBooksRequest request)
        {
            var username = User.Identity.Name;
           
            var result = await _catalogModule.ExecuteQueryAsync(new GetUnreturnedBooksQuery(username, request.LibraryName, request.BookTitle, request.PageNumber, request.PageSize,request.OrderBy));

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);

        }

        [Authorize(Roles = "Owner")]
        [HttpPut("{rentalBookId}/report-comment")]
        public async Task<IActionResult> ReportCommentAsync([FromRoute] Guid rentalBookId)
        {
            var username = User.Identity.Name;
          
            var result = await _catalogModule.ExecuteCommandAsync(new ReportCommentCommand(username, rentalBookId));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);
        }


        [Authorize(Roles = "Owner")]
        [HttpGet("comments")]
        public async Task<IActionResult> GetCommentsAsync([FromQuery] CommentSearchRequest request)
        {
            var username = User.Identity.Name;
            var result = await _catalogModule.ExecuteQueryAsync(new GetCommentsQuery(username, request.LibraryName, request.BookTitle, request.UserName, request.PageNumber, request.PageSize,request.OrderBy));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("total-rental-books-owner")]
        public async Task<IActionResult> GetTotalRentalBooksAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var username = User.Identity.Name;
           
            var result = await _catalogModule.ExecuteQueryAsync(new TotalRentalBooksOwnerQuery(username, startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);

        }

        [Authorize(Roles = "Owner")]
        [HttpGet("{libraryId}/total-rental-books-of-library-owner")]
        public async Task<IActionResult> GetTotalRentalBooksOfLibraryAsync([FromRoute] Guid libraryId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var username = User.Identity.Name;
          
            var result = await _catalogModule.ExecuteQueryAsync(new TotalRentalBooksOfLibraryOwnerQuery(username, libraryId, startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);

        }

        [Authorize(Roles = "Owner")]
        [HttpGet("top-five-rated-books")]
        public async Task<IActionResult> GetTopFiveRatedBooksAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var username = User.Identity.Name;
          
            var result = await _catalogModule.ExecuteQueryAsync(new TopFiveRatedBooksOwnerQuery(username, startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("{libraryId}/top-five-rated-books-of-library-owner")]
        public async Task<IActionResult> GetTopFiveRatedBooksOfLibraryAsync([FromRoute] Guid libraryId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var username = User.Identity.Name;
          
            var result = await _catalogModule.ExecuteQueryAsync(new TopFiveRatedBooksOfLibraryOwnerQuery(username, libraryId, startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("top-ten-most-popular-books-owner")]
        public async Task<IActionResult> GetTopTenMostPopularBooksAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var username = User.Identity.Name;
           
            var result = await _catalogModule.ExecuteQueryAsync(new TopTenMostPopularBooksOwnerQuery(username, startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);

        }

        [Authorize(Roles = "Owner")]
        [HttpGet("{libraryId}/top-ten-most-popular-books-of-library-owner")]
        public async Task<IActionResult> GetTopTenMostPopularBooksOfLibraryAsync([FromRoute] Guid libraryId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var username = User.Identity.Name;
         
            var result = await _catalogModule.ExecuteQueryAsync(new TopFiveMostPopularBooksOfLibraryOwnerQuery(username, libraryId, startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);

        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("rental-books")]
        public async Task<IActionResult> RentalBooks([FromBody] OwnerRentalRequest request)
        {
            string username = User.Identity.Name;
            var result = await _catalogModule.ExecuteCommandAsync(new CreateOwnerRentalCommand(username, request.BookIds));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result?.ErrorMessage);
        }
    }
}
