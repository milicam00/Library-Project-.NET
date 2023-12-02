using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBookInLibraryXml
{
    public class ImportBooksInLibraryXmlCommand : CommandBase<Result>
    {
        public ImportBooksInLibraryXmlCommand(string username, Stream stream, Guid libraryId)
        {
            Username = username;
            Stream = stream;
            LibraryId = libraryId;
        }
        public string Username { get; set; }                
        public Stream Stream { get; set; }
        public Guid LibraryId { get; set; }
    }
}
