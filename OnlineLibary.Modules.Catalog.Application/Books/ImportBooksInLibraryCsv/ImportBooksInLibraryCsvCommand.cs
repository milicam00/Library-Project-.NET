using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Books.ImportBooksInLibraryCsv
{
    public class ImportBooksInLibraryCsvCommand : CommandBase<Result>
    {
        public ImportBooksInLibraryCsvCommand(string username, Stream fileStream,Guid libraryId) 
        {
            UserName = username;
            FileStream = fileStream;
            LibraryId = libraryId;
        }
        public string UserName { get; set; }    
        public Stream FileStream { get; }
        public Guid LibraryId { get; set; } 
    }
}
