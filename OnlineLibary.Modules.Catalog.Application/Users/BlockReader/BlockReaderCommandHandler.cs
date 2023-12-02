using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.BlockReader
{
    public class BlockReaderCommandHandler : ICommandHandler<BlockReaderCommand, Result>
    {
        private readonly IReaderRepository _readerRepository;
        public BlockReaderCommandHandler(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<Result> Handle(BlockReaderCommand request, CancellationToken cancellationToken)
        {
            Reader reader = await _readerRepository.GetByUsername(request.UserName);
            if (reader == null)
            {
                return Result.Failure("Reader does not exist.");
            }

            if (reader.IsBlocked)
            {
                return Result.Failure("Reader is already blocked.");
            }
            reader.Block();
            _readerRepository.Update(reader);

            return Result.Success("Reader is blocked");


        }
    }
}
