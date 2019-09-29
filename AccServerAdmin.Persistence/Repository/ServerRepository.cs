using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
{
    public class ServerRepository : IServerRepository
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
            return await _dbContext.Servers
                .Include(s => s.NetworkConfiguration)
                .Include(s => s.GameConfiguration)
                .Include(s => s.EventConfiguration)
                .ThenInclude(e => e.Sessions)
                .FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Server> AddAsync(Server entity)
        {
            _dbContext.Servers.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
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

        /// <inheritdoc />
        public async Task<bool> IsUniqueNameAsync(string serverName)
        {
            return await _dbContext.Servers.AnyAsync(s => s.Name == serverName).ConfigureAwait(false);
        }
    }
}
