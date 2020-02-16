using System;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Application.Common
{
    public class ServerInstanceCleanUp : IServerInstanceCleanUp
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IDirectory _directory;

        public ServerInstanceCleanUp(
            IGetAppSettingsQuery getAppSettingsQuery,
            IDirectory directory)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _directory = directory;
        }

        public async Task Execute(Guid serverId)
        {
            var settings = await _getAppSettingsQuery.Execute();
            var path = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            _directory.Delete(path, true);
        }
    }
}
