using System;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class DeleteServerCommand : IDeleteServerCommand
    {
        private readonly IServerRepository _serverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServerCommand(
            IServerRepository serverRepository,
            IUnitOfWork unitOfWork)
        {
            _serverRepository = serverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid serverId)
        {
            _serverRepository.Delete(serverId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
