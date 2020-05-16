using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Results.Queries
{
    public class SessionResultQuery : ISessionResultQuery
    {
        private readonly IDataRepository<Session> _sessionRepository;

        public SessionResultQuery(IDataRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Session> Execute(Guid sessionId)
        {
            var session =  await _sessionRepository.GetQueryable()
                //.Include(s => s.Laps).ThenInclude(l => l.Driver)
                .Include(s => s.LeaderBoard).ThenInclude(lb => lb.Car)
                .Include(s => s.LeaderBoard).ThenInclude(lb => lb.CurrentDriver)
                //.Include(s => s.Penalties).ThenInclude(p => p.Car).ThenInclude(c => c.Drivers)
                //.Include(s => s.Penalties)
                .FirstOrDefaultAsync(s => s.Id == sessionId).ConfigureAwait(false);

            return session;
        }
    }
}

