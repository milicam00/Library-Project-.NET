﻿using Autofac;
using MediatR;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration;

namespace OnlineLibrary.Modules.UserAccess.Infrastructure
{
    public class UserAccessModule : IUserAccessModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using (var scope = UserAccessCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            using (var scope = UserAccessCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = UserAccessCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
