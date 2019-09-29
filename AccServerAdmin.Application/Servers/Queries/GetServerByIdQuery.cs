using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Queries
{
    public class GetServerByIdQuery : IGetServerByIdQuery
    {
        private readonly IDataRepository<Server> _serverRepository;

        public GetServerByIdQuery(IDataRepository<Server> serverRepository)
        {
            _serverRepository = serverRepository;
        }
        
        public async Task<Server> ExecuteAsync(Guid serverId)
        {
            return await _serverRepository.GetAsync(serverId);
        }
    }
}
