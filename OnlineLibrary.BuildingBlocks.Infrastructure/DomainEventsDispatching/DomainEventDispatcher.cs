using Autofac;
using Autofac.Core;
using MediatR;
using Newtonsoft.Json;
using OnlineLibrary.BuildingBlocks.Application.Events;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.BuildingBlocks.Infrastructure.Serialization;

namespace OnlineLibrary.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;

        private readonly ILifetimeScope _scope;


        private readonly IDomainEventsAccessor _domainEventsProvider;

        private readonly IDomainNotificationsMapper _domainNotificationsMapper;

        public DomainEventsDispatcher(
            IMediator mediator,
            ILifetimeScope scope,
            IDomainEventsAccessor domainEventsProvider,
            IDomainNotificationsMapper domainNotificationsMapper)
        {
            _mediator = mediator;
            _scope = scope;
            _domainEventsProvider = domainEventsProvider;
            _domainNotificationsMapper = domainNotificationsMapper;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEvents = _domainEventsProvider.GetAllDomainEvents();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent),
                    new NamedParameter("id", domainEvent.Id)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            _domainEventsProvider.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = _domainNotificationsMapper.GetName(domainEventNotification.GetType());
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContactResolver()
                });

              
            }
        }
    }
}
