using System;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.ServerConfig;

namespace AccServerAdmin.Application.ServerConfig.Commands
{
    public class SaveServerConfigCommand : ISaveServerConfigCommand
    {
        private readonly IServerDirectoryResolver _serverResolver;
        private readonly IServerConfigRepository _configRepository;

        public SaveServerConfigCommand(
            IServerDirectoryResolver serverResolver,
            IServerConfigRepository serverRepository)
        {
            _serverResolver = serverResolver;
            _configRepository = serverRepository;
        }

        public void Execute(Guid serverId, Configuration config)
        {
            var path = _serverResolver.Resolve(serverId);
            _configRepository.Save(path, config);
        }
    }
}
