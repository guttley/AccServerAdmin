using System;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Server;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Application.ServerConfig.Commands
{
    public class CreateServerConfigCommand : ICreateServerConfigCommand
    {
        private readonly AppSettings _settings;
        private readonly IServerRepository _serverRepository;
        private readonly IDirectory _directory;
        private readonly IFile _file;

        public CreateServerConfigCommand(
            IOptions<AppSettings> options,
            IServerRepository serverRepository,
            IDirectory directory,
            IFile file)
        {
            _settings = options.Value;
            _serverRepository = serverRepository;
            _directory = directory;
            _file = file;
        }

        public void Execute(Guid serverId, Configuration config)
        {
            //var sourceFiles = _directory.GetFiles(_settings.ServerBasePath);

            //_serverRepository.Save(server);

            //foreach (var sourceFile in sourceFiles)
            //{
            //    var destinationFile = Path.Combine(server.Location, Path.GetFileName(sourceFile));
            //    _file.Copy(sourceFile, destinationFile);
            //}

        }
    }
}
