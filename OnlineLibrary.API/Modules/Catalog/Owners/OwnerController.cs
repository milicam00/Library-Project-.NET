using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibary.Modules.Catalog.Application.Books.AddBooks;
using OnlineLibary.Modules.Catalog.Application.Books.ImportBookInLibraryXml;
using OnlineLibary.Modules.Catalog.Application.Books.ImportBooks;
using OnlineLibary.Modules.Catalog.Application.Books.ImportBooksInLibraryCsv;
using OnlineLibary.Modules.Catalog.Application.Books.ImportBooksInLibraryXlsx;
using OnlineLibary.Modules.Catalog.Application.Books.ImportBooksXlsx;
using OnlineLibary.Modules.Catalog.Application.Books.ImportBooksXml;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Users.BlockOwner;
using OnlineLibary.Modules.Catalog.Application.Users.UnblockOwner;
using OnlineLibrary.API.Configuration.Authentication;
using OnlineLibrary.API.Modules.Catalog.LibraryBooks.Requests;
using OnlineLibrary.BuildingBlocks.Application.ICsvGeneration;
using OnlineLibrary.BuildingBlocks.Application.IXlsxGeneration;
using OnlineLibrary.BuildingBlocks.Application.XmlGeneration;
using OnlineLibrary.Modules.UserAccess.Application.BlockUser;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Application.UnblockUser;
using System.Text;
using BookOfOwnerDto = OnlineLibrary.BuildingBlocks.Domain.Results.BookOfOwnerDto;
using GetBooksOfLibraryOwnerQuery = OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfLibraryOwner.GetBooksOfLibraryOwnerQuery;

namespace OnlineLibrary.API.Modules.Catalog.Owners
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ICatalogModule _catalogModule;
        private readonly IUserAccessModule _userAccessModule;
        private readonly IXmlGenerationService _xmlGenerationService;
        private readonly ICsvGenerationService _csvGenerationService;
        private readonly IXlsxGenerationService _xlsxGenerationService;
        public OwnerController(ICatalogModule catalogModule, IUserAccessModule userAccessModule, IXmlGenerationService xmlGenerationService, ICsvGenerationService csvGenerationService, IXlsxGenerationService xlsxGenerationService)
        {
            _catalogModule = catalogModule;
            _userAccessModule = userAccessModule;
            _xmlGenerationService = xmlGenerationService;
            _csvGenerationService = csvGenerationService;
            _xlsxGenerationService = xlsxGenerationService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{username}/block-owner")]
        public async Task<IActionResult> BlockUserAsync([FromRoute] string username)
        {
            var firstTransaction = await _catalogModule.ExecuteCommandAsync(new BlockOwnerCommand(username));
            if (!firstTransaction.IsSuccess)
            {
                await _userAccessModule.ExecuteCommandAsync(new UnblockUserCommand(username));
            }

            var secondTransaction = await _userAccessModule.ExecuteCommandAsync(new BlockUserCommand(username));
            if (!secondTransaction.IsSuccess)
            {
                await _catalogModule.ExecuteCommandAsync(new UnblockOwnerCommand(username));
            }
            if (firstTransaction.IsSuccess && secondTransaction.IsSuccess)
            {
                return Ok(secondTransaction);
            }

            return BadRequest("Failed.");


        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{username}/unblock-owner")]
        public async Task<IActionResult> UnblockUserAsync([FromRoute] string username)
        {
            var firstTransaction = await _catalogModule.ExecuteCommandAsync(new UnblockOwnerCommand(username));
            if (!firstTransaction.IsSuccess)
            {
                await _userAccessModule.ExecuteCommandAsync(new BlockUserCommand(username));
            }

            var secondTransaction = await _userAccessModule.ExecuteCommandAsync(new UnblockUserCommand(username));
            if (!secondTransaction.IsSuccess)
            {
                await _catalogModule.ExecuteCommandAsync(new BlockOwnerCommand(username));
            }
            if (firstTransaction.IsSuccess && secondTransaction.IsSuccess)
            {
                return Ok(secondTransaction);
            }


            return BadRequest("Unblocking failed.");


        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpGet("books/xml")]
        public async Task<IActionResult> GetBooksXmlAsync()
        {
            string username = User.Identity.Name;
            var result = await _catalogModule.ExecuteQueryAsync(new OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfOwner.GetBooksOfLibraryOwnerQuery(username));
            if (result.IsSuccess && result.Data is List<BookOfOwnerDto> books)
            {
                string xmlData = _xmlGenerationService.SerializeBooksToXml(books);
                byte[] byteArray = Encoding.UTF8.GetBytes(xmlData);
                return File(byteArray, "text/xml", "books.xml");
            }
            return BadRequest(result.ErrorMessage);
        }


        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpGet("books/csv")]
        public async Task<IActionResult> GetBooksCsvAsync()
        {
            var username = User.Identity.Name;
            var result = await _catalogModule.ExecuteQueryAsync(new OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfOwner.GetBooksOfLibraryOwnerQuery(username));
            if (result.IsSuccess)
            {
                List<BookOfOwnerDto> books = (List<BookOfOwnerDto>)result.Data;
                var csvData = _csvGenerationService.SerializeBooksToCsv(books);
                return File(csvData, "text/csv", "books.csv");
            }

            return BadRequest(result.ErrorMessage);

        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpGet("books/xlsx")]
        public async Task<IActionResult> GetBooksXlsxAsync()
        {
            var username = User.Identity.Name;
            var result = await _catalogModule.ExecuteQueryAsync(new OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfOwner.GetBooksOfLibraryOwnerQuery(username));
            if (result.IsSuccess)
            {
                List<BookOfOwnerDto> books = (List<BookOfOwnerDto>)result.Data;
                var xlsxData = _xlsxGenerationService.SerializeBooksToXlsx(books);
                return File(xlsxData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "books.xlsx");
            }

            return BadRequest(result.ErrorMessage);
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpGet("books-of-library/xml")]
        public async Task<IActionResult> GetBooksOfLibraryAsync([FromQuery] string libraryName)
        {
            var username = User.Identity.Name;
            var result = await _catalogModule.ExecuteQueryAsync(new OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfLibraryOwner.GetBooksOfLibraryOwnerQuery(username, libraryName));
            if (result.IsSuccess)
            {
                List<BookOfOwnerDto> books = (List<BookOfOwnerDto>)result.Data;
                string xmlData = _xmlGenerationService.SerializeBooksToXml(books);
                return Ok(xmlData);
            }

            return BadRequest(result.ErrorMessage);
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpGet("books-of-library/csv")]
        public async Task<IActionResult> GetBooksOfLibraryOwnerCsvAsync([FromQuery] string libraryName)
        {
            var username = User.Identity.Name;
            var result = await _catalogModule.ExecuteQueryAsync(new GetBooksOfLibraryOwnerQuery(username, libraryName));
            if (result.IsSuccess)
            {
                List<BookOfOwnerDto> books = (List<BookOfOwnerDto>)result.Data;
                var csvData = _csvGenerationService.SerializeBooksToCsv(books);
                return File(csvData, "text/csv", "books.csv");
            }

            return BadRequest(result.ErrorMessage);

        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpGet("books-of-library/xlsx")]
        public async Task<IActionResult> GetBooksOfLibraryXlsxAsync([FromQuery] string libraryName)
        {
            var username = User.Identity.Name;
            var result = await _catalogModule.ExecuteQueryAsync(new GetBooksOfLibraryOwnerQuery(username, libraryName));
            if (result.IsSuccess)
            {
                List<BookOfOwnerDto> books = (List<BookOfOwnerDto>)result.Data;
                var xlsxData = _xlsxGenerationService.SerializeBooksToXlsx(books);
                return File(xlsxData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "books.xlsx");
            }

            return BadRequest(result.ErrorMessage);
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("import-books/csv")]
        public async Task<IActionResult> ImportBooksCsvAsync([FromForm] IFormFile file)
        {
            var username = User.Identity.Name;
            using (var stream = file.OpenReadStream())
            {
                var result = await _catalogModule.ExecuteCommandAsync(new ImportBooksCsvCommand(stream, username));
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result?.ErrorMessage);
            }
            
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("import-books/xlsx")]
        public async Task<IActionResult> ImportBooksXlsxAsync([FromForm] IFormFile file)
        {
            var username = User.Identity.Name;
            using (var stream = file.OpenReadStream())
            {
                var result = await _catalogModule.ExecuteCommandAsync(new ImportBooksXlsxCommand(username, stream));
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result.ErrorMessage);
            }
           
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("import-books/xml")]
        public async Task<IActionResult> ImportBooksXmlAsync([FromForm] IFormFile file)
        {
            var username = User.Identity?.Name;
            using (var stream = file.OpenReadStream())
            {
                var result = await _catalogModule.ExecuteCommandAsync(new ImportBooksXmlCommand(username, stream));
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result.ErrorMessage);
            }
          
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("{libraryId}/import-books-in-library/csv")]
        public async Task<IActionResult> ImportBooksInLibraryCsvAsync([FromForm] IFormFile file, [FromRoute] Guid libraryId)
        {
            var username = User.Identity?.Name;
            using (var stream = file.OpenReadStream())
            {
                var result = await _catalogModule.ExecuteCommandAsync(new ImportBooksInLibraryCsvCommand(username, stream, libraryId));
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result.ErrorMessage);
            }
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("{libraryId}/import-books-in-library/xml")]
        public async Task<IActionResult> ImportBooksInLibraryXmlAsync([FromForm] IFormFile file, [FromRoute] Guid libraryId)
        {
            var username = User.Identity?.Name;
            using (var stream = file.OpenReadStream())
            {
                var result = await _catalogModule.ExecuteCommandAsync(new ImportBooksInLibraryXmlCommand(username, stream, libraryId));
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result.ErrorMessage);
           }
        }

        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("{libraryId}/import-books-in-library/xlsx")]
        public async Task<IActionResult> ImportBooksInLibraryXlsxAsync([FromForm] IFormFile file, [FromRoute] Guid libraryId)
        {
            var username = User.Identity?.Name;
            using (var stream = file.OpenReadStream())
            {
                var result = await _catalogModule.ExecuteCommandAsync(new ImportBooksInLibraryXlsxCommand(username, stream, libraryId));    
                if(result.IsSuccess)
                {
                    return Ok(result);  
                }
                return BadRequest(result?.ErrorMessage);        
            }
        }
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost("add-books")]
        public async Task<IActionResult> AddBooksAsync([FromBody] CreateBooksRequest request)
        {
            var username = User.Identity.Name;
            var res = await _catalogModule.ExecuteCommandAsync(new AddBooksCommand(request.Books, username));
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return BadRequest(res.ErrorMessage);
        }
    }
}
