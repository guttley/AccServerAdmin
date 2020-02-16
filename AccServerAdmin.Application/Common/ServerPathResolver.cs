using System;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;

namespace AccServerAdmin.Application.Common
{
    public class ServerPathResolver : IServerPathResolver
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;

        public ServerPathResolver(IGetAppSettingsQuery getAppSettingsQuery)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
        }
        
        public async Task<string> Execute(Guid serverId)
        {
            var settings = await _getAppSettingsQuery.Execute().ConfigureAwait(false);
            var destinationPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            return destinationPath;
        }
    }
}
