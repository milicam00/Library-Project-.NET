using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.UnblockUser
{
    public class UnblockUserCommandHandler : ICommandHandler<UnblockUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        public UnblockUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
        {

            User user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return Result.Failure("User does not exist.");
            }

            if (user.IsActive)
            {
                return Result.Failure("User is already unblocked.");
            }
            user.UnblockUser();
            _userRepository.UpdateUser(user);
            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive
            };


            return Result.Success(userDto);

        }

    }
}
