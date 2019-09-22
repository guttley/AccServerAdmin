using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Server;
using System.Linq;
using AccServerAdmin.Resouce;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;
        private readonly IServerRepository _serverRepository;

        public UpdateServerCommand(
            IOptions<AppSettings> options,
            IServerRepository serverRepository,
            IDirectory directory)
        {
            _settings = options.Value;
            _serverRepository = serverRepository;
            _directory = directory;
        }

        public void Execute(Guid serverId, string serverName)
        {
            var server = _directory.GetDirectories(_settings.InstanceBasePath)
                            .Where(d => d.Contains(serverId.ToString()))
                            .Select(_serverRepository.Read)
                            .FirstOrDefault();

            if (server is null)
                throw new KeyNotFoundException(string.Format(Strings.ServerIdNotFoundFormat, serverId));

            server.Name = serverName;
            _serverRepository.Save(server);
        }
    }
}
