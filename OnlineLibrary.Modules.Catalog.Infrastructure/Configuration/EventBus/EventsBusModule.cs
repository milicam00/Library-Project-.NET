using Autofac;
using OnlineLibrary.BuildingBlocks.EventBus;
using OnlineLibrary.BuildingBlocks.Infrastructure.EventBus;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Configuration.EventBus
{
    internal class EventsBusModule : Autofac.Module
    {
        private readonly IEventsBus _eventsBus;

        public EventsBusModule(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_eventsBus != null)
            {
                builder.RegisterInstance(_eventsBus).SingleInstance();
            }
            else
            {
                builder.RegisterType<InMemoryEventBusClient>()
                .As<IEventsBus>()
                .SingleInstance();
            }
        }
    }
}
