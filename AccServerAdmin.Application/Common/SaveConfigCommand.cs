using System;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Common
{
    public class SaveConfigCommand<T> : ISaveConfigCommand<T> where T : new()
    {
        private readonly IServerDirectoryResolver _serverResolver;
        private readonly IConfigRepository<T> _configRepository;

        public SaveConfigCommand(
            IServerDirectoryResolver serverResolver,
            IConfigRepository<T> configRepository)
        {
            _serverResolver = serverResolver;
            _configRepository = configRepository;
        }

        public void Execute(Guid serverId, T config)
        {
            var path = _serverResolver.Resolve(serverId);
            _configRepository.Save(path, config);
        }
    }
}
