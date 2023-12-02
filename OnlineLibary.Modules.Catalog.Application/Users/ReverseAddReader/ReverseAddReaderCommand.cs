using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;

namespace OnlineLibary.Modules.Catalog.Application.Users.ReverseAddReader
{
    public class ReverseAddReaderCommand : CommandBase<Result>
    {
        public string Username { get; set; }
        public ReverseAddReaderCommand(string username)
        {
            Username = username;
        }
    }
}
