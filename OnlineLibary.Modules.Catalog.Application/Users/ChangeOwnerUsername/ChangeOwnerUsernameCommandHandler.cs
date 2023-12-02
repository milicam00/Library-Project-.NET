using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.ChangeOwnerUsername
{
    public class ChangeOwnerUsernameCommandHandler : ICommandHandler<ChangeOwnerUsernameCommand, Result>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IReaderRepository _readerRepository;

        public ChangeOwnerUsernameCommandHandler(IOwnerRepository ownerRepository, IReaderRepository readerRepository)
        {
            _ownerRepository = ownerRepository;
            _readerRepository = readerRepository;
        }
        public async Task<Result> Handle(ChangeOwnerUsernameCommand request, CancellationToken cancellationToken)
        {

            Owner owner = await _ownerRepository.GetByUsername(request.OldUsername);
            Owner existingOwnerWithNewUsername = await _ownerRepository.GetByUsername(request.NewUsername);
            Reader existingReaderWithNewUsername = await _readerRepository.GetByUsername(request.NewUsername);
            if (owner == null)
            {
                return Result.Failure("Owner does  not exist.");
            }

            if (existingOwnerWithNewUsername != null || existingReaderWithNewUsername != null)
            {
                return Result.Failure("Owner with this username already exist.");
            }
            owner.ChangeUsername(request.NewUsername);
            _ownerRepository.Update(owner);

            return Result.Success("Successfully changed username.");


        }
    }
}
