using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace AccServerAdmin.Persistence.DbContext
{
    /// <summary>
    /// Used by PMC Db Migration.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var options = CreateDbContextOptions(System.IO.Directory.GetCurrentDirectory());
            return new ApplicationDbContext(options);
        }

        private DbContextOptions<ApplicationDbContext> CreateDbContextOptions(string basePath)
        {
            var config = GetConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("AzureSqlDb"));

            return optionsBuilder.Options;
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            if (environment != null)
            {
                builder.AddJsonFile($"appsettings.{environment}.json", optional: true);
            }

            return builder.Build();
        }
    }
}
