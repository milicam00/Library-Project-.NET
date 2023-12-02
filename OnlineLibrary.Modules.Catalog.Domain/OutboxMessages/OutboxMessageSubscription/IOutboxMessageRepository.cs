namespace OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription
{

    public interface IOutboxMessageRepository
    {
        Task AddAsync(OutboxMessage outbox);
        Task<List<OutboxMessage>> GetAllUnprocessedEmailMessages();
        Task AddAsync(List<OutboxMessage> outboxs);
        void Update(OutboxMessage outbox);


    }

}
