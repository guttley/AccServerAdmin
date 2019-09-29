using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppSettings> AppSettings { get; set; }
        public DbSet<Server> Servers { get; set; }

        public DbSet<ServerConfiguration> ServerConfigurations { get; set; }
        public DbSet<GameConfiguration> GameConfigurations { get; set; }
        public DbSet<EventConfiguration> EventConfigurations { get; set; }

    }
}
