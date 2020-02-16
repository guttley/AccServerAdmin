using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Application.Common
{
    public class ServerInstanceCreator : IServerInstanceCreator
    {
        private readonly IServerPathResolver _serverPathResolver;
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IDirectory _directory;
        private readonly IFile _file;

        public ServerInstanceCreator(
            IServerPathResolver serverPathResolver,
            IGetAppSettingsQuery getAppSettingsQuery,
            IDirectory directory,
            IFile file)
        {
            _serverPathResolver = serverPathResolver;
            _getAppSettingsQuery = getAppSettingsQuery;
            _directory = directory;
            _file = file;
        }

        public async Task<string> Execute(Server server)
        {
            var settings = await _getAppSettingsQuery.Execute().ConfigureAwait(false);
            var sourceFiles = _directory.GetFiles(settings.ServerBasePath).ToList();

            if (!sourceFiles.Any())
            {
                throw new EmptyDirectoryException($"The source directory \"{settings.ServerBasePath}\" is empty, it must be populated with the ACC server files");
            }

            var destinationPath = await _serverPathResolver.Execute(server.Id);
            var cfgPath = Path.Combine(destinationPath, "cfg");
            var logPath = Path.Combine(destinationPath, "log");
            var resultPath = Path.Combine(destinationPath, "results");
            var resultArchivePath = Path.Combine(resultPath,  "archive");


            if (!_directory.Exists(destinationPath))
            {
                _directory.CreateDirectory(destinationPath);
            }

            if (!_directory.Exists(cfgPath))
            {
                _directory.CreateDirectory(cfgPath);
            }

            if (!_directory.Exists(logPath))
            {
                _directory.CreateDirectory(logPath);
            }

            if (!_directory.Exists(resultPath))
            {
                _directory.CreateDirectory(resultPath);
            }

            if (!_directory.Exists(resultArchivePath))
            {
                _directory.CreateDirectory(resultArchivePath);
            }

            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile = Path.Combine(destinationPath, Path.GetFileName(sourceFile));
                _file.Copy(sourceFile, destinationFile);
            }

            return destinationPath;
        }
    }
}