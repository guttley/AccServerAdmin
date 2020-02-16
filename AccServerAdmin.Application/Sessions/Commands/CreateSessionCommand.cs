using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Sessions.Commands
{
    public class CreateSessionCommand : ICreateSessionCommand
    {
        private readonly IDataRepository<SessionConfiguration> _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSessionCommand(
            IDataRepository<SessionConfiguration> sessionRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid serverId, SessionConfiguration session)
        {
            await _sessionRepository.Add(session).ConfigureAwait(false);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
