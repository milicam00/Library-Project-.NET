using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Libraries.CreateLibrary
{
    public class CreateLibraryCommandHandler : ICommandHandler<CreateLibraryCommand, Result>
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IOwnerRepository _ownerRepository;
        public CreateLibraryCommandHandler(ILibraryRepository libraryRepository,IOwnerRepository ownerRepository)
        {
            _libraryRepository = libraryRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<Result> Handle(CreateLibraryCommand request, CancellationToken cancellationToken)
        {

            Library libraryWithSameName = await _libraryRepository.GetByName(request.LibraryName);
            if (libraryWithSameName != null)
            {
                return Result.Failure("Library with same name already exist.");
            }
            Owner owner = await _ownerRepository.GetByUsername(request.Username);
            var library = Library.Create(request.LibraryName, request.IsActive, owner.OwnerId);
            await _libraryRepository.AddAsync(library);

            var libraryDto = new LibraryDto
            {
                LibraryId = library.LibraryId,
                Name = library.LibraryName,
                IsActive = library.IsActive
            };
            return Result.Success(libraryDto);


        }
    }
}
