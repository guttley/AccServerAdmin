using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
{
    public class ServerRepository : IDataRepository<Server>
    {
        private readonly ApplicationDbContext _dbContext;

        public ServerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await _dbContext.Servers.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Server> GetAsync(Guid id)
        {
            return await _dbContext.Servers.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task AddAsync(Server entity)
        {
            _dbContext.Servers.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Server dbEntity, Server entity)
        {
            dbEntity.Name = entity.Name;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Server entity)
        {
            _dbContext.Servers.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
