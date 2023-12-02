using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.UnitTests.Readers
{
    [TestFixture]
    public class ReaderTests
    {
        [Test]
        public void CreateReader_Should_Set_Properties_Correctly()
        {
            //Arrange
            var username = "username";  
            var email = "email";    
            var firstName = "firstName";
            var lastName = "lastName";

            //Act
            var reader = Reader.CreateUser(username, email, firstName, lastName);

            //Assert
            Assert.That(reader.ReaderId, Is.Not.EqualTo(default(Guid)));
            Assert.That(reader.UserName, Is.EqualTo(username));
            Assert.That(reader.UserName, Is.EqualTo(username));
            Assert.That(reader.FirstName, Is.EqualTo(firstName));
            Assert.That(reader.LastName, Is.EqualTo(lastName));
        }

        [Test]
        public void ChangeUsername_Should_Change_Username()
        {
            //Arrange
            var username = "username";
            var reader = Reader.CreateUser("user", "email@gmail.com", "firstName", "lastName");
            
            //Act
            reader.ChangeUsername(username);

            //Assert
            Assert.That(reader.UserName, Is.EqualTo(username));

        }
    }
}
