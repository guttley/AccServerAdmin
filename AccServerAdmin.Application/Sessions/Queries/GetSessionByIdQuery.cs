using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Sessions.Queries
{
    public class GetSessionByIdQuery : IGetSessionByIdQuery
    {
        private readonly IDataRepository<SessionConfiguration> _sessionRepository;

        public GetSessionByIdQuery(IDataRepository<SessionConfiguration> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<SessionConfiguration> Execute(Guid sesisonId)
        {
            return await _sessionRepository.Get(sesisonId);
        }
    }
}
