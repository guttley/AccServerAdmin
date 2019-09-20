using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccServerAdmin.Application.Servers.Commands.DeleteServer
{
    using Domain;
    using Infrastructure.IO;

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
                throw new KeyNotFoundException();

            _directory.Delete(path, true);
        }
    }
}
