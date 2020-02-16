using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccResults;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace AccServerAdmin.Application.Common
{
    public class ResultImporter
    {
        private readonly ILogger<ResultImporter> _logger;
        private readonly IDriverRepository _driverRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServerPathResolver _serverPathResolver;
        private readonly IJsonConverter _jsonConverter;
        private readonly IFile _file;

        public ResultImporter(
            ILogger<ResultImporter> logger,
            IDriverRepository driverRepository,
            ISessionRepository sessionRepository,
            IUnitOfWork unitOfWork,
            IServerPathResolver serverPathResolver,
            IJsonConverter jsonConverter,
            IFile file)
        {
            _logger = logger;
            _driverRepository = driverRepository;
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _serverPathResolver = serverPathResolver;
            _jsonConverter = jsonConverter;
            _file = file;
        }


        public async Task Execute(Guid serverId)
        {
            var path = await _serverPathResolver.Execute(serverId).ConfigureAwait(false);
            var resultPath = Path.Combine(path, "results");
            var resultArchivePath = Path.Combine(resultPath, "archive");
            var resultFiles = GetResultFiles(resultPath);

            foreach (var resultFile in resultFiles)
            {
                try
                {
                    await ProcessResults(resultFile);

                    ArchiveResultFile(resultArchivePath, resultFile);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error caught processing result file: {0}", resultFile);
                }
            }

            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }

        private void ArchiveResultFile(string archivePath, string resultFile)
        {
            var filename = Path.GetFileName(resultFile);
            var archiveFile = Path.Combine(archivePath, filename);
            _file.Copy(resultFile, archiveFile);
            _file.Delete(resultFile);
        }

        private IEnumerable<string> GetResultFiles(string path)
        {
            return Directory.EnumerateFiles(path, "*.json")
                .Where(f => f.Contains("p.json", StringComparison.OrdinalIgnoreCase) ||
                            f.Contains("q.json", StringComparison.OrdinalIgnoreCase) ||
                            f.Contains("r.json", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private async Task ProcessResults(string resultFile)
        {
            var json = _file.ReadAllText(resultFile);
            var results = _jsonConverter.DeserializeObject<ResultFile>(json);

            // First import drivers
            await ImportDrivers(results).ConfigureAwait(false);
            
            // Import the session
            await ImportSession(resultFile, results).ConfigureAwait(false);

        }

        private async Task ImportSession(string resultFile, ResultFile results)
        {
            var session = new Session
            {
                ImportFile = resultFile,
                SessionTimestamp = GetSessionTimestamp(resultFile),
                SessionType = results.SessionType,
                Track = results.TrackName,
                IsWet = results.SessionResult.IsWetSession > 0
            };

            await _sessionRepository.Add(session).ConfigureAwait(false);
        }

        private DateTime GetSessionTimestamp(string resultFile)
        {
            var parts = resultFile.Split("_");

            var year = int.Parse(parts[0].Substring(0, 2));
            var month = int.Parse(parts[0].Substring(2, 2));
            var day = int.Parse(parts[0].Substring(4, 2));

            var hour = int.Parse(parts[1].Substring(0, 2));
            var minute = int.Parse(parts[1].Substring(2, 2));
            var second = int.Parse(parts[1].Substring(4, 2));

            return new DateTime(year, month, day, hour, minute, second);

        }

        private async Task ImportDrivers(ResultFile results)
        {
            var drivers = results.SessionResult.Leaderboard.SelectMany(l => l.Car.Drivers).ToList();

            _logger.LogInformation($"Importing {drivers.Count} drivers");

            foreach (var driver in drivers)
            {
                var existingDriver = await _driverRepository.GetQueryable().AnyAsync(d => d.PlayerId == driver.PlayerId);

                if (existingDriver)
                {
                    _logger.LogDebug($"Driver already exists: {driver.PlayerId} - {driver.Firstname} {driver.Lastname}");
                }
                else
                {
                    var configDriver = new Domain.AccConfig.Driver
                    {
                        PlayerId = driver.PlayerId,
                        Firstname = driver.Firstname,
                        Lastname = driver.Lastname,
                        Shortname = driver.Shortname
                    };
                      
                    await _driverRepository.Add(configDriver).ConfigureAwait(false);
                    _logger.LogDebug($"Driver imported: {driver.PlayerId} - {driver.Firstname} {driver.Lastname}");
                }
            }

            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
