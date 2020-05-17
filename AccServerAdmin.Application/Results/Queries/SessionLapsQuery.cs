using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Results.Queries
{
    public class SessionLapsQuery : ISessionLapsQuery
    {
        private readonly IDataRepository<Session> _sessionRepository;
        private readonly IDataRepository<SessionLap> _lapRepository;

        public SessionLapsQuery(
            IDataRepository<Session> sessionRepository,
            IDataRepository<SessionLap> lapRepository)
        {
            _sessionRepository = sessionRepository;
            _lapRepository = lapRepository;
        }

        public async Task<Session> Execute(Guid sessionId, Guid carId)
        {
            var session = await _sessionRepository.Get(sessionId).ConfigureAwait(false);
            session.Laps = await _lapRepository.GetQueryable()
                .Include(l => l.Driver)
                .Where(l => l.Car.Id == carId).ToListAsync();

            return session;
        }
    }
}
