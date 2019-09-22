using System;
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

        public T Execute(Guid serverId)
        {
            var path = _serverResolver.Resolve(serverId);
            var config = _configRepository.Read(path);

            return config;
        }
    }
}
