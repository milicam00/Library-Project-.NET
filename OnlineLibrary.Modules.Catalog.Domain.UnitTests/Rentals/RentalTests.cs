using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.UnitTests.Rentals
{
    [TestFixture]
    public class RentalTests
    {
        [Test]
        public void Create_Rental_Should_Set_Properties_Correctly()
        {
            //Arrange 
            var readerId = Guid.NewGuid();
            var bookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            //Act
            var rental = Rental.Create(readerId, bookIds);

            //Assert
            Assert.That(rental.RentalId, Is.Not.EqualTo(default(Guid)));
            Assert.That(rental.ReaderId, Is.EqualTo(readerId));
            Assert.IsNotNull(rental.RentalBooks);
            Assert.That(rental.RentalBooks.Count, Is.EqualTo(bookIds.Count));
            Assert.IsFalse(rental.Returned);

        }

        [Test]
        public void ReturnBooks_Should_Update_ReturnDate_And_Returned()
        {
            //Arrange
            var rental = new Rental();

            //Act
            rental.ReturnBooks();

            //Assert
            Assert.That(rental.ReturnDate, Is.Not.EqualTo(default(DateTime)));
            Assert.IsTrue(rental.Returned);
        }
    }
}
