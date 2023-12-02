using OnlineLibary.Modules.Catalog.Application.Contracts;

namespace OnlineLibrary.Modules.Administration.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}
