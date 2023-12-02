using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBooksXml
{
    public class ImportBooksXmlCommand : CommandBase<Result>
    {
        public ImportBooksXmlCommand(string username,Stream stream)
        {
            Username = username;
            Stream = stream;
        }  
        public string Username { get; set; }    
        public Stream Stream { get; set; }  
    }
}
