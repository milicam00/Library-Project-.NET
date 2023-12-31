﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;

namespace OnlineLibrary.Modules.Catalog.Infrastructure.Domain.Catalog.Users
{
    public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.HasKey(x => x.ReaderId);

            builder.Property(n => n.FirstName)
               .HasMaxLength(75)
               .IsRequired();

            builder.Property(n => n.LastName)
                .HasMaxLength(75)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(p => p.UserName)
                .IsUnique();
        }
    }
}
