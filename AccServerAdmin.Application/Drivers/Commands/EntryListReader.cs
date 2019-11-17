using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public class EntryListReader : IEntryListReader
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IJsonConverter _jsonConverter;

        public EntryListReader(
            IGetAppSettingsQuery getAppSettingsQuery,
            IJsonConverter jsonConverter)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _jsonConverter = jsonConverter;
        }

        public async Task<List<Driver>> ExecuteAsync(Guid serverId)
        {
            var settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);
            var resultsPath = Path.Combine(settings.InstanceBasePath, serverId.ToString(), "results");
            var entries = Directory.EnumerateFiles(resultsPath, "*entryList.json");
            var allDrivers = new Dictionary<string, Driver>();

            foreach (var entry in entries)
            {
                var entryList = ReadEntryFile(entry);
                var drivers = entryList.Entries.SelectMany(e => e.Drivers);

                foreach (var driver in drivers)
                {
                    if (allDrivers.ContainsKey(driver.PlayerId))
                        continue;

                    allDrivers.Add(driver.PlayerId, driver);
                }
            }

            return allDrivers.Values.ToList();
        }

        private EntryList ReadEntryFile(string entryPath)
        {
            var json = File.ReadAllText(entryPath);
            var entryList = _jsonConverter.DeserializeObject<EntryList>(json);
            return entryList;
        }
    }
}
