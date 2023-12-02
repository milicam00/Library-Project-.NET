using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.ChangeReaderUsername
{
    public class ChangeReaderUsernameCommandHandler : ICommandHandler<ChangeReaderUsernameCommand, Result>
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IOwnerRepository _ownerRepository;
        public ChangeReaderUsernameCommandHandler(IReaderRepository readerRepository, IOwnerRepository ownerRepository)
        {
            _readerRepository = readerRepository;
            _ownerRepository = ownerRepository;
        }
        public async Task<Result> Handle(ChangeReaderUsernameCommand request, CancellationToken cancellationToken)
        {

            Reader reader = await _readerRepository.GetByUsername(request.OldUsername);
            Reader existingReaderWithNewUsername = await _readerRepository.GetByUsername(request.NewUsername);
            Owner existingOwnerWithNewUsername = await _ownerRepository.GetByUsername(request.NewUsername);
            if (reader == null)
            {
                return Result.Failure("Reader does  not exist.");
            }

            if (existingReaderWithNewUsername != null || existingOwnerWithNewUsername != null)
            {
                return Result.Failure("User with this username already exist.");
            }

            reader.ChangeUsername(request.NewUsername);
            _readerRepository.Update(reader);

            return Result.Success("Successfully changed username.");


        }
    }
}
