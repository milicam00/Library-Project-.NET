using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.CreateAPIKey
{
    public class CreateAPIKeyCommandHandler : ICommandHandler<CreateAPIKeyCommand, Result>
    {
        private readonly IAPIKeyRepository _apiKeyRepository;
        private readonly IUserRepository _userRepository;
        public CreateAPIKeyCommandHandler(IAPIKeyRepository apiKeyRepository,IUserRepository userRepository)
        {
            _apiKeyRepository = apiKeyRepository;
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(CreateAPIKeyCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByUsernameAsync(request.Username);
            if(user == null)
            {
                return Result.Failure("This user does not exist.");
            }

            var apiKey = APIKey.CreateKey(request.Username,request.KeyName);
            await _apiKeyRepository.AddAsync(apiKey);

            return Result.Success(apiKey);
        }
    }
}
