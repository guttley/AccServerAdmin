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

        public async Task<IList<SessionLap>> Execute(string track)
        {
            var sessions = _sessionRepository.GetQueryable()
                .Where(s => s.Track == track && s.SessionType != "R" && s.SessionType != "R2")
                .Select(s => s.Id);

            var driverLaps = await _lapRepository.GetQueryable()
                .Include(l => l.Driver)
                .Include(l => l.Car)
                .Where(l => l.Valid && sessions.Contains(l.SessionId))
                .ToListAsync();
            
            var laps = driverLaps.GroupBy(l => l.Driver.Id)
                .Select(d => d.OrderBy(l => l.LapTime))
                .Select(l => l.First()).ToList();

            return laps;
        }
    }
}
