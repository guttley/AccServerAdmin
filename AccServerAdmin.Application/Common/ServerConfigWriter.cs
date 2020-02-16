using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.Bop.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Application.Common
{
    public class ServerConfigWriter : IServerConfigWriter
    {
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;
        private readonly IGetBopListQuery _getBopListQuery;

        public ServerConfigWriter(
            IFile file,
            IJsonConverter jsonConverter,
            IGetBopListQuery getBopListQuery)
        {
            _file = file;
            _jsonConverter = jsonConverter;
            _getBopListQuery = getBopListQuery;
        }

        public async Task Execute(Server server, string serverPath)
        {
            var cfgPath = Path.Combine(serverPath, "cfg");

            var globalBop = new GlobalBop
            {
                BopList = (await _getBopListQuery.Execute(server.Id).ConfigureAwait(false)).ToList()
            };

            Save(server.NetworkCfg,  cfgPath, "configuration.json");
            Save(server.GameCfg, cfgPath, "settings.json");
            Save(server.EventCfg, cfgPath, "event.json");
            Save(server.EventRules, cfgPath, "eventRules.json");
            Save(server.EntryList, cfgPath, "entrylist.json");
            Save(globalBop, cfgPath, "bop.json");
        }

        private void Save<T>(T config, string cfgPath, string filename)
        {
            var json = _jsonConverter.SerializeObject(config);
            var path = Path.Combine(cfgPath, filename);
            _file.WriteAllText(path, json);
        }

    }
}
