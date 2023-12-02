using Autofac.Core.Activators.Reflection;
using System.Collections.Concurrent;
using System.Reflection;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Configuration
{
    internal class AllConstructorFinder : IConstructorFinder
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
           new ConcurrentDictionary<Type, ConstructorInfo[]>();

        public ConstructorInfo[] FindConstructors(Type targetType)
        {
            var result = Cache.GetOrAdd(
                targetType,
                t => t.GetTypeInfo().DeclaredConstructors.ToArray());

            return  result;
        }
    }
}
