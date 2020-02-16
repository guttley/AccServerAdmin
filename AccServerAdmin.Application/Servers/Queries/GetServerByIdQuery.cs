using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Servers.Queries
{
    public class GetServerByIdQuery : IGetServerByIdQuery
    {
        private readonly IServerRepository _serverRepository;

        public GetServerByIdQuery(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }
        
        public async Task<Server> Execute(Guid serverId)
        {
            var server = await _serverRepository.Get(serverId).ConfigureAwait(false);

            server.EventCfg.Sessions =
                server.EventCfg.Sessions
                    .OrderBy(s => s.DayOfWeekend)
                    .ThenBy(s => s.HourOfDay)
                    .ToList();
            
            return server;
        }
    }
}
