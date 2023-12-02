using OnlineLibrary.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.BuildingBlocks.Application.Events
{
    public class DomainNotificationBase<T> : IDomainEventNotification<T>
       where T : IDomainEvent
    {
        public T DomainEvent { get; }

        public Guid Id { get; }

        public DomainNotificationBase(T domainEvent, Guid id)
        {
            this.Id = id;
            this.DomainEvent = domainEvent;
        }
    }
}
