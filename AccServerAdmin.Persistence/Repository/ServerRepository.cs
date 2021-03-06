﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
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
        public override async Task<IEnumerable<Server>> GetAll()
        {
            return await DbContext.Set<Server>()
                .Include(s => s.NetworkCfg)
                .Include(s => s.GameCfg)
                .Include(s => s.EventRules)
                .Include(s => s.AssistRules)
                .Include(s => s.ServerBop)
                .Include(s => s.EventCfg).ThenInclude(e => e.Sessions)
                .Include(s => s.EntryList).ThenInclude(e => e.Entries).ThenInclude(e => e.Entries).ThenInclude(e => e.Driver)
                .ToListAsync();
        }

        /// <inheritdoc />
        public override async Task<Server> Get(Guid id)
        {
            return await DbContext.Set<Server>()
                .Include(s => s.NetworkCfg)
                .Include(s => s.GameCfg)
                .Include(s => s.EventRules)
                .Include(s => s.AssistRules)
                .Include(s => s.ServerBop)
                .Include(s => s.EventCfg).ThenInclude(e => e.Sessions)
                .Include(s => s.EntryList).ThenInclude(e => e.Entries).ThenInclude(e => e.Entries).ThenInclude(e => e.Driver)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public override void Update(Guid id, Server updated)
        {
            base.Update(id, updated);

            var existingNetCfg = DbContext.Set<NetworkCfg>().Find(updated.NetworkCfg.Id);
            var existingGameCfg = DbContext.Set<GameCfg>().Find(updated.GameCfg.Id);
            var existingEventCfg = DbContext.Set<EventCfg>().Find(updated.EventCfg.Id);
            var existingEventRules = DbContext.Set<EventRules>().Find(updated.EventRules.Id);
            var existingEntries = DbContext.Set<EntryList>().Find(updated.EntryList.Id);
            var existingAssistRules = DbContext.Set<AssistRules>().Find(updated.AssistRules.Id);

            if (existingNetCfg != null)
            {
                DbContext.Entry(existingNetCfg).CurrentValues.SetValues(updated.NetworkCfg);
            }

            if (existingGameCfg != null)
            {
                DbContext.Entry(existingGameCfg).CurrentValues.SetValues(updated.GameCfg);
            }

            if (existingEventCfg != null)
            {
                DbContext.Entry(existingEventCfg).CurrentValues.SetValues(updated.EventCfg);
            }

            if (existingEventRules != null)
            {
                DbContext.Entry(existingEventRules).CurrentValues.SetValues(updated.EventRules);
            }

            if (existingEntries != null)
            {
                DbContext.Entry(existingEntries).CurrentValues.SetValues(updated.EntryList);
            }

            if (existingAssistRules != null)
            {
                DbContext.Entry(existingAssistRules).CurrentValues.SetValues(updated.AssistRules);
            }
        }

        /// <inheritdoc />
        public async Task<bool> IsUniqueNameAsync(string serverName)
        {
            return await DbContext.Set<Server>()
                .AnyAsync(s => s.Name == serverName)
                ;
        }

        /// <inheritdoc />
        public async Task<bool> IsDuplicatePortsAsync(Guid serverId, int tcpPort, int udpPort)
        {
            var hasMatch = await DbContext.Set<Server>()
                .AnyAsync(s => s.Id != serverId && (s.NetworkCfg.TcpPort == tcpPort || s.NetworkCfg.UdpPort == udpPort))
                ;

            return hasMatch;
        }

    }
}
