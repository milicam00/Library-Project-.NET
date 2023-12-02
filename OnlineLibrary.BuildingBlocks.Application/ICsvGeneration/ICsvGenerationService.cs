using OnlineLibrary.BuildingBlocks.Domain.Results;

namespace OnlineLibrary.BuildingBlocks.Application.ICsvGeneration
{
    public interface ICsvGenerationService
    {
        byte[] SerializeBooksToCsv(List<BookOfOwnerDto> books);
        Task<List<BookDto>> DeserializeBooksFromCsv(Stream fileStream);
        Task<List<ImportedBookForOneLibraryDto>> DeserializeBooksFromCsvForOneLibrary(Stream fileStream);
    }
}
