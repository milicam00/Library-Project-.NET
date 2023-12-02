using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Infrastructure
{
    public class OverdueRentals
    {
        private CatalogContext _context;
        private IEmailService _emailService;

        public OverdueRentals(CatalogContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task AlertOverdueRentals()
        {
            var thirtyDays = DateTime.Now.AddDays(-30);
            List<Rental> overdueRentals = _context.Rentals
                .Where(rental => !rental.ReturnDate.HasValue && rental.RentalDate <= thirtyDays)
                .ToList();

            foreach (var overdue in overdueRentals)
            {
                Reader reader = _context.Readers.FirstOrDefault(x => x.ReaderId == overdue.ReaderId);
                if (reader != null)
                {
                    await _emailService.SendEmailAsync("mileticmilica246@gmail.com", "No returned books", "Please return books");
                }
            }
        }
    }
}
