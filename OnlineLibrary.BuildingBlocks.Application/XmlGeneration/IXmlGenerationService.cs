using OnlineLibrary.BuildingBlocks.Domain.Results;

namespace OnlineLibrary.BuildingBlocks.Application.XmlGeneration
{
    public interface IXmlGenerationService
    {
        string SerializeBooksToXml(List<BookOfOwnerDto> books);
        List<BookOfOwnerDto> DeserializeBooksFromXml(Stream fileStream);
        List<ImportedBookForOneLibraryDto> DeserializeBooksForOneLibraryFromXml(Stream fileStream);
    }
}
