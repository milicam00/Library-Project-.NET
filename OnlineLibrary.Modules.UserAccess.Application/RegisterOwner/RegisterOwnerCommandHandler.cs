using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Authentication;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.AddOwner
{
    public class RegisterOwnerCommandHandler : ICommandHandler<RegisterOwnerCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public RegisterOwnerCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<Result> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
        {

            var password = PasswordManager.HashPassword(request.Password);
            User userWithSameUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (userWithSameUsername != null)
            {
                return Result.Failure("User with same username already exist.");
            }
            var user = User.CreateOwner(
                request.Username,
                password,
                request.Email,
                request.FirstName,
                request.LastName
                );
            await _userRepository.AddAsync(user);

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
