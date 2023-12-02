using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.UnitTests.RentalBooks
{
    [TestFixture]
    public class RentalBookTests
    {
        [Test]
        public void Create_RentalBook_Should_Set_Properties_Correctly()
        {
            //Arrange
            var bookId = Guid.NewGuid();

            //Act
            var rentalBook = new RentalBook(bookId);

            //Assert
            Assert.That(rentalBook.BookId, Is.EqualTo(bookId));
            Assert.IsNull(rentalBook.RatedRating);
            Assert.IsNull(rentalBook.TextualComment);
        }

        [Test]
        public void RateBook_Should_Set_Rating_And_Text()
        {
            //Arrange
            var rentalBook = new RentalBook();
            var rating = 4;
            var comment = "Good book!";

            //Act
            rentalBook.RateBook(rating, comment);

            //Assert
            Assert.That(rentalBook.RatedRating, Is.EqualTo(rating));
            Assert.That(rentalBook.TextualComment, Is.EqualTo(comment));
        }
    }
}
