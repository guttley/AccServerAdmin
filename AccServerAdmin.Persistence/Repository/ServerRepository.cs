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
                .Include(s => s.NetworkCfg)
                .Include(s => s.GameCfg)
                .Include(s => s.EventRules)
                .Include(s => s.EventCfg)
                .ThenInclude(e => e.Sessions)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override async Task<Server> GetAsync(Guid id)
        {
            return await DbContext.Set<Server>()
                .Include(s => s.NetworkCfg)
                .Include(s => s.GameCfg)
                .Include(s => s.EventRules)
                .Include(s => s.EventCfg)                
                .ThenInclude(e => e.Sessions)
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);
        }

        public override void Update(Guid id, Server updated)
        {
            base.Update(id, updated);

            var existingNs = DbContext.Set<NetworkCfg>().Find(updated.NetworkCfg.Id);
            var existingGs = DbContext.Set<GameCfg>().Find(updated.GameCfg.Id);
            var existingEs = DbContext.Set<EventCfg>().Find(updated.EventCfg.Id);
            var existingRs = DbContext.Set<EventRules>().Find(updated.EventRules.Id);

            if (existingNs != null)
            {
                DbContext.Entry(existingNs).CurrentValues.SetValues(updated.NetworkCfg);
            }

            if (existingGs != null)
            {
                DbContext.Entry(existingGs).CurrentValues.SetValues(updated.GameCfg);
            }

            if (existingEs != null)
            {
                DbContext.Entry(existingEs).CurrentValues.SetValues(updated.EventCfg);
            }

            if (existingRs != null)
            {
                DbContext.Entry(existingRs).CurrentValues.SetValues(updated.EventRules);
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
