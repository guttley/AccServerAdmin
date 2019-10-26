using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Sessions.Commands
{
    public class UpdateSessionCommand : IUpdateSessionCommand
    {
        private readonly IDataRepository<SessionConfiguration> _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServerConfigWriter _serverConfigWriter;

        public UpdateSessionCommand(
            IDataRepository<SessionConfiguration> sessionRepository,
            IUnitOfWork unitOfWork,
            IServerConfigWriter serverConfigWriter)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _serverConfigWriter = serverConfigWriter;
        }

        public async Task ExecuteAsync(Guid serverId, SessionConfiguration session)
        {
            _sessionRepository.Update(session.Id, session);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            await _serverConfigWriter.ExecuteAsync(serverId);
        }
    }
}
