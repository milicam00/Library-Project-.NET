using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;

namespace OnlineLibrary.Modules.UserAccess.Infrastructure.Domain.APIKeys
{
    public class APIKeyConfiguration : IEntityTypeConfiguration<APIKey>
    {
        public void Configure(EntityTypeBuilder<APIKey> builder)
        {
            builder.HasKey(x => x.KeyId);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x=>x.Username)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
