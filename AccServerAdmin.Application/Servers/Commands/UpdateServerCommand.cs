using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly IDataRepository<Server> _serverRepository;

        public UpdateServerCommand(IDataRepository<Server> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task ExecuteAsync(Server server)
        {
            var dbServer = await _serverRepository.GetAsync(server.Id);
            await _serverRepository.UpdateAsync(dbServer, server);
        }
    }
}
