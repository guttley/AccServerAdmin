using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly IServerRepository _serverRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServerConfigWriter _serverConfigWriter;

        public UpdateServerCommand(
            IServerRepository serverRepository,
            IUnitOfWork unitOfWork,
            IServerConfigWriter serverConfigWriter)
        {
            _serverRepository = serverRepository;
            _unitOfWork = unitOfWork;
            _serverConfigWriter = serverConfigWriter;
        }

        public async Task ExecuteAsync(Server server)
        {
            _serverRepository.Update(server.Id, server);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            await _serverConfigWriter.ExecuteAsync(server);
        }
    }
}
