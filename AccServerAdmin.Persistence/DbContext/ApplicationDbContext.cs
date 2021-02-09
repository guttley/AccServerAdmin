using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Domain.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AccServerAdmin.Persistence.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
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

            builder.Entity<SessionCarDriver>()
                   .HasKey(cd => new { cd.DriverId, cd.SessionCarId});

            builder.Entity<SessionCarDriver>()
                .HasOne(cd => cd.Car)
                .WithMany(cd => cd.Drivers)
                .HasForeignKey(cd => cd.SessionCarId);

            builder.Entity<SessionCarDriver>()
                .HasOne(cd => cd.Driver)
                .WithMany(cd => cd.SessionCars)
                .HasForeignKey(cd => cd.DriverId);
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
        public DbSet<AssistRules> AssistRules { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionCar> SessionCars { get; set; }
        public DbSet<SessionCarDriver> SessionCarDrivers { get; set; }
        public DbSet<SessionLap> SessionLaps { get; set; }
        public DbSet<SessionPenalty> SessionPenalties { get; set; }
        public DbSet<LeaderboardLine> LeaderboardLines { get; set; }
        public DbSet<GlobalEntryList> GlobalEntries { get; set; }
    }
}
