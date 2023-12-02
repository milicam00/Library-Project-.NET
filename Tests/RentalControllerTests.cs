using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibary.Modules.Catalog.Application.Rentals.GetRentalBooks;
using OnlineLibrary.API.Modules.Catalog.Rental.Controllers;

namespace IntegrationTests
{
    [TestFixture]
    public class RentalControllerTests
    {
        private RentalController _rentalController;
        private Mock<ICatalogModule> _catalogModuleMock;

        [SetUp]
        public void Setup()
        {
            _catalogModuleMock = new Mock<ICatalogModule>();
            _rentalController = new RentalController(_catalogModuleMock.Object);
        }

        [Test]
        public async Task GetRentalBooks_WithValidData_ReturnsOkResult()
        {
            //Arrange
            var rentalId = Guid.NewGuid();
            var rentalIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            _catalogModuleMock.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetRentalBooksQuery>())).ReturnsAsync(rentalIds);

            //Act
            var actionResult = await _rentalController.GetRentalBooksAsync(rentalId) as OkObjectResult;

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));


        }
    }
}
