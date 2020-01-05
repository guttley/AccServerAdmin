using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace AccServerAdmin.Application.Entries.Queries
{
    public class EntryListReader : IEntryListReader
    {
        private readonly ILogger<EntryListReader> _logger;
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IJsonConverter _jsonConverter;

        public EntryListReader(
            ILogger<EntryListReader> logger,
            IGetAppSettingsQuery getAppSettingsQuery,
            IJsonConverter jsonConverter)
        {
            _logger = logger;
            _getAppSettingsQuery = getAppSettingsQuery;
            _jsonConverter = jsonConverter;
        }

        public async Task<List<Driver>> ExecuteAsync(Guid serverId)
        {
            var settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);
            var resultsPath = Path.Combine(settings.InstanceBasePath, serverId.ToString(), "results");
            var entries = Directory.EnumerateFiles(resultsPath, "*entryList.json").ToList();
            var allDrivers = new Dictionary<string, Driver>();

            _logger.LogDebug($"Server results path: {resultsPath}");
            _logger.LogDebug($"Found {entries.Count} entry lists");

            foreach (var entry in entries)
            {
                _logger.LogDebug($"Parsing file: {entry}");
                var entryList = ReadEntryFile(entry);
                var drivers = entryList.Entries.SelectMany(e => e.Drivers);

                foreach (var driver in drivers)
                {
                    if (allDrivers.ContainsKey(driver.PlayerId))
                        continue;

                    allDrivers.Add(driver.PlayerId, driver);
                    _logger.LogDebug($"Added driver {driver.Firstname} {driver.Lastname}");
                }
            }

            return allDrivers.Values.ToList();
        }

        private EntryList ReadEntryFile(string entryPath)
        {
            var json = File.ReadAllText(entryPath, Encoding.Unicode);
            var entryList = _jsonConverter.DeserializeObject<EntryList>(json);
            return entryList;
        }
    }
}
