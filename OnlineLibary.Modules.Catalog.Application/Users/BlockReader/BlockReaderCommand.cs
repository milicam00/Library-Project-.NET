using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Users.BlockReader
{
    public class BlockReaderCommand : CommandBase<Result>
    {
        public BlockReaderCommand(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
}
