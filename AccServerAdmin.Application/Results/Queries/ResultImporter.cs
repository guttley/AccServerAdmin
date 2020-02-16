using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Domain.AccResults;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Notifications.Results;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Results.Queries
{
    public class ResultImporter : IResultImporter
    {
        private readonly IHubContext<ResultImportHub, IResultImport> _hubContext;
        private readonly IDriverRepository _driverRepository;
        private readonly IDataRepository<Session> _sessionRepository;
        private readonly IDataRepository<SessionDriver> _sessionDriverRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServerPathResolver _serverPathResolver;
        private readonly IJsonConverter _jsonConverter;
        private readonly IFile _file;
        private readonly List<SessionDriver> _driverCache = new List<SessionDriver>();
        private readonly Regex _fileMatcher = new Regex(@"\d{6}_\d{6}_F?[PQR]\d?.json");

        private string _serverName;


        public ResultImporter(
            IHubContext<ResultImportHub, IResultImport> hubContext,
            IDriverRepository driverRepository,
            IDataRepository<Session> sessionRepository,
            IDataRepository<SessionDriver> sessionDriverRepository,
            IUnitOfWork unitOfWork,
            IServerPathResolver serverPathResolver,
            IJsonConverter jsonConverter,
            IFile file)
        {
            _hubContext = hubContext;
            _driverRepository = driverRepository;
            _sessionRepository = sessionRepository;
            _sessionDriverRepository = sessionDriverRepository;
            _unitOfWork = unitOfWork;
            _serverPathResolver = serverPathResolver;
            _jsonConverter = jsonConverter;
            _file = file;
        }


        public async Task Execute(Guid serverId, string serverName)
        {
            _serverName = serverName;
            _driverCache.AddRange(_sessionDriverRepository.GetQueryable().AsNoTracking().Include("Driver"));

            var path = await _serverPathResolver.Execute(serverId);
            var resultPath = Path.Combine(path, "results");
            var resultArchivePath = Path.Combine(resultPath, "archive");
            var resultFiles = GetResultFiles(resultPath).ToList();

            await _hubContext.Clients.All.ImportMessage($"Found {resultFiles.Count} files to import");

            foreach (var resultFile in resultFiles)
            {
                try
                {
                    await ProcessResults(resultFile);

                    ArchiveResultFile(resultArchivePath, resultFile);
                }
                catch (Exception ex)
                {
                    await _hubContext.Clients.All.ImportMessage($"Error caught processing result file: {resultFile}, Error: {ex.Message}");
                }
            }

            await _hubContext.Clients.All.ImportMessage("Import Complete");
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
            return Directory.EnumerateFiles(path, "*")
                .Where(f => _fileMatcher.IsMatch(f))
                .ToList();
        }

        private async Task ProcessResults(string resultFile) 
        {
            var json = _file.ReadAllText(resultFile);
            var results = _jsonConverter.DeserializeObject<ResultFile>(json);

            // First import drivers
            await ImportDrivers(results);
            
            // Import the session
            var session = await ImportSession(resultFile, results);

            if (session == null)
            {
                await _hubContext.Clients.All.ImportMessage($"Ignoring result file {resultFile} as it appears to already be imported");
                return;
            }

            // Import the laps
            await ImportLaps(session, results);

            await _unitOfWork.SaveChanges();
        }

        private async Task ImportLaps(Session session, ResultFile results)
        {
            var cars = results.SessionResult.Leaderboard.Select(l => l.Car).ToList();
            var laps = results.Laps.OrderBy(l => l.CarId).ToList();

            foreach (var lap in laps)
            {
                var car = cars.FirstOrDefault(c => c.CarId == lap.CarId);
                var sessionDriver = GetSessionDriver(car);

                var sessionLap = new SessionLap
                {
                    Driver = sessionDriver,
                    Laptime = lap.LapTime,
                    Split1 = lap.Splits.Count > 0 ? lap.Splits[0] : 0,
                    Split2 = lap.Splits.Count > 1 ? lap.Splits[1] : 0,
                    Split3 = lap.Splits.Count > 2 ? lap.Splits[2] : 0
                };

                session.Laps.Add(sessionLap);
                //await _hubContext.Clients.All.ImportMessage($"Imported lap: {sessionDriver.Driver.Fullname} - {ListData.Cars[(CarModel)car.CarModel]} - {TimeSpan.FromMilliseconds(sessionLap.Laptime):mm\\:ss\\.fff}");
            }

            await _hubContext.Clients.All.ImportMessage($"Imported laps: {laps.Count} from session at {session.SessionTimestamp}");
        }

        private SessionDriver GetSessionDriver(Car car)
        {
            var playerId = car.Drivers.FirstOrDefault()?.PlayerId;

            var sessionDriver = _driverCache.FirstOrDefault(sd => sd.Driver.PlayerId == playerId &&
                                                                  sd.CarModel == (CarModel) car.CarModel &&
                                                                  sd.RaceNumber == car.RaceNumber &&
                                                                  sd.CupCategory == car.CupCategory &&
                                                                  sd.TeamName == car.TeamName);

            if (sessionDriver == null)
            {
                sessionDriver = new SessionDriver
                {
                    Driver = _driverRepository.GetQueryable().FirstOrDefault(d => d.PlayerId == playerId),
                    CarModel = (CarModel) car.CarModel,
                    RaceNumber = car.RaceNumber,
                    CupCategory = car.CupCategory,
                    TeamName = car.TeamName
                };

                _driverCache.Add(sessionDriver);
                _sessionDriverRepository.Add(sessionDriver);
            }

            return sessionDriver;
        }

        private async Task<Session> ImportSession(string resultFile, ResultFile results)
        {
            var timestamp = GetSessionTimestamp(resultFile);

            if (await _sessionRepository.GetQueryable().AnyAsync(s => s.SessionTimestamp == timestamp))
            {
                return null;
            }

            var session = new Session
            {
                ServerName = _serverName,
                SessionTimestamp = timestamp,
                SessionType = results.SessionType,
                Track = results.TrackName,
                IsWet = results.SessionResult.IsWetSession > 0
            };

            await _sessionRepository.Add(session);

            return session;
        }

        private DateTime GetSessionTimestamp(string resultFile)
        {
            var parts = Path.GetFileName(resultFile).Split("_");

            var year = 2000 + int.Parse(parts[0].Substring(0, 2));
            var month = int.Parse(parts[0].Substring(2, 2));
            var day = int.Parse(parts[0].Substring(4, 2));

            var hour = int.Parse(parts[1].Substring(0, 2));
            var minute = int.Parse(parts[1].Substring(2, 2));
            var second = int.Parse(parts[1].Substring(4, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }

        private async Task ImportDrivers(ResultFile results)
        {
            var drivers = results.SessionResult.Leaderboard
                .SelectMany(l => l.Car.Drivers)
                .OrderBy(d => d.Fullname)
                .ToList();

            await _hubContext.Clients.All.ImportMessage($"Importing {drivers.Count} drivers");

            foreach (var driver in drivers)
            {
                try
                {
                    var existingDriver = await _driverRepository.GetQueryable().AnyAsync(d => d.PlayerId == driver.PlayerId);

                    if (existingDriver)
                    {
                        //await _hubContext.Clients.All.ImportMessage($"Driver already exists: {driver.PlayerId} - {driver.Fullname}");
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

                        await _driverRepository.Add(configDriver);
                        await _hubContext.Clients.All.ImportMessage($"Driver imported: {driver.PlayerId} - {driver.Fullname}");
                        await _unitOfWork.SaveChanges();
                    }
                } 
                catch (Exception ex)
                {
                    await _hubContext.Clients.All.ImportMessage($"Error caught importing driver: {driver?.Fullname}, Error: {ex.Message}");
                }
            }

            
        }
    }
}
