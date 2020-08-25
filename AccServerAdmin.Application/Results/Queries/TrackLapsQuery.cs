using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Results.Queries
{
    public class TrackLapsQuery : ITrackLapsQuery
    {
        private readonly IDataRepository<Session> _sessionRepository;
        private readonly IDataRepository<SessionLap> _lapRepository;

        public TrackLapsQuery(
            IDataRepository<Session> sessionRepository,
            IDataRepository<SessionLap> lapRepository)
        {
            _sessionRepository = sessionRepository;
            _lapRepository = lapRepository;
        }

        public async Task<IList<SessionLap>> Execute(string track, int daysHistory)
        {
            var timestamp = DateTime.Now.Subtract(TimeSpan.FromDays(daysHistory));
            var sessions = _sessionRepository.GetQueryable()
                .Where(s => s.Track == track && s.SessionType != "R" && s.SessionType != "R2" && s.SessionTimestamp > timestamp)
                .Select(s => s.Id);

            var allLaps = await _lapRepository.GetQueryable()
                .Include(l => l.Driver)
                .Include(l => l.Car)
                .Where(l => l.Valid && sessions.Contains(l.SessionId))
                .ToListAsync();

            var drivers = allLaps.Select(l => l.Driver.Id).Distinct();
            var laps = new List<SessionLap>();

            foreach (var driverId in drivers)
            {
                var driverLaps = allLaps.Where(l => l.Driver.Id == driverId);
                var carLaps = driverLaps.GroupBy(l => l.Car.CarModel)
                                        .Select(l => l.OrderBy(x => x.LapTime));

                laps.AddRange(carLaps.Select(carLap => carLap.First()));
            }

            return laps;
        }
    }
}
