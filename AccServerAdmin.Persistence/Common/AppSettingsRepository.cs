using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Common
{
    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class AppSettingsRepository : IDataRepository<AppSettings>
    {
        private readonly ApplicationDbContext _dbContext;

        public AppSettingsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AppSettings>> GetAllAsync()
        {
            return await _dbContext.AppSettings.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<AppSettings> GetAsync(Guid id)
        {
            throw new NotImplementedException("Use GetAll().FirstOrDefault()");
        }

        /// <inheritdoc />
        public async Task<AppSettings> AddAsync(AppSettings entity)
        {
            if (_dbContext.AppSettings.Any())
                throw new InvalidOperationException("Cannot add a second app settings record");

            _dbContext.AppSettings.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(AppSettings dbEntity, AppSettings entity)
        {
            dbEntity.InstanceBasePath = entity.InstanceBasePath;
            dbEntity.ServerBasePath = entity.ServerBasePath;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(AppSettings entity)
        {
            _dbContext.AppSettings.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
