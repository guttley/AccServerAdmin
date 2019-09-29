using System.Threading.Tasks;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    using AccServerAdmin.Domain;

    public class CreateServerCommand : ICreateServerCommand
    {
        private readonly IDataRepository<AppSettings> _settingsRepository;
        private readonly IDataRepository<Server> _serverRepository;
        private readonly IDirectory _directory;
        private readonly IFile _file;

        public CreateServerCommand(
            IDataRepository<AppSettings> appSettingsRepository,
            IDataRepository<Server> serverRepository,
            IDirectory directory,
            IFile file)
        {
            _settingsRepository = appSettingsRepository;
            _serverRepository = serverRepository;
            _directory = directory;
            _file = file;
        }

        public async Task<Server> ExecuteAsync(string serverName)
        {
            var server = new Server {Name = serverName};
            await _serverRepository.AddAsync(server);

            /*
            var sourceFiles = _directory.GetFiles(_settings.ServerBasePath);

            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile = Path.Combine(server.Location, Path.GetFileName(sourceFile));
                _file.Copy(sourceFile, destinationFile);
            }
            */

            return server;
        }
    }
}
