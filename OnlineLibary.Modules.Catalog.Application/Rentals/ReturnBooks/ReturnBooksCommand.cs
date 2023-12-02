using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.ReturnBooks
{
    public class ReturnBooksCommand : CommandBase<Result>
    {
        public string ReaderUsername { get; set; }
        public ReturnBooksCommand(string readerUsername)
        { 
            ReaderUsername = readerUsername;
        }
    }
}
