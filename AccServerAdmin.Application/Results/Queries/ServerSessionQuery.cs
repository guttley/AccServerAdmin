using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Results.Queries
{
    public class ServerSessionQuery : IServerSessionQuery
    {
        private readonly IDataRepository<Session> _sessionRepository;

        public ServerSessionQuery(IDataRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public Task<IEnumerable<Session>> Execute()
        {
            return _sessionRepository.GetAll();
        }
        
    }
}
