using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Domain.AccResults;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Notifications.Results;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Results
{
    public class ResultImporter : IResultImporter
    {
        private readonly IHubContext<ResultImportHub, IResultImport> _hubContext;
        private readonly IDriverRepository _driverRepository;
        private readonly IDataRepository<Session> _sessionRepository;
        private readonly IDataRepository<SessionCar> _sessionCarRepository;
        private readonly IDataRepository<SessionPenalty> _sessionPenaltyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServerPathResolver _serverPathResolver;
        private readonly IJsonConverter _jsonConverter;
        private readonly IFile _file;
        private readonly List<SessionCar> _driverCache = new List<SessionCar>();
        private readonly Regex _fileMatcher = new Regex(@"\d{6}_\d{6}_F?[PQR]\d?.json");

        private string _serverName;


        public ResultImporter(
            IHubContext<ResultImportHub, IResultImport> hubContext,
            IDriverRepository driverRepository,
            IDataRepository<Session> sessionRepository,
            IDataRepository<SessionCar> sessionCarRepository,
            IDataRepository<SessionPenalty> sessionPenaltyRepository,
            IUnitOfWork unitOfWork,
            IServerPathResolver serverPathResolver,
            IJsonConverter jsonConverter,
            IFile file)
        {
            _hubContext = hubContext;
            _driverRepository = driverRepository;
            _sessionRepository = sessionRepository;
            _sessionCarRepository = sessionCarRepository;
            _sessionPenaltyRepository = sessionPenaltyRepository;
            _unitOfWork = unitOfWork;
            _serverPathResolver = serverPathResolver;
            _jsonConverter = jsonConverter;
            _file = file;
        }


        public async Task Execute(Guid serverId, string serverName)
        {
            _serverName = serverName;

            var path = await _serverPathResolver.Execute(serverId);
            var resultPath = Path.Combine(path, "results");
            var resultArchivePath = Path.Combine(resultPath, "archive");
            var resultFiles = GetResultFiles(resultPath).ToList();

            await _hubContext?.Clients.All.ImportMessage($"Found {resultFiles.Count} files to import");

            foreach (var resultFile in resultFiles)
            {
                _driverCache.Clear();

                try
                {
                    await ProcessResults(resultFile);

                    ArchiveResultFile(resultArchivePath, resultFile);
                }
                catch (Exception ex)
                {
                    await _hubContext?.Clients.All.ImportMessage($"Error caught processing result file: {resultFile}, Error: {ex.InnerException?.Message ?? ex.Message}");
                }
            }

            await _hubContext?.Clients.All.ImportMessage("Import Complete");
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
                await _hubContext?.Clients.All.ImportMessage($"Ignoring result file {resultFile} as it appears to already be imported");
                return;
            }

            var cars = await ImportSessionCars(session, results);
            await ImportLaps(session, cars, results);
            await ImportPenalties(session, cars, results);
            await ImportLeaderBoard(session, cars, results);

            await _unitOfWork.SaveChanges();
        }
        
        private SessionCar GetCar(Session session, ResultFile results, int carId)
        {
            var car = results.SessionResult.Leaderboard.FirstOrDefault(l => l.Car.CarId == carId);
            var drivers = car.Car.Drivers.Select(d => GetDriver(d.PlayerId)).ToList();

            var sessionCar = new SessionCar
            {
                SessionId = session.Id,
                CarModel = (CarModel)car.Car.CarModel,
                CupCategory = car.Car.CupCategory,
                RaceNumber = car.Car.RaceNumber,
                TeamName = car.Car.TeamName
            };

            sessionCar.Drivers = drivers.Select(d => new SessionCarDriver
            {
                Car = sessionCar, 
                SessionCarId = sessionCar.Id,
                Driver = d,
                DriverId = d.Id
            }).ToList();

            return sessionCar;
        }

        private Domain.AccConfig.Driver GetDriver(string playerId)
        {
            return _driverRepository.GetQueryable().FirstOrDefault(p => p.PlayerId == playerId);
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

        private async Task<Dictionary<int, SessionCar>> ImportSessionCars(Session session, ResultFile results)
        {
            var sessionCars = new Dictionary<int, SessionCar>();
            var cars = results.SessionResult.Leaderboard.Select(l => l.Car).ToList();

            foreach (var car in cars)
            {
                var sessionCar = GetCar(session, results, (int)car.CarId);

                sessionCars.Add((int)car.CarId, sessionCar);
                await _sessionCarRepository.Add(sessionCar);
            }


            await _hubContext?.Clients.All.ImportMessage($"Imported session cars: {sessionCars.Count} from session at {session.SessionTimestamp}");
            return sessionCars;
        }

        private async Task ImportPenalties(Session session, Dictionary<int, SessionCar> cars, ResultFile results)
        {
            var penalties = results.Penalties.OrderBy(p => p.ViolationInLap);

            foreach (var penalty in penalties)
            {
                var car = cars[(int)penalty.CarId];

                var sessionPenalty = new SessionPenalty
                {
                    SessionId = session.Id,
                    Car = car,
                    Penalty = penalty.PenaltyString,
                    PenaltyValue = (int)penalty.PenaltyValue,
                    Reason = penalty.Reason,
                    ViolationInLap = (int)penalty.ViolationInLap,
                    ClearedInLap = (int)penalty.ClearedInLap
                };

                await _sessionPenaltyRepository.Add(sessionPenalty);
            }

            await _hubContext?.Clients.All.ImportMessage($"Imported penalties: {penalties.Count()} from session at {session.SessionTimestamp}");
        }

        private async Task ImportLaps(Session session, Dictionary<int, SessionCar> cars, ResultFile results)
        {
            var laps = results.Laps.OrderBy(l => l.CarId).ToList();
            var lapNumbers = new Dictionary<Guid, int>();

            foreach (var lap in laps)
            {
                var car = cars[(int)lap.CarId];
                var driver = car.Drivers[(int) lap.DriverIndex];

                if (!lapNumbers.ContainsKey(driver.SessionCarId))
                {
                    lapNumbers.Add(driver.SessionCarId, 0);
                }

                var sessionLap = new SessionLap
                {
                    SessionId = session.Id,
                    Car = car,
                    Driver = driver.Driver,
                    Lap = ++lapNumbers[driver.SessionCarId],
                    Valid = lap.IsValidForBest,
                    LapTime = lap.LapTime,
                    Split1 = lap.Splits.Count > 0 ? lap.Splits[0] : 0,
                    Split2 = lap.Splits.Count > 1 ? lap.Splits[1] : 0,
                    Split3 = lap.Splits.Count > 2 ? lap.Splits[2] : 0
                };

                session.Laps.Add(sessionLap);
                //await _hubContext.Clients.All.ImportMessage($"Imported lap: {sessionDriver.Driver.Fullname} - {ListData.Cars[(CarModel)car.CarModel]} - {TimeSpan.FromMilliseconds(sessionLap.Laptime):mm\\:ss\\.fff}");
            }

            await _hubContext.Clients.All.ImportMessage($"Imported laps: {laps.Count} from session at {session.SessionTimestamp}");
        }

        private async Task ImportLeaderBoard(Session session, Dictionary<int, SessionCar> cars, ResultFile results)
        {
            var leaders = results.SessionResult.Leaderboard.OrderBy(l => l.Timing.BestLap).ToList();
            var i = 0;

            foreach (var l in leaders)
            {
                var car = cars[(int)l.Car.CarId];

                var leader = new LeaderboardLine
                {
                    Car = car,
                    CurrentDriver = GetDriver(l.CurrentDriver.PlayerId),
                    BestLap = (int) l.Timing.BestLap,
                    BestSplit1 = (int) (l.Timing.BestSplits.Count > 0 ? l.Timing.BestSplits[0] : 0),
                    BestSplit2 = (int) (l.Timing.BestSplits.Count > 1 ? l.Timing.BestSplits[1] : 0),
                    BestSplit3 = (int) (l.Timing.BestSplits.Count > 2 ? l.Timing.BestSplits[2] : 0),
                    LastLap = (int) l.Timing.BestLap,
                    LastSplit1 = (int) (l.Timing.LastSplits.Count > 0 ? l.Timing.LastSplits[0] : 0),
                    LastSplit2 = (int) (l.Timing.LastSplits.Count > 1 ? l.Timing.LastSplits[1] : 0),
                    LastSplit3 = (int) (l.Timing.LastSplits.Count > 2 ? l.Timing.LastSplits[2] : 0),
                    LapCount = (int) l.Timing.LapCount,
                    TotalTime = (int) l.Timing.TotalTime,
                    MissingMandatoryPitstop = l.MissingMandatoryPitstop > 0,
                };

                session.LeaderBoard.Add(leader);
                i++;
            }


            await _hubContext?.Clients.All.ImportMessage($"Imported leaderboard lines: {i} from session at {session.SessionTimestamp}");
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
                IsWet = results.SessionResult.IsWetSession > 0,
                BestLap = (int)results.SessionResult.Bestlap,
                BestSplit1 = (int)(results.SessionResult.BestSplits.Count != 0 ? results.SessionResult.BestSplits[0] : 0),
                BestSplit2 = (int)(results.SessionResult.BestSplits.Count > 0 ? results.SessionResult.BestSplits[1] : 0),
                BestSplit3 = (int)(results.SessionResult.BestSplits.Count > 1 ? results.SessionResult.BestSplits[2] : 0),
            };

            await _sessionRepository.Add(session);

            return session;
        }

        private async Task ImportDrivers(ResultFile results)
        {
            var drivers = results.SessionResult.Leaderboard
                .SelectMany(l => l.Car.Drivers)
                .OrderBy(d => d.Fullname)
                .ToList();

            await _hubContext?.Clients.All.ImportMessage($"Importing {drivers.Count} drivers");

            foreach (var driver in drivers)
            {
                try
                {
                    var existingDriver = await _driverRepository.GetQueryable().AnyAsync(d => d.PlayerId == driver.PlayerId);

                    if (!existingDriver)
                    {
                        var configDriver = new Domain.AccConfig.Driver
                        {
                            PlayerId = driver.PlayerId,
                            Firstname = driver.Firstname,
                            Lastname = driver.Lastname,
                            Shortname = driver.Shortname
                        };

                        await _driverRepository.Add(configDriver);
                        await _hubContext?.Clients.All.ImportMessage($"Driver imported: {driver.PlayerId} - {driver.Fullname}");
                        await _unitOfWork.SaveChanges();
                    }
                } 
                catch (Exception ex)
                {
                    await _hubContext?.Clients.All.ImportMessage($"Error caught importing driver: {driver?.Fullname}, Error: {ex.Message}");
                }
            }

            
        }
    }
}
