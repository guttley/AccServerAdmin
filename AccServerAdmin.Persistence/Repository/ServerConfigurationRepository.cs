using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
{
    public class ServerConfigurationRepository : IDataRepository<ServerConfiguration>
    {
        private readonly ApplicationDbContext _dbContext;

        public ServerConfigurationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ServerConfiguration>> GetAllAsync()
        {
            return await _dbContext.ServerConfigurations.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ServerConfiguration> GetAsync(Guid serverId)
        {
            return await _dbContext.ServerConfigurations.FirstOrDefaultAsync(s => s.ServerId == serverId);
        }

        /// <inheritdoc />
        public async Task AddAsync(ServerConfiguration entity)
        {
            _dbContext.ServerConfigurations.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(ServerConfiguration dbEntity, ServerConfiguration entity)
        {
            dbEntity.MaxClients = entity.MaxClients;
            dbEntity.RegisterToLobby = entity.RegisterToLobby;
            dbEntity.TcpPort = entity.TcpPort;
            dbEntity.UdpPort = entity.UdpPort;
            dbEntity.Version = entity.Version;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(ServerConfiguration entity)
        {
            _dbContext.ServerConfigurations.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
