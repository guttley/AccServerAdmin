using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Server;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AccServerAdmin.Application.Servers.Queries.GetServerList
{
    public class GetServerListQuery : IGetServerListQuery
    {
        private readonly AppSettings _settings;
        private readonly IServerPersistence _serverPersistence;
        private readonly Regex _guidRegex;

        public GetServerListQuery(
            IServerPersistence serverPersistence,
            IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _serverPersistence = serverPersistence;
            _guidRegex = new Regex("(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}");
        }

        private IEnumerable<Server> GetServersFromDirectories(IEnumerable<string> instanceDirectories)
        {
            return instanceDirectories.Where(IsCorrectlyNamedInstanceDirectory)
                                      .Select(_serverPersistence.Read)
                                      .Where(s => s != null)
                                      .ToList();
        }

        private bool IsCorrectlyNamedInstanceDirectory(string directory)
        {
            return _guidRegex.IsMatch(directory);
        }

        public IEnumerable<Server> Execute()
        {
            var instanceDirs = Directory.GetDirectories(_settings.InstanceBasePath).ToImmutableList();
            return GetServersFromDirectories(instanceDirs);
        }
    }
}
