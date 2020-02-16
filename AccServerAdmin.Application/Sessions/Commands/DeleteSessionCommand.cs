using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Sessions.Commands
{
    public class DeleteSessionCommand : IDeleteSessionCommand
    {
        private readonly IDataRepository<SessionConfiguration> _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSessionCommand(
            IDataRepository<SessionConfiguration> sessionRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid sessionId)
        {
            _sessionRepository.Delete(sessionId);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
