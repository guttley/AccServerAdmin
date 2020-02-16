using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Sessions.Commands
{
    public class UpdateSessionCommand : IUpdateSessionCommand
    {
        private readonly IDataRepository<SessionConfiguration> _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSessionCommand(
            IDataRepository<SessionConfiguration> sessionRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid serverId, SessionConfiguration session)
        {
            _sessionRepository.Update(session.Id, session);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
