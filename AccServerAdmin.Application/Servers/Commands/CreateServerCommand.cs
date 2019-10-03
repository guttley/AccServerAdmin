using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    using AccServerAdmin.Domain;
    using System.IO;

    public class CreateServerCommand : ICreateServerCommand
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IServerRepository _serverRepository;
        private readonly IDirectory _directory;
        private readonly IFile _file;

        public CreateServerCommand(
            IGetAppSettingsQuery getAppSettingsQuery,
            IServerRepository serverRepository,
            IDirectory directory,
            IFile file)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _serverRepository = serverRepository;
            _directory = directory;
            _file = file;
        }

        public async Task<Server> ExecuteAsync(string serverName)
        {
            var server = new Server {Name = serverName};

            if (await _serverRepository.IsUniqueNameAsync(serverName).ConfigureAwait(false))
            {
                throw new NonUniqueNameException("Server names must be unique");
            }

            var settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);
            var sourceFiles = _directory.GetFiles(settings.ServerBasePath);

            if (!sourceFiles.Any())
            {
                throw new EmptyDirectoryException($"The source directory \"{settings.ServerBasePath}\" is empty, it must be populated with the ACC server files");
            }

            var destinationPath = Path.Combine(settings.ServerBasePath, server.Id.ToString());
            await _serverRepository.AddAsync(server);
            await _serverRepository.SaveAsync();

            if (!_directory.Exists(destinationPath))
            {
                _directory.CreateDirectory(destinationPath);
            }

            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile = Path.Combine(destinationPath, Path.GetFileName(sourceFile));
                _file.Copy(sourceFile, destinationFile);
            }

            return server;
        }
    }
}
