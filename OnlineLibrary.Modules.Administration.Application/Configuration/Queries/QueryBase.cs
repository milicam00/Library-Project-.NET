using OnlineLibary.Modules.Catalog.Application.Contracts;

namespace OnlineLibrary.Modules.Administration.Application.Configuration.Queries
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        protected QueryBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected QueryBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
