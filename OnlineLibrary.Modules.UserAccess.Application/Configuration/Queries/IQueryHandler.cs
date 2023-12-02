using MediatR;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
