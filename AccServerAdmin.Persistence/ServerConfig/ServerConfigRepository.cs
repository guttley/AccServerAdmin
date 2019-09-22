using System;
using System.IO;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Persistence.ServerConfig
{
    using AccServerAdmin.Resouce;
    using Domain;
    using Domain.AccConfig;

    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class ServerConfigRepository : IServerConfigRepository
    {
        private const string ConfigDir = "cfg";
        private const string Filename = "configuration.json";
        private readonly IDirectory _directory;
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;

        public const int DefaultMaxClients = 30;
        public const int DefaultUdpPort = 9331;
        public const int DefaultTcpPort = 9332;
        public const int DefaultConfigVersion = 1;
        public const int DefaultRegisterToLobby = 1;

        public ServerConfigRepository(
            IDirectory directory,
            IFile file,
            IJsonConverter converter)
        {
            _directory = directory;
            _file = file;
            _jsonConverter = converter;
        }

        /// <inheritdoc />
        public Configuration New()
        {
            return new Configuration
            {
                MaxClients = DefaultMaxClients,
                TcpPort =  DefaultTcpPort,
                UdpPort = DefaultUdpPort,
                Version = DefaultConfigVersion,
                RegisterToLobby = DefaultRegisterToLobby
            };
        }

        /// <inheritdoc />
        public void Save(string serverDirectory,  Configuration config)
        {
            var path = Path.Combine(serverDirectory, ConfigDir);
            var json = _jsonConverter.SerializeObject(config);

            if (!_directory.Exists(path))
                _directory.CreateDirectory(path);

            path = Path.Combine(path, Filename);
            _file.WriteAllText(path, json);
        }

        /// <inheritdoc />
        public Configuration Read(string serverDirectory)
        {
            var path = Path.Combine(serverDirectory, ConfigDir, Filename);

            if (!_file.Exists(path))
                return New();

            var json = _file.ReadAllText(path);
            var config = _jsonConverter.DeserializeObject<Configuration>(json);

            return config;

        }
    }
}
