using Autofac;
using OnlineLibrary.BuildingBlocks.Application.Events;
using OutboxMessage = OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription.OutboxMessage;
using OnlineLibrary.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using OnlineLibrary.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OnlineLibrary.Modules.Catalog.Infrastructure.Configuration;

namespace OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    public class OutboxModule : Autofac.Module
    {
        private readonly BiDictionary<string, Type> _domainNotificationsMap;

        public OutboxModule(BiDictionary<string, Type> domainNotificationsMap)
        {
            _domainNotificationsMap = domainNotificationsMap;
        }

        protected override void Load(ContainerBuilder builder)
        {
          

            this.CheckMappings();

            builder.RegisterType<DomainNotificationsMapper>()
                .As<IDomainNotificationsMapper>()
                .FindConstructorsWith(new AllConstructorFinder())
                .WithParameter("domainNotificationsMap", _domainNotificationsMap)
                .SingleInstance();
        }

        private void CheckMappings()
        {
            var domainEventNotifications = Assemblies.Application
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IDomainEventNotification)))
                .ToList();

            List<Type> notMappedNotifications = new List<Type>();
            foreach (var domainEventNotification in domainEventNotifications)
            {
                _domainNotificationsMap.TryGetBySecond(domainEventNotification, out var name);

                if (name == null)
                {
                    notMappedNotifications.Add(domainEventNotification);
                }
            }

            if (notMappedNotifications.Any())
            {
                throw new ApplicationException($"Domain Event Notifications {notMappedNotifications.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
            }
        }
    }
}
