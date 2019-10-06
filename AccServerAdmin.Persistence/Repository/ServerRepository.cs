using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
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
            return await DbContext.Set<Server>()
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
            return await DbContext.Set<Server>()
                .Include(s => s.NetworkConfiguration)
                .Include(s => s.GameConfiguration)
                .Include(s => s.EventConfiguration)
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);
        }

        public override void Update(Guid id, Server updated)
        {
            base.Update(id, updated);

            var existingNs = DbContext.Set<NetworkConfiguration>().Find(updated.NetworkConfiguration.Id);
            var existingGs = DbContext.Set<GameConfiguration>().Find(updated.GameConfiguration.Id);
            var existingEs = DbContext.Set<EventConfiguration>().Find(updated.EventConfiguration.Id);

            if (existingNs != null)
            {
                DbContext.Entry(existingNs).CurrentValues.SetValues(updated.NetworkConfiguration);
            }

            if (existingGs != null)
            {
                DbContext.Entry(existingGs).CurrentValues.SetValues(updated.GameConfiguration);
            }

            if (existingEs != null)
            {
                DbContext.Entry(existingEs).CurrentValues.SetValues(updated.EventConfiguration);
            }
        }

        /// <inheritdoc />
        public async Task<bool> IsUniqueNameAsync(string serverName)
        {
            return await DbContext.Set<Server>()
                .AnyAsync(s => s.Name == serverName)
                .ConfigureAwait(false);
        }
    }
}
