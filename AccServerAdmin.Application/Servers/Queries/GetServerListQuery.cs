using AccServerAdmin.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Queries
{
    public class GetServerListQuery : IGetServerListQuery
    {
        private readonly IDataRepository<Server> _serverRepository;

        public GetServerListQuery(IDataRepository<Server> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<IEnumerable<Server>> ExecuteAsync()
        {
            return await _serverRepository.GetAllAsync().ConfigureAwait(false);
        }
    }
}
