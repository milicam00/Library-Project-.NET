using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace OnlineLibrary.Modules.Catalog.Domain.UnitTests.Books
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void NewBook_HasUniqueId()
        {
            //Arange
            var book1 = new Book("Title 1", "Description 1", "Author 1", 200, Genres.Adventure, 2020, 3, Guid.NewGuid());
            var book2 = new Book("Title 2", "Description 2", "Author 2", 250, Genres.Poetry, 2022, 3, Guid.NewGuid());

            //Act & Assert
            Assert.That(book2.BookId, Is.Not.EqualTo(book1.BookId));
        }

        [Test]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            //Arrange
            var title = "Title";
            var description = "Description";
            var author = "Author";
            var pages = 2;
            var genres  = Genres.Adventure;
            var pubblicationYear = 2022;
            var numOfCopies = 100;
            var libraryId = Guid.NewGuid();

            //Act
            var book = new Book(title, description, author, pages, genres, pubblicationYear, numOfCopies, libraryId);

            //Assert
            Assert.That(book.Title, Is.EqualTo(title));
            Assert.That(book.Description, Is.EqualTo(description));
            Assert.That(book.Author, Is.EqualTo(author));
            Assert.That(book.Pages, Is.EqualTo(pages));
            Assert.That(book.Genres, Is.EqualTo(genres));
            Assert.That(book.PubblicationYear, Is.EqualTo(pubblicationYear));
            Assert.That(book.NumberOfCopies, Is.EqualTo(numOfCopies));
            Assert.That(book.LibraryId, Is.EqualTo(libraryId));

        }

        [Test]
        public void EditBook_ChangesProperties()
        {
            //Arrange
            var book = new Book("Title", "Description", "Author", 200, Genres.Drama, 2020, 2, Guid.NewGuid());

            //Act
            book.EditBook("New Title", "New Description", "New Author", 250, 2021, 4, 30);

            //Assert
            Assert.That(book.Title, Is.EqualTo("New Title"));
            Assert.That(book.Description, Is.EqualTo("New Description"));
            Assert.That(book.Author, Is.EqualTo("New Author"));
            Assert.That(book.Pages, Is.EqualTo(250));
            Assert.That(book.PubblicationYear, Is.EqualTo(2021));
            Assert.That(book.NumberOfCopies, Is.EqualTo(30));
        }

        [Test]
        public void DeleteBook_SetsIsDeletedToTrue()
        {
            // Arrange
            var book = new Book("Title", "Description", "Author", 200, Genres.Fantasy, 2020, 2, Guid.NewGuid());

            // Act
            book.DeleteBook();

            // Assert
            Assert.IsTrue(book.IsDeleted);
        }
    }
}