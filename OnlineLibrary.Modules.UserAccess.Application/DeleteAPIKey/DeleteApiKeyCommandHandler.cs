using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;

namespace OnlineLibrary.Modules.UserAccess.Application.DeleteAPIKey
{
    public class DeleteApiKeyCommandHandler : ICommandHandler<DeleteApiKeyCommand, Result>
    {
        private readonly IAPIKeyRepository _apiKeyRepository;
        public DeleteApiKeyCommandHandler(IAPIKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }
        public async Task<Result> Handle(DeleteApiKeyCommand request, CancellationToken cancellationToken)
        {
            APIKey? apiKey = await _apiKeyRepository.GetAsync(request.Username);
            if (apiKey == null)
            {
                return Result.Failure("This user has no key.");
            }
            
            _apiKeyRepository.DeleteApiKey(apiKey);
            return Result.Success(apiKey);              
        }
    }
}
