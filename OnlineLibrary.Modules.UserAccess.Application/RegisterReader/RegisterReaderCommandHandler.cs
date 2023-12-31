﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Authentication;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Application.AddReader
{
    public class RegisterReaderCommandHandler : ICommandHandler<RegisterReaderCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        public RegisterReaderCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(RegisterReaderCommand request, CancellationToken cancellationToken)
        {

            var password = PasswordManager.HashPassword(request.Password);
            User userWithSameUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (userWithSameUsername != null)
            {
                return Result.Failure("User with same username already exist.");
            }
            var user = User.CreateReader(
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
