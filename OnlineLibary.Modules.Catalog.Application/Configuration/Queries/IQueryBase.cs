using OnlineLibary.Modules.Catalog.Application.Contracts;

namespace OnlineLibary.Modules.Catalog.Application.Configuration.Queries
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        public Guid Id { get; }

        protected QueryBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected QueryBase(Guid id)
        {
            this.Id = id;
        }
    }
}
