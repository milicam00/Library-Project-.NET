using Autofac;
using MediatR;
using OnlineLibrary.BuildingBlocks.Application.Events;
using OnlineLibrary.BuildingBlocks.Infrastructure;
using OnlineLibrary.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using System.Reflection;

namespace OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventsAccessor>()
                .As<IDomainEventsAccessor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandsScheduler>()
                .As<ICommandsScheduler>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

            //builder.RegisterGenericDecorator(
            //    typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>),
            //    typeof(ICommandHandler<,>));

            //builder.RegisterGenericDecorator(
            //    typeof(ValidationCommandHandlerDecorator<>),
            //    typeof(ICommandHandler<>));

            //builder.RegisterGenericDecorator(
            //    typeof(ValidationCommandHandlerWithResultDecorator<,>),
            //    typeof(ICommandHandler<,>));

            //builder.RegisterGenericDecorator(
            //    typeof(LoggingCommandHandlerDecorator<>),
            //    typeof(ICommandHandler<>));

            //builder.RegisterGenericDecorator(
            //    typeof(LoggingCommandHandlerWithResultDecorator<,>),
            //    typeof(ICommandHandler<,>));

            //builder.RegisterGenericDecorator(
            //    typeof(DomainEventsDispatcherNotificationHandlerDecorator<>),
            //    typeof(INotificationHandler<>));

            builder.RegisterAssemblyTypes(Assemblies.Application)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
