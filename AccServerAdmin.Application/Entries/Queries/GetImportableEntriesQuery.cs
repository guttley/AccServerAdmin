using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Application.Entries.Queries
{
    public class GetImportableEntriesQuery : IGetImportableEntriesQuery
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IDirectory _directory;

        public GetImportableEntriesQuery(
            IGetAppSettingsQuery getAppSettingsQuery,
            IDirectory directory)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _directory = directory;
        }

        public async Task<bool> ExecuteAsync(Guid serverId)
        {
            try
            {
                var settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);
                var resultsPath = Path.Combine(settings.InstanceBasePath, serverId.ToString(), "results");
                var entries = Directory.EnumerateFiles(resultsPath, "*entryList.json");

                return entries.Any();
            }
            catch
            {
                return false;
            }
        }
    }
}
