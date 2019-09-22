using System;
using System.Collections.Generic;
using System.Linq;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Server;
using AccServerAdmin.Resouce;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Application.ServerConfig.Commands
{
    public class UpdateServerConfigCommand : IUpdateServerConfigCommand
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;
        private readonly IServerRepository _serverRepository;

        public UpdateServerConfigCommand(
            IOptions<AppSettings> options,
            IServerRepository serverRepository,
            IDirectory directory)
        {
            _settings = options.Value;
            _serverRepository = serverRepository;
            _directory = directory;
        }

        public void Execute(Guid serverId, Configuration config)
        {
            var server = _directory.GetDirectories(_settings.InstanceBasePath)
                            .Where(d => d.Contains(serverId.ToString()))
                            .Select(_serverRepository.Read)
                            .FirstOrDefault();

            if (server is null)
                throw new KeyNotFoundException(string.Format(Strings.ServerIdNotFoundFormat, serverId));

            //server.Name = serverName;
            //_serverRepository.Save(server);
        }

    }
}
