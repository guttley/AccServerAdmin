﻿using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Persistence.ServerConfig
{
    using Domain.AccConfig;

    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class ServerConfigRepository : BaseConfigRepository<ServerConfiguration>
    {
        private const string ConfigDir = "cfg";
        private const string Filename = "configuration.json";

        public const int DefaultMaxClients = 30;
        public const int DefaultUdpPort = 9331;
        public const int DefaultTcpPort = 9332;
        public const int DefaultConfigVersion = 1;
        public const int DefaultRegisterToLobby = 1;

        public ServerConfigRepository(
            IDirectory directory,
            IFile file,
            IJsonConverter converter)
            : base(directory, file, converter, ConfigDir, Filename)
        {

        }

        /// <inheritdoc />
        public override ServerConfiguration New()
        {
            return new ServerConfiguration
            {
                MaxClients = DefaultMaxClients,
                TcpPort =  DefaultTcpPort,
                UdpPort = DefaultUdpPort,
                Version = DefaultConfigVersion,
                RegisterToLobby = DefaultRegisterToLobby
            };
        }

    }
}