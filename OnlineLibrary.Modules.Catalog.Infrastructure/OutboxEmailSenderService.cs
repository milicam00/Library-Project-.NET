using Newtonsoft.Json;
using OnlineLibrary.BuildingBlocks.Application.Emails;
using OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription;



namespace OnlineLibrary.Modules.Catalog.Infrastructure
{
    public class OutboxEmailSenderService : IOutboxEmailSenderService
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IEmailService _emailService;
        public OutboxEmailSenderService(IOutboxMessageRepository outboxMessageRepository, IEmailService emailService)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _emailService = emailService;
        }

        public async Task SendUnprocessedEmailsAsync()
        {
            List<OutboxMessage> emailMessages = await _outboxMessageRepository.GetAllUnprocessedEmailMessages();
            foreach (OutboxMessage emailMessage in emailMessages)
            {
                string jsonData = emailMessage.Data;
                var deserializedData = JsonConvert.DeserializeObject<EmailData>(jsonData);
                await _emailService.SendEmailAsync(deserializedData.Recipient, deserializedData.Subject, deserializedData.Body);
                emailMessage.ProcessedDate = DateTime.Now;
                _outboxMessageRepository.Update(emailMessage);

            }
        }
    }
}
