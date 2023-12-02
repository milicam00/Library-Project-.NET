using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.AddOwner
{
    public class AddOwnerCommandHandler : ICommandHandler<AddOwnerCommand, Result>
    {
        private readonly IOwnerRepository _ownerRepository;

        public AddOwnerCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Result> Handle(AddOwnerCommand request, CancellationToken cancellationToken)
        {

            var owner = Owner.CreateOwner(
            request.UserName,
            request.Email,
            request.FirstName,
            request.LastName
            );
            await _ownerRepository.AddAsync(owner);

            return Result.Success("Successfully registration!");


        }
    }
}
