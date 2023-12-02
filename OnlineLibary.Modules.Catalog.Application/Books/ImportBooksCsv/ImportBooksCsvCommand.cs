using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBooks
{
    public class ImportBooksCsvCommand : CommandBase<Result>
    {
        public ImportBooksCsvCommand(Stream fileStream,string username)
        {
            FileStream = fileStream;
            Username = username;    
        }  
        public string Username { get; set; }
        public Stream FileStream { get; }
    }
}
