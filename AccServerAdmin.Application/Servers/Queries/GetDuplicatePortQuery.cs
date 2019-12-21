using System;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Queries
{
    public class GetDuplicatePortQuery : IGetDuplicatePortQuery
    {
        private readonly IServerRepository _serverRepository;

        public GetDuplicatePortQuery(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<bool> ExecuteAsync(Guid serverId, int tcpPort, int udpPort)
        {
            return await _serverRepository.IsDuplicatePortsAsync(serverId, tcpPort, udpPort).ConfigureAwait(false);
        }
    }
}
