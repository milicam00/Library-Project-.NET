using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.AddReader;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class AddReaderCommandHandlerTests
    {
        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var username = "username";
            var email = "email";
            var firstName = "firstname";
            var lastName = "lastName";

            //Act
            var command = new AddReaderCommand(username, email, firstName, lastName);

            //Assert
            Assert.That(command.UserName, Is.EqualTo(username));
            Assert.That(command.Email, Is.EqualTo(email));
            Assert.That(command.FirstName, Is.EqualTo(firstName));
            Assert.That(command.LastName, Is.EqualTo(lastName));

        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            //Arrange
            var username = "username";
            var email = "email";
            var firstName = "firstname";
            var lastName = "lastName";

            Reader reader = new Reader
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var readerRepository = new Mock<IReaderRepository>();
            readerRepository.Setup(r => r.AddAsync(reader));

            var command = new AddReaderCommand(username, email, firstName, lastName);
            var handler = new AddReaderCommandHandler(readerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
