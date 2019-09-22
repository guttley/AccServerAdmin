using System;
using System.Collections.Generic;
using System.Linq;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Resouce;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Application.Common
{
    public class ServerDirectoryResolver : IServerDirectoryResolver
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;

        public ServerDirectoryResolver(
            IOptions<AppSettings> options,
            IDirectory directory)
        {
            _settings = options.Value;
            _directory = directory;
        }

        public string Resolve(Guid serverId)
        {
            var path = _directory.GetDirectories(_settings.InstanceBasePath)
                                 .FirstOrDefault(d => d.Contains(serverId.ToString()));
            
            if (string.IsNullOrEmpty(path))
                throw new KeyNotFoundException(string.Format(Strings.ServerIdNotFoundFormat, serverId));

            return path;
        }
    }
}
