using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Resouce;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class DeleteServerCommand : IDeleteServerCommand
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;

        public DeleteServerCommand(
            IOptions<AppSettings> options,
            IDirectory directory)
        {
            _settings = options.Value;
            _directory = directory;
        }

        public void Execute(Guid serverId)
        {
            var instanceDirs = _directory.GetDirectories(_settings.InstanceBasePath);
            var path = instanceDirs.FirstOrDefault(d => d.Contains(serverId.ToString()));

            if (string.IsNullOrEmpty(path))
                throw new KeyNotFoundException(string.Format(Strings.ServerIdNotFoundFormat, serverId));

            _directory.Delete(path, true);
        }
    }
}
