﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Rentals.CreateRental;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetPreviousRentalsReader;
using OnlineLibary.Modules.Catalog.Application.Rentals.RateBook;
using OnlineLibary.Modules.Catalog.Application.Rentals.ReturnBooks;
using OnlineLibrary.API.Modules.Catalog.Rental.Requests;

namespace OnlineLibrary.API.Modules.Catalog.Rental.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalReaderController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
      
        public RentalReaderController(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        [Authorize(Roles = "Reader")]
        [HttpPost("rental-book")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> RentalBookAsync([FromBody] RentalRequest request)
        {
            var result = await _catalogModule.ExecuteCommandAsync(new CreateRentalCommand(
                    request.UserId,
                    request.BookIds

                ));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }

        }

        [Authorize(Roles = "Reader")]
        [HttpPost("{rentalId}/rate-book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RateBookAsync([FromRoute] Guid rentalId, RateRequest request)
        {
            
            var rate = await _catalogModule.ExecuteCommandAsync(new RateBookCommand(rentalId, request.Rate, request.Comment));
            if (rate.IsSuccess)
            {
                return Ok(rate);
            }

            return BadRequest(rate.ErrorMessage);

        }

        [Authorize(Roles = "Reader")]
        [HttpPut("return-books")]
        public async Task<IActionResult> ReturnBooksAsync()
        {
            var username = User.Identity.Name;
           
            var result = await _catalogModule.ExecuteCommandAsync(new ReturnBooksCommand(username));

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);

        }

        [Authorize(Roles = "Reader")]
        [HttpGet("previous-rentals-reader")]
        public async Task<IActionResult> GetPreviousRentalsReaderAsync([FromQuery] SearchRentalReaderRequest request)
        {
            var username = User.Identity.Name;
          

            var result = await _catalogModule.ExecuteQueryAsync(new GetPreviousRentalsReaderQuery(username, request.Title, request.PageNumber, request.PageSize,request.OrderBy));
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
