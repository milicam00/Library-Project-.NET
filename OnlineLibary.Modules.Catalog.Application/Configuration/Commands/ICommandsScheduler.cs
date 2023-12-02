using OnlineLibary.Modules.Catalog.Application.Contracts;

namespace OnlineLibary.Modules.Catalog.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}
