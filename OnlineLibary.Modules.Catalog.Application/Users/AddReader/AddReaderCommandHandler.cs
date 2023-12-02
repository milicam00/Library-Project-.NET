using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Users.AddReader
{
    public class AddReaderCommandHandler : ICommandHandler<AddReaderCommand, Result>
    {
        private readonly IReaderRepository _readerRepository;
        public AddReaderCommandHandler(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<Result> Handle(AddReaderCommand request, CancellationToken cancellationToken)
        {
            var reader = Reader.CreateUser(
                   request.UserName,
                   request.Email,
                   request.FirstName,
                   request.LastName
                   );
            await _readerRepository.AddAsync(reader);

            return Result.Success("Successfully registration");

        }
    }



}
