using OnlineLibrary.Modules.UserAccess.Application.BlockUser;

namespace OnlineLibrary.Modules.UserAccess.Application.Contracts
{
    public interface IUserAccessModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);
        

        //Task ExecuteCommandAsync(AddAdminUserCommand addAdminUserCommand);
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
