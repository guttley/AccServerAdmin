using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Resouce;

namespace AccServerAdmin.Application.Common
{
    using AccServerAdmin.Domain;

    public class ServerDirectoryResolver : IServerDirectoryResolver
    {
        private readonly IDataRepository<AppSettings> _appSettingsRepository;
        private readonly IDirectory _directory;

        public ServerDirectoryResolver(
            IDataRepository<AppSettings> appSettingsRepository,
            IDirectory directory)
        {
            _appSettingsRepository = appSettingsRepository;
            _directory = directory;
        }

        public async Task<string> ResolveAsync(Guid serverId)
        {
            var settings = (await _appSettingsRepository.GetAllAsync().ConfigureAwait(false)).FirstOrDefault();

            var path = _directory.GetDirectories(settings.InstanceBasePath)
                                 .FirstOrDefault(d => d.Contains(serverId.ToString()));
            
            if (string.IsNullOrEmpty(path))
                throw new KeyNotFoundException(string.Format(Strings.ServerIdNotFoundFormat, serverId));

            return path;
        }
    }
}
