using System;
using AccServerAdmin.Persistence.Server;
using AccServerAdmin.Application.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly IServerDirectoryResolver _serverResolver;
        private readonly IServerRepository _serverRepository;

        public UpdateServerCommand(
            IServerDirectoryResolver serverResolver,
            IServerRepository serverRepository)
        {
            _serverResolver = serverResolver;
            _serverRepository = serverRepository;
        }

        public void Execute(Guid serverId, string serverName)
        {
            var serverPath = _serverResolver.Resolve(serverId);
            var server = _serverRepository.Read(serverPath);

            server.Name = serverName;
            _serverRepository.Save(server);
        }
    }
}
