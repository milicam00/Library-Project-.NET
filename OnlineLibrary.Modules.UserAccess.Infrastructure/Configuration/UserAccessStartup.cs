using Autofac;
using MediatR;
using OnlineLibrary.BuildingBlocks.Application;
using OnlineLibrary.BuildingBlocks.Infrastructure.EventBus;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration.EventBus;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration.Mediation;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Serilog.Extensions.Logging;
using System.Reflection;
using ILogger = Serilog.ILogger;
namespace OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration
{
    public class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(
            //IContainer container,
            string connectionString, string smtpServer, int smtpPort, bool enableSsl, string smtpUsername, string smtpPassword,
            IExecutionContextAccessor executionContextAccessor,
            Serilog.ILogger logger,
            IEventsBus eventsBus
           )
        {
           
            ConfigureContainer(connectionString, smtpServer, smtpPort, enableSsl, smtpUsername, smtpPassword, logger, executionContextAccessor, eventsBus);
           
        }

        private static void ConfigureContainer(
            string connectionString,
             string smtpServer,
            int smtpPort,
            bool enableSsl,
            string smtpUsername,
            string smtpPassword,
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            IEventsBus eventsBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var loggerFactory = new SerilogLoggerFactory(logger);

            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, smtpServer, smtpPort, enableSsl, smtpUsername, smtpPassword, loggerFactory));
            containerBuilder.RegisterInstance(executionContextAccessor);

       

            _container = containerBuilder.Build();
            UserAccessCompositionRoot.SetContainer(_container);

        }
    }
}
