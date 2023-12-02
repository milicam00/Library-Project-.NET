using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineLibrary.BuildingBlocks.Application.Data;
using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.BuildingBlocks.Application.ICsvGeneration;
using OnlineLibrary.BuildingBlocks.Application.IXlsxGeneration;
using OnlineLibrary.BuildingBlocks.Application.XmlGeneration;
using OnlineLibrary.BuildingBlocks.Infrastructure;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentals.OwnerRentalSubscription;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Books;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Libraries;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.OutboxMessages;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.OwnerRentals;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Owners;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.RentalBooks;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Rentals;
using OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Users;
using OnlineLibrary.Modules.UserAccess.Application.ResetPasswordRequest;
using IOutboxMessageRepository = OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription.IOutboxMessageRepository;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Configuration.DataAccess
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly bool _enableSsl;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        
      
        private readonly ILoggerFactory _loggerFactory;
        public DataAccessModule(string databaseConnectionString,string smtpServer,int smtpPort,bool enableSsl, string smtpUsername,string smtpPassword,ILoggerFactory loggerFactory)
        {
            _databaseConnectionString = databaseConnectionString;
            _loggerFactory = loggerFactory;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _enableSsl = enableSsl;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
           
          
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();
            builder.RegisterType<CsvGenerationService>().As<ICsvGenerationService>();
            builder.RegisterType<XlsxGenerationService>().As<IXlsxGenerationService>();
            builder.RegisterType<XmlGenerationService>().As<IXmlGenerationService>();
            builder.RegisterType<SmtpEmailService>()
                .As<IEmailService>()
                .WithParameter("smtpServer",_smtpServer)
                .WithParameter("smtpPort",_smtpPort)
                .WithParameter("enableSsl", _enableSsl)
                .WithParameter("smtpUsername",_smtpUsername)
                .WithParameter("smtpPassword",_smtpPassword)
             
                .InstancePerLifetimeScope();
      
            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);

                    return new CatalogContext(dbContextOptionsBuilder.Options, _loggerFactory);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookRepository>()
                .As<IBookRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LibraryRepository>()
                .As<ILibraryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RentalRepository>()
                .As<IRentalRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RentalBooksRepository>()
                .As<IRentalBooksRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OutboxMessageRepository>()
                .As<IOutboxMessageRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReaderRepository>()
                .As<IReaderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OwnerRepository>()
                .As<IOwnerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OwnerRentalRepository>()
                .As<IOwnerRentalRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
