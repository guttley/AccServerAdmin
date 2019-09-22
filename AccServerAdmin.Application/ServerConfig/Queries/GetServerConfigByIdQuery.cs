using System;
using System.Collections.Generic;
using System.Linq;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Server;
using AccServerAdmin.Resouce;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Application.ServerConfig.Queries
{
    public class GetServerConfigByIdQuery : IGetServerByIdQuery
    {
        private readonly AppSettings _settings;
        private readonly IServerRepository _serverRepository;
        private readonly IDirectory _directory;

        public GetServerConfigByIdQuery(
            IOptions<AppSettings> options,
            IServerRepository serverRepository,
            IDirectory directory)
        {
            _settings = options.Value;
            _serverRepository = serverRepository;
            _directory = directory;
        }

        public Server Execute(Guid serverId)
        {
            var instanceDirs = _directory.GetDirectories(_settings.InstanceBasePath);

            var server = instanceDirs.Where(d => d.Contains(serverId.ToString()))
                                .Select(_serverRepository.Read)
                                .FirstOrDefault();

            if (server is null)
                throw new KeyNotFoundException(string.Format(Strings.ServerIdNotFoundFormat, serverId));

            return server;
        }
    }
}
