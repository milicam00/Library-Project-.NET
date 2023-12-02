using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.ReverseAddOwner
{
    public class ReverseAddOwnerCommandHandler : ICommandHandler<ReverseAddOwnerCommand, Result>
    {
        private readonly IOwnerRepository _ownerRepository;

        public ReverseAddOwnerCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Result> Handle(ReverseAddOwnerCommand request, CancellationToken cancellationToken)
        {

            Owner owner = await _ownerRepository.GetByUsername(request.Username);
            if (owner == null)
            {
                return Result.Failure("User does not exist.");
            }
            _ownerRepository.Delete(owner);
            return Result.Success("Deleted user");

        }
    }
}
