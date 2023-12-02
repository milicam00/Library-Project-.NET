using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Rentals.ApproveComment;
using OnlineLibary.Modules.Catalog.Application.Rentals.BlockComment;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetReportedComments;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveMostPopularOwners;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveRatedBooks;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopFiveRatedBooksOfLibrary;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopTenMostPopularBooks;
using OnlineLibary.Modules.Catalog.Application.Rentals.TopTenMostPopularBooksOfLibrary;
using OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentalsBookOfLibrary;
using OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentalsBooks;

namespace OnlineLibrary.API.Modules.Catalog.Rental.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalAdminController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
        public RentalAdminController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("reported-comments")]
        public async Task<IActionResult> GetReportedCommentsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? orderBy)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new GetReportedCommentsQuery(pageNumber, pageSize,orderBy));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{rentalBookId}/approve-comment")]
        public async Task<IActionResult> ApproveCommentAsync([FromRoute] Guid rentalBookId)
        {
            var result = await _catalogModule.ExecuteCommandAsync(new ApproveCommentCommand(rentalBookId));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{rentalBookId}/block-comment")]
        public async Task<IActionResult> BlockCommentAsync([FromRoute] Guid rentalBookId)
        {
            var result = await _catalogModule.ExecuteCommandAsync(new BlockCommentCommand(rentalBookId));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("total-rental-books")]
        public async Task<IActionResult> GetTotalRentalBooksAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new TotalRentalBooksQuery(startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{libraryId}/total-rental-books-of-library")]
        public async Task<IActionResult> GetTotalRentalBooksAsync([FromRoute] Guid libraryId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new TotalRentalBooksOfLibraryQuery(libraryId, startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("top-ten-rated-books")]
        public async Task<IActionResult> GetTopTenRatedBooksAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new TopTenRatedBooksQuery(startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{libraryId}/top-five-rated-books-of-library")]
        public async Task<IActionResult> GetTopFiveRatedBooksOfLibraryAsync([FromRoute] Guid libraryId,[FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new TopFiveRatedBooksOfLibraryQuery(libraryId,startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet("top-ten-most-popular-books")]
        public async Task<IActionResult> GetTopTenMostPopularBooksAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new TopTenMostPopularBooksQuery(startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet("{libraryId}/top-ten-most-popular-books-of-library")]
        public async Task<IActionResult> GetTopTenMostPopularBooksAsync([FromRoute] Guid libraryId,[FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new TopTenMostPopularBooksOfLibraryQuery(libraryId,startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("top-ten-most-popular-owners")]
        public async Task<IActionResult> GetTopTenMostPopularOwnersAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _catalogModule.ExecuteQueryAsync(new TopTenMostPopularOwnersQuery(startDate, endDate));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }
       
    }
}
