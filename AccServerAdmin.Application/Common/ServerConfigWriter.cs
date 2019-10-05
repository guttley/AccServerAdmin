using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Application.Common
{
    public class ServerConfigWriter : IServerConfigWriter
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;

        public ServerConfigWriter(
            IGetAppSettingsQuery getAppSettingsQuery,
            IFile file,
            IJsonConverter jsonConverter)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _file = file;
            _jsonConverter = jsonConverter;
        }

        public async Task ExecuteAsync(Server server)
        {
            var settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);
            var cfgPath = Path.Combine(settings.InstanceBasePath, server.Id.ToString(), "cfg");
            
            Save(server.NetworkConfiguration,  cfgPath, "configuration.json");
            Save(server.GameConfiguration, cfgPath, "settings.json");
            Save(server.EventConfiguration, cfgPath, "event.json");
        }

        private void Save<T>(T config, string cfgPath, string filename)
        {
            var json = _jsonConverter.SerializeObject(config);
            var path = Path.Combine(cfgPath, filename);
            _file.WriteAllText(path, json);
        }

    }
}
