namespace OnlineLibrary.Modules.Catalog.Infrastructure
{
    public interface IOutboxEmailSenderService
    {
        Task SendUnprocessedEmailsAsync();
    }
}
