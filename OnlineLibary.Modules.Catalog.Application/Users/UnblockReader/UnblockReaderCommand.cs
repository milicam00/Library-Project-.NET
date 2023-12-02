using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Users.UnblockReader
{
    public class UnblockReaderCommand : CommandBase<Result>
    {
        public UnblockReaderCommand(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
}
