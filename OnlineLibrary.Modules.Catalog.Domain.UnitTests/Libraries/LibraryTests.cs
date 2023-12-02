using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.UnitTests.Libraries
{
    [TestFixture]
    public class LibraryTests
    {
        [Test]
        public void Create_Library_Should_Set_Properties_Correctly()
        {
            //Arrange
            var libraryName = "Test Library";
            var isActive = true;
            var ownerId = Guid.NewGuid();

            //Act
            var library = Library.Create(libraryName, isActive, ownerId);

            //Assert
            Assert.That(library.LibraryId, Is.Not.EqualTo(default(Guid)));
            Assert.That(library.LibraryName, Is.EqualTo(libraryName));
            Assert.That(library.IsActive, Is.EqualTo(isActive));
            Assert.That(library.OwnerId, Is.EqualTo(ownerId));

        }

        [Test]
        public void ActivateLibrary_Should_Set_IsActive_To_True()
        {
            //Arrange 
            var library = new Library();

            //Act
            library.ActivateLibrary();

            //Assert
            Assert.IsTrue(library.IsActive);
        }

        [Test]
        public void DeactivateLibrary_Should_Set_IsActive_To_False()
        {
            // Arrange
            var library = new Library();

            // Act
            library.DeactivateLibrary();

            // Assert
            Assert.IsFalse(library.IsActive);
        }
    }
}
