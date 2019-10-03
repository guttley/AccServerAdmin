using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly IServerRepository _serverRepository;

        public UpdateServerCommand(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task ExecuteAsync(Server server)
        {
            _serverRepository.Update(server);
            await _serverRepository.SaveAsync().ConfigureAwait(false);
        }
    }
}
