using Moq;
using OnlineLibrary.Modules.UserAccess.Application.AddOwner;
using OnlineLibrary.Modules.UserAccess.Application.AddReader;
using OnlineLibrary.Modules.UserAccess.Application.Authentication;
using OnlineLibrary.Modules.UserAccess.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Modules.UserAccess.Application.UnitTest
{
    [TestFixture]
    public class RegisterReaderCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidRequest_CreatesOwnerAndReturnsUserDto()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var email = "testuser@example.com";
            var firstName = "Test";
            var lastName = "Test";

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).Returns(Task.FromResult<User>(null));

            var handler = new RegisterReaderCommandHandler(userRepository.Object);
            var command = new RegisterReaderCommand(username, password, email, firstName, lastName);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsInstanceOf<UserDto>(result.Data);

            var userDto = (UserDto)result.Data;
            Assert.That(userDto.UserName, Is.EqualTo(username));
            Assert.That(userDto.Email, Is.EqualTo(email));
            Assert.That(userDto.FirstName, Is.EqualTo(firstName));
            Assert.That(userDto.LastName, Is.EqualTo(lastName));
            Assert.IsTrue(userDto.IsActive);
        }


        [Test]
        public async Task Handle_UserWithSameUsernameAlreadyExist_ReturnFailureResult()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var email = "testuser@example.com";
            var firstName = "Test";
            var lastName = "Test";
            var user = new User(Guid.NewGuid(), username, PasswordManager.HashPassword(password), email, firstName, lastName, UserRole.Reader);
            user.IsActive = true;

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);

            var handler = new RegisterOwnerCommandHandler(userRepository.Object);
            var command = new RegisterOwnerCommand(username, password, email, firstName, lastName);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.ErrorMessage, Is.EqualTo("User with same username already exist."));

        }
    }
}
