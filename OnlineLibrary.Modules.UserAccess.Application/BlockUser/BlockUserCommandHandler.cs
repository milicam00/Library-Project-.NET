using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.BlockUser
{
    public class BlockUserCommandHandler : ICommandHandler<BlockUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public BlockUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {

            User user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return Result.Failure("User does not exist.");
            }

            if (!user.IsActive)
            {
                return Result.Failure("User is already blocked.");
            }
            user.BlockUser();
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
