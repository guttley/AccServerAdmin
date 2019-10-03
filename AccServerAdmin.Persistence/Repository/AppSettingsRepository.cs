using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;

namespace AccServerAdmin.Persistence.Repository
{
    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class AppSettingsRepository : DataRepository<AppSettings>
    {
        public AppSettingsRepository(ApplicationDbContext dbContext)
        : base(dbContext)
        {
            
        }

        /// <inheritdoc />
        public override Task<AppSettings> GetAsync(Guid id)
        {
            throw new NotImplementedException("Use GetAll().FirstOrDefault()");
        }

        /// <inheritdoc />
        public override async Task AddAsync(AppSettings entity)
        {
            if (DbContext.AppSettings.Any())
                throw new InvalidOperationException("Cannot add a second app settings record");

            await base.AddAsync(entity);
        }

        
    }
}
