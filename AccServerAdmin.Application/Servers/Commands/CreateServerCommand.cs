using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    using AccServerAdmin.Domain;

    public class CreateServerCommand : ICreateServerCommand
    {
        private readonly IServerRepository _serverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServerCommand(
            IServerRepository serverRepository,
            IUnitOfWork unitOfWork)
        {
            _serverRepository = serverRepository;
            _unitOfWork = unitOfWork;
           
        }

        public async Task<Server> ExecuteAsync(string serverName)
        {
            var server = new Server {Name = serverName};

            if (await _serverRepository.IsUniqueNameAsync(serverName).ConfigureAwait(false))
            {
                throw new NonUniqueNameException("Server names must be unique");
            }

            await _serverRepository.AddAsync(server);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return server;
        }
    }
}