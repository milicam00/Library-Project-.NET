using Microsoft.Extensions.Logging;
using OnlineLibrary.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;

namespace OnlineLibrary.BuildingBlocks.EventBus
{
    public class InMemoryEventBusClient : IEventsBus
    {
        private readonly Serilog.ILogger _logger;

        public InMemoryEventBusClient(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            _logger.Information("Publishing {Event}", @event.GetType().FullName);
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        public void StartConsuming()
        {
        }
    }
}
