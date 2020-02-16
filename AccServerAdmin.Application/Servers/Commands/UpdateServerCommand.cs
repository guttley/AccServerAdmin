using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly IServerRepository _serverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServerCommand(
            IServerRepository serverRepository,
            IUnitOfWork unitOfWork)
        {
            _serverRepository = serverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Server server)
        {
            _serverRepository.Update(server.Id, server);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
