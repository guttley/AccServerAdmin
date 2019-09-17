using System.IO;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Infrastructure.Helpers
{
    using AccServerAdmin.Domain;
    using AccServerAdmin.Infrastructure.IO;

    public class ServerSetup : IServerSetup
    {
        private readonly AppSettings _settings;
        private readonly IFile _file;
        private readonly IDirectory _directory;

        public ServerSetup(
            IOptions<AppSettings> settings,
            IDirectory directory,
            IFile file)
        {
            _settings = settings.Value;
            _directory = directory;
            _file = file;
        }

        /// <inheritdoc />
        public void Execute(Server server)
        {
            var sourceFiles = _directory.GetFiles(_settings.ServerBasePath);
            
            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile = Path.Combine(server.Location, Path.GetFileName(sourceFile));
                _file.Copy(sourceFile, destinationFile);
            }

        }
    }
}
