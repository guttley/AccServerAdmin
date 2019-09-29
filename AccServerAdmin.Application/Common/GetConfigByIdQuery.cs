using System;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Common
{
    public class GetConfigByIdQuery<T> : IGetConfigByIdQuery<T> where T : new()
    {
        private readonly IServerDirectoryResolver _serverResolver;
        private readonly IConfigRepository<T> _configRepository;

        public GetConfigByIdQuery(
            IServerDirectoryResolver serverResolver,
            IConfigRepository<T> configRepository)
        {
            _serverResolver = serverResolver;
            _configRepository = configRepository;
        }

        public async Task<T> ExecuteAsync(Guid serverId)
        {
            var path = await _serverResolver.ResolveAsync(serverId).ConfigureAwait(false); 
            var config = _configRepository.Read(path);
            return config;
        }
    }
}
