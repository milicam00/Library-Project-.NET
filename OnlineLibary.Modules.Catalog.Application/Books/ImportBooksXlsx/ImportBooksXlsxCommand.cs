using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBooksXlsx
{
    public class ImportBooksXlsxCommand : CommandBase<Result>
    {
        public ImportBooksXlsxCommand(string username, Stream stream)
        {
            Username = username;
            FileStream = stream;
        }
        public string Username { get; set; }
        public Stream FileStream { get; set; }
    }
}
