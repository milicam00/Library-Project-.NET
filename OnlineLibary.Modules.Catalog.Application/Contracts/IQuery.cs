using MediatR;

namespace OnlineLibary.Modules.Catalog.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
