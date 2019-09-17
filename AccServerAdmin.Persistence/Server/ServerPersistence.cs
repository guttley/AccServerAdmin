using System;
using System.IO;
using AccServerAdmin.Infrastructure.IO;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Persistence.Server
{
    using AccServerAdmin.Infrastructure.Helpers;
    using AccServerAdmin.Domain;
    
    public class ServerPersistence : IServerPersistence
    {
        private readonly AppSettings _settings;
        private readonly IDirectory _directory;
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;
        private const string Filename = "AccAdmin.json";

        public ServerPersistence(
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
        public void Save(Server server)
        {
            var path = Path.Combine(_settings.InstanceBasePath, server.Id.ToString(), Filename);

            if (string.IsNullOrEmpty(server.Location))
                server.Location = Path.GetDirectoryName(path);

            if (!_directory.Exists(server.Location))
                _directory.CreateDirectory(server.Location);

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
