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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Driver>()
                .HasIndex(d => d.PlayerId)
                .IsUnique();

            builder.Entity<DriverEntry>()
                .HasKey(de => new {de.EntryId, de.DriverId});

            builder.Entity<DriverEntry>()
                .HasOne(de => de.Entry)
                .WithMany(e => e.Entries)
                .HasForeignKey(de => de.EntryId);

            builder.Entity<DriverEntry>()
                   .HasOne(de => de.Driver)
                   .WithMany(e => e.Entries)
                   .HasForeignKey(de => de.DriverId);

            builder.Entity<Session>()
                .HasIndex(s => s.SessionTimestamp)
                .IsUnique();

            builder.Entity<Session>()
                .HasIndex(s => s.Track);
        }

        //
        //  These are required to generate the migration
        //
        public DbSet<AppSettings> AppSettings { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<NetworkCfg> NetworkCfgs { get; set; }
        public DbSet<GameCfg> GameCfgs { get; set; }
        public DbSet<EventCfg> EventCfgs { get; set; }
        public DbSet<EventRules> EventRules { get; set; }
        public DbSet<EntryList> EntryList { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverEntry> DriverEntries { get; set; }
        public DbSet<BalanceOfPerformance> BalanceOfPerformance { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<AssistRules> AssistRules { get; set; }

    }
}
