using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Persistence.ServerConfig
{
    using Domain.AccConfig;

    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class NetworkConfigRepository : BaseConfigRepository<NetworkConfiguration>
    {
        private const string ConfigDir = "cfg";
        private const string Filename = "configuration.json";

        public const int DefaultMaxClients = 30;
        public const int DefaultUdpPort = 9331;
        public const int DefaultTcpPort = 9332;
        public const int DefaultConfigVersion = 1;
        public const bool DefaultRegisterToLobby = true;

        public NetworkConfigRepository(
            IDirectory directory,
            IFile file,
            IJsonConverter converter)
            : base(directory, file, converter, ConfigDir, Filename)
        {

        }

        /// <inheritdoc />
        public override NetworkConfiguration New()
        {
            return new NetworkConfiguration
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
