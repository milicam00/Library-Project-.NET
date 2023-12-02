using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutboxMessage = OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription.OutboxMessage;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.OutboxMessages
{
    public class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
