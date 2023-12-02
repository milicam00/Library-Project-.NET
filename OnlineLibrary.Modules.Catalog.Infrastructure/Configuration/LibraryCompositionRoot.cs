using Autofac;


namespace OnlineLibrary.Modules.Catalog.Infrastructure.Configuration
{
    internal static class LibraryCompositionRoot
    {
        private static IContainer? _container;

        public static void SetContainer(Autofac.IContainer container)
        {
            _container = container;
        }

        internal static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }
    }
}
