using MediatR;
using OnlineLibary.Modules.Catalog.Application.Contracts;

namespace OnlineLibrary.Modules.Administration.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
