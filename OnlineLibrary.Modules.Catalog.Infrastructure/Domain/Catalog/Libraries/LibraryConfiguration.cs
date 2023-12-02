using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;


namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Libraries
{
    public class LibraryConfiguration : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.HasKey(x => x.LibraryId);

            builder.Property(n=>n.LibraryName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(e => e.Books)
                .WithOne(e => e.Library)
                .HasForeignKey(e => e.LibraryId)
                .IsRequired();

            builder.HasOne(b => b.Owner)
                .WithMany(l => l.Libraries)
                .HasForeignKey(b => b.OwnerId);
        }
    }
}
