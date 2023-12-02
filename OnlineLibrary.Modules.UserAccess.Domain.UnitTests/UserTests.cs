using OnlineLibrary.Modules.UserAccess.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Domain.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CreateAdmin_Should_Set_Admin_Role()
        {
            // Arrange
            var userName = "adminuser";
            var password = "adminpassword";
            var email = "admin@example.com";
            var firstName = "Admin";
            var lastName = "User";

            // Act
            var user = User.CreateAdmin(userName, password, email, firstName, lastName);

            // Assert
            Assert.That(user.UserName, Is.EqualTo(userName));
            Assert.That(user.Password, Is.EqualTo(password));
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.IsTrue(user.IsActive);
            Assert.That(user.FirstName, Is.EqualTo(firstName));
            Assert.That(user.LastName, Is.EqualTo(lastName));

        }

        [Test]
        public void CreateOwner_Should_Set_Admin_Role()
        {
            // Arrange
            var userName = "owneruser";
            var password = "ownerpassword";
            var email = "owner@example.com";
            var firstName = "Owner";
            var lastName = "User";

            // Act
            var user = User.CreateOwner(userName, password, email, firstName, lastName);

            // Assert
            Assert.That(user.UserName, Is.EqualTo(userName));
            Assert.That(user.Password, Is.EqualTo(password));
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.IsTrue(user.IsActive);
            Assert.That(user.FirstName, Is.EqualTo(firstName));
            Assert.That(user.LastName, Is.EqualTo(lastName));

        }

        [Test]
        public void CreateReader_Should_Set_Admin_Role()
        {
            // Arrange
            var userName = "readeruser";
            var password = "readerpassword";
            var email = "reader@example.com";
            var firstName = "Reader";
            var lastName = "User";

            // Act
            var user = User.CreateOwner(userName, password, email, firstName, lastName);

            // Assert
            Assert.That(user.UserName, Is.EqualTo(userName));
            Assert.That(user.Password, Is.EqualTo(password));
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.IsTrue(user.IsActive);
            Assert.That(user.FirstName, Is.EqualTo(firstName));
            Assert.That(user.LastName, Is.EqualTo(lastName));

        }

        [Test]
        public void BlockUser_Should_Set_IsActive_To_False()
        {
            // Arrange
            var user = new User();

            // Act
            user.BlockUser();

            // Assert
            Assert.IsFalse(user.IsActive);
        }

        [Test]
        public void UnblockUser_Should_Set_IsActive_To_True()
        {
            // Arrange
            var user = new User();
            user.BlockUser();

            // Act
            user.UnblockUser();

            // Assert
            Assert.IsTrue(user.IsActive);
        }

        [Test]
        public void SetResetPasswordCode_Should_Set_ResetPasswordCode()
        {
            // Arrange
            var user = new User();
            var resetCode = 123456;

            // Act
            user.SetResetPasswordCode(resetCode);

            // Assert
            Assert.That(user.ResetPasswordCode, Is.EqualTo(resetCode));
        }

        [Test]
        public void ChangeUsername_Should_Set_New_Username()
        {
            // Arrange
            var user = new User();
            var newUsername = "newuser";

            // Act
            user.ChangeUsername(newUsername);

            // Assert
            Assert.That(user.UserName, Is.EqualTo(newUsername));
        }
    }
}