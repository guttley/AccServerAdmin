using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Persistence.Common;
using Microsoft.EntityFrameworkCore;


namespace AccServerAdmin.Application.Results.Queries
{
    public class TracksWithSessionsQuery : ITracksWithSessionsQuery
    {
        private readonly IDataRepository<Session> _sessionRepository;

        public TracksWithSessionsQuery(IDataRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }
            

        public async Task<IList<string>> Execute(int daysHistory)
        {
            var timestamp = DateTime.Now.Subtract(TimeSpan.FromDays(daysHistory));
            var sessions = _sessionRepository.GetQueryable().Where(s => s.SessionTimestamp > timestamp);

            return await sessions.Select(s => s.Track)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
