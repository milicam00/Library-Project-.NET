using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.BlockOwner
{
    public class BlockOwnerCommandHandler : ICommandHandler<BlockOwnerCommand, Result>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILibraryRepository _libraryRepository;
        public BlockOwnerCommandHandler(IOwnerRepository ownerRepository, ILibraryRepository libraryRepository)
        {
            _ownerRepository = ownerRepository;
            _libraryRepository = libraryRepository;

        }
        public async Task<Result> Handle(BlockOwnerCommand request, CancellationToken cancellationToken)
        {
            Owner owner = await _ownerRepository.GetByUsername(request.UserName);
            if (owner == null)
            {
                return Result.Failure("User does not exist.");
            }

            if (owner.IsBlocked)
            {
                return Result.Failure("User is already blocked.");
            }
            owner.Block();

            List<Library> libraries = await _libraryRepository.GetByOwnerId(owner.OwnerId);
            if (libraries != null)
            {
                foreach (Library library in libraries)
                {
                    library.DeactivateLibrary();
                    _libraryRepository.UpdateLibrary(library);
                }
            }

            _ownerRepository.UpdateOwner(owner);

            return Result.Success("User is blocked.");


        }
    }
}
