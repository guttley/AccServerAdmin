using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Queries
{
    public class GetServerByIdQuery : IGetServerByIdQuery
    {
        private readonly IServerRepository _serverRepository;

        public GetServerByIdQuery(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }
        
        public async Task<Server> ExecuteAsync(Guid serverId)
        {
            return await _serverRepository.GetAsync(serverId).ConfigureAwait(false);
        }
    }
}
