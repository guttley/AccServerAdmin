using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;

namespace AccServerAdmin.Application.Results.Queries
{
    public class GetImportableResultsQuery : IGetImportableResultsQuery
    {
        private readonly IServerPathResolver _serverPathResolver;
        private readonly Regex _fileMatcher = new Regex(@"\d{6}_\d{6}_F?[PQR]\d?.json");

        public GetImportableResultsQuery(IServerPathResolver serverPathResolver)
        {
            _serverPathResolver = serverPathResolver;
        }

        public async Task<bool> Execute(Guid serverId)
        {
            try
            {
                var path = await _serverPathResolver.Execute(serverId);
                var resultsPath = Path.Combine(path, "results");
                var hasImportableFiles = Directory.EnumerateFiles(resultsPath, "*").Any(f => _fileMatcher.IsMatch(f));

                return hasImportableFiles;
            }
            catch
            {
                return false;
            }
        }
    }
}
