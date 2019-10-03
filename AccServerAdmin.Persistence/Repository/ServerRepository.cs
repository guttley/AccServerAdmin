using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
{
    public class ServerRepository : DataRepository<Server>, IServerRepository
    {
        public ServerRepository(ApplicationDbContext dbContext)
         : base(dbContext)
        {
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await DbContext.Servers
                .Include(s => s.NetworkConfiguration)
                .Include(s => s.GameConfiguration)
                .Include(s => s.EventConfiguration)
                .ThenInclude(e => e.Sessions)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override async Task<Server> GetAsync(Guid id)
        {
            return await DbContext.Servers
                .Include(s => s.NetworkConfiguration)
                .Include(s => s.GameConfiguration)
                .Include(s => s.EventConfiguration)
                .ThenInclude(e => e.Sessions)
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> IsUniqueNameAsync(string serverName)
        {
            return await DbContext.Servers
                .AnyAsync(s => s.Name == serverName)
                .ConfigureAwait(false);
        }
    }
}
