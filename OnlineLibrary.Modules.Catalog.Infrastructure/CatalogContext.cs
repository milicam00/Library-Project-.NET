using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentalBooks.OwnerRentalBookSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentals.OwnerRentalSubscription;
using OutboxMessage = OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription.OutboxMessage;

namespace OnlineLibrary.Modules.Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<RentalBook> RentalBooks { get; set; }
        public DbSet<OwnerRental> OwnerRentals { get; set; }
        public DbSet<OwnerRentalBook> OwnerRentalBooks { get; set; }


        private readonly ILoggerFactory _loggerFactory;

        public CatalogContext(DbContextOptions<CatalogContext> options, ILoggerFactory loggerFactory)
           : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;Pooling=False";
            optionsBuilder.UseSqlServer(connection, b=>b.MigrationsAssembly("OnlineLibrary.API"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
           => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
