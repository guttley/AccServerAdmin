using System.IO;
using System.Linq;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Application.Common
{
    public class ServerConfigWriter : IServerConfigWriter
    {
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;

        public ServerConfigWriter(
            IFile file,
            IJsonConverter jsonConverter)
        {
            _file = file;
            _jsonConverter = jsonConverter;
        }

        public void Execute(Server server, string serverPath)
        {
            var cfgPath = Path.Combine(serverPath, "cfg");

            // Must order the Sessions P, Q, R or the server will complain
            server.EventCfg.Sessions = server.EventCfg.Sessions.OrderBy(s => s.SessionType).ToList();

            Save(server.NetworkCfg,  cfgPath, "configuration.json");
            Save(server.GameCfg, cfgPath, "settings.json");
            Save(server.EventCfg, cfgPath, "event.json");
            Save(server.EventRules, cfgPath, "eventRules.json");
            Save(server.EntryList, cfgPath, "entrylist.json");
        }

        private void Save<T>(T config, string cfgPath, string filename)
        {
            var json = _jsonConverter.SerializeObject(config);
            var path = Path.Combine(cfgPath, filename);
            _file.WriteAllText(path, json);
        }

    }
}
