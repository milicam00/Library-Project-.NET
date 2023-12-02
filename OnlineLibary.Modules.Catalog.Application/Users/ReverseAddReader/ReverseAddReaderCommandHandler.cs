using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.ReverseAddReader
{
    public class ReverseAddReaderCommandHandler : ICommandHandler<ReverseAddReaderCommand, Result>
    {
        private readonly IReaderRepository _readerRepository;
        public ReverseAddReaderCommandHandler(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<Result> Handle(ReverseAddReaderCommand request, CancellationToken cancellationToken)
        {

            Reader reader = await _readerRepository.GetByUsername(request.Username);
            if (reader == null)
            {
                return Result.Failure("User does not exist.");
            }
            _readerRepository.Delete(reader);
            return Result.Success("Deleted user");

        }
    }
}
