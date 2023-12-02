using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.AddReader
{
    public class RegisterReaderCommand : CommandBase<Result>
    {
        public RegisterReaderCommand(
            string username,
            string password,
             string email,
            string firstName,
            string lastName
            )
        {

            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;

        }

        public string Username { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string Password { get; }
    }
}
