using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AccServerAdmin.Application.Servers.Commands.UpdateServer
{
    using Domain;
    using Infrastructure.IO;
    using Persistence.Server;
    using System.Linq;

    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;
        private readonly IServerRepository _serverRepository;

        public UpdateServerCommand(
            IOptions<AppSettings> options,
            IDirectory directory,
            IServerRepository serverRepository)
        {
            _settings = options.Value;
            _directory = directory;
            _serverRepository = serverRepository;
        }


        public void Execute(Guid serverId, string serverName)
        {
            var server = _directory.GetDirectories(_settings.InstanceBasePath)
                            .Where(d => d.Contains(serverId.ToString()))
                            .Select(_serverRepository.Read)
                            .FirstOrDefault();

            if (server is null)
                throw new KeyNotFoundException();

            server.Name = serverName;
            _serverRepository.Save(server);
        }
    }
}
