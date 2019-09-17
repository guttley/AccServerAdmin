﻿using Microsoft.Extensions.Options;
using System.IO;


namespace AccServerAdmin.Application.Servers.Commands.CreateServer
{
    using AccServerAdmin.Domain;
    using AccServerAdmin.Infrastructure.IO;
    using AccServerAdmin.Persistence.Server;    

    public class CreateServerCommand : ICreateServerCommand
    {
        private readonly AppSettings _settings;
        private readonly IServerRepository _serverRepository;
        private readonly IDirectory _directory;
        private readonly IFile _file;

        public CreateServerCommand(
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

        public Server Execute(string serverName)
        {
            var server = new Server { Name = serverName };
            var sourceFiles = _directory.GetFiles(_settings.ServerBasePath);

            _serverRepository.Save(server);            

            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile = Path.Combine(server.Location, Path.GetFileName(sourceFile));
                _file.Copy(sourceFile, destinationFile);
            }

            return server;
        }
    }
}
