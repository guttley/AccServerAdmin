using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
{
    public class NetworkConfigurationRepository : IDataRepository<NetworkConfiguration>
    {
        private readonly ApplicationDbContext _dbContext;

        public NetworkConfigurationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<NetworkConfiguration>> GetAllAsync()
        {
            return await _dbContext.NetworkConfigurations.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<NetworkConfiguration> GetAsync(Guid serverId)
        {
            return await _dbContext.NetworkConfigurations.FirstOrDefaultAsync(s => s.ServerId == serverId).ConfigureAwait(false); ;
        }

        /// <inheritdoc />
        public async Task<NetworkConfiguration> AddAsync(NetworkConfiguration entity)
        {
            _dbContext.NetworkConfigurations.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(NetworkConfiguration dbEntity, NetworkConfiguration entity)
        {
            dbEntity.MaxClients = entity.MaxClients;
            dbEntity.RegisterToLobby = entity.RegisterToLobby;
            dbEntity.TcpPort = entity.TcpPort;
            dbEntity.UdpPort = entity.UdpPort;
            dbEntity.Version = entity.Version;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(NetworkConfiguration entity)
        {
            _dbContext.NetworkConfigurations.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
