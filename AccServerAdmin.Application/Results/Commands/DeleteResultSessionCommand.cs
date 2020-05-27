using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Results.Commands
{
    public class DeleteResultSessionCommand : IDeleteResultSessionCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataRepository<Session> _sessionRepository;

        public DeleteResultSessionCommand(
            IUnitOfWork unitOfWork,
            IDataRepository<Session> sessionRepository)
        {
            _unitOfWork = unitOfWork;
            _sessionRepository = sessionRepository;
        }

        public async Task Execute(Guid sessionId)
        {
            _sessionRepository.Delete(sessionId);
            await _unitOfWork.SaveChanges();
        }
    }
}
