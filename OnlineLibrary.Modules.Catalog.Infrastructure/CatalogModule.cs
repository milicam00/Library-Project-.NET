using Autofac;
using MediatR;
using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.Modules.Catalog.Infrastructure.Configuration;

namespace OnlineLibrary.Modules.Catalog.Infrastructure
{
    public class CatalogModule : ICatalogModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using (var scope = LibraryCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            using (var scope = LibraryCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = LibraryCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
