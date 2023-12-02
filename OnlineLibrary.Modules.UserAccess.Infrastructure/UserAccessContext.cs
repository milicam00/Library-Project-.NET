using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Modules.UserAccess.Domain.APIKey;
using OnlineLibrary.Modules.UserAccess.Domain.Users;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Domain.APIKeys;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Domain.RefreshTokens;
using OnlineLibrary.Modules.UserAccess.Infrastructure.Domain.Users;

namespace OnlineLibrary.Modules.UserAccess.Infrastructure
{
    public class UserAccessContext : DbContext
    {
        public DbSet<User> Users { get; set; }
       
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<APIKey> APIKeys { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public UserAccessContext (DbContextOptions options,ILoggerFactory loggerFactory)
            :base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new APIKeyConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;Pooling=False";
            optionsBuilder.UseSqlServer(connection, b => b.MigrationsAssembly("OnlineLibrary.API"));
        }
    }
}
