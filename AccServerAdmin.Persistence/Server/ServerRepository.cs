using System;
using System.IO;
using Microsoft.Extensions.Options;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Infrastructure.Helpers;

namespace AccServerAdmin.Persistence.Server
{
    using Domain;

    public class ServerRepository : IServerRepository
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;
        private const string Filename = "AccAdmin.json";

        public ServerRepository(
            IOptions<AppSettings> settings,
            IDirectory directory,
            IFile file,
            IJsonConverter jsonConverter)
        {
            _settings = settings.Value;
            _directory = directory;
            _file = file;
            _jsonConverter = jsonConverter;
        }

        /// <inheritdoc />
        public Server New(string serverName)
        {
            var id = Guid.NewGuid();
            var server = new Server
            {
                Id = id,
                Name = serverName,
                Location = Path.Combine(_settings.InstanceBasePath, id.ToString())
            };

            return server;
        }

        /// <inheritdoc />
        public void Save(Server server)
        {
            if (!_directory.Exists(Path.GetDirectoryName(server.Location)))
                _directory.CreateDirectory(server.Location);

            var path = Path.Combine(server.Location, Filename);
            var json = _jsonConverter.SerializeObject(server);
            _file.WriteAllText(path, json);
        }

        /// <inheritdoc />
        public Server Read(string directory)
        {
            var path = Path.Combine(directory, Filename);

            if (!_file.Exists(path))
                throw new ArgumentException($"No {Filename} exists in the directory: {directory}");

            var json = _file.ReadAllText(path);
            var server = _jsonConverter.DeserializeObject<Server>(json);
            server.Location = path;

            return server;
        }

    }
}
