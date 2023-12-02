using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBooksInLibraryXlsx
{
    public class ImportBooksInLibraryXlsxCommand : CommandBase<Result>
    {
        public ImportBooksInLibraryXlsxCommand(string username,Stream stream,Guid libraryId)
        {
            Username = username;
            FileStream = stream;
            LibraryId = libraryId;
        }
        public string Username { get; set; }                
        public Stream FileStream { get; set; }  
        public Guid LibraryId { get; set; }
    }
}
