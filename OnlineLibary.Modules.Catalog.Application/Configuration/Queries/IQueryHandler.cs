
using MediatR;
using OnlineLibary.Modules.Catalog.Application.Contracts;

namespace OnlineLibary.Modules.Catalog.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
