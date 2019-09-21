using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Server;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AccServerAdmin.Application.Servers.Queries.GetServerList
{
    public class GetServerListQuery : IGetServerListQuery
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;
        private readonly IServerRepository _serverRepository;
        private readonly Regex _guidRegex;

        public GetServerListQuery(
            IOptions<AppSettings> options,
            IServerRepository serverRepository,
            IDirectory directory)
        {
            _settings = options.Value;
            _serverRepository = serverRepository;
            _directory = directory;
            _guidRegex = new Regex("(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}");
        }

        private IEnumerable<Server> GetServersFromDirectories(IEnumerable<string> instanceDirectories)
        {
            return instanceDirectories.Where(IsCorrectlyNamedInstanceDirectory)
                                      .Select(_serverRepository.Read)
                                      .Where(s => s != null)
                                      .ToList();
        }

        private bool IsCorrectlyNamedInstanceDirectory(string directory)
        {
            return _guidRegex.IsMatch(directory);
        }

        public IEnumerable<Server> Execute()
        {
            var instanceDirs = _directory.GetDirectories(_settings.InstanceBasePath);
            return GetServersFromDirectories(instanceDirs);
        }
    }
}
