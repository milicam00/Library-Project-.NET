using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;

namespace OnlineLibrary.Modules.UserAccess.Application.EditAPIKey
{
    public class EditAPIKeyCommandHandler : ICommandHandler<EditAPIKeyCommand, Result>
    {
        private readonly IAPIKeyRepository _apiKeyRepository;
        public EditAPIKeyCommandHandler(IAPIKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }
        public async Task<Result> Handle(EditAPIKeyCommand request, CancellationToken cancellationToken)
        {
            var apiKey = await _apiKeyRepository.GetAsync(request.Username);
            if (apiKey == null)
            {
                return Result.Failure("Invalid API key.");
            }

            apiKey.Name = request.NewName;
            _apiKeyRepository.Update(apiKey);
            return Result.Success(apiKey);
        }
    }
}
