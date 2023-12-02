using Moq;
using OnlineLibary.Modules.Catalog.Application.Users.AddOwner;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibrary.Modules.Catalog.Application.UnitTest
{
    [TestFixture]
    public class AddOwnerCommandHandlerTests 
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
            var command = new AddOwnerCommand(username, email, firstName, lastName);

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

            Owner owner = new Owner
            { 
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(r => r.AddAsync(owner));

            var command = new AddOwnerCommand(username,email,firstName, lastName);
            var handler = new AddOwnerCommandHandler(ownerRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
