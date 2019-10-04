using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the configuration.json file
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NetworkConfiguration
    {
        public const int DefaultMaxClients = 30;
        public const int DefaultUdpPort = 9331;
        public const int DefaultTcpPort = 9332;
        public const int DefaultConfigVersion = 1;
        public const bool DefaultRegisterToLobby = true;

        public NetworkConfiguration()
        {
            MaxClients = DefaultMaxClients;
            TcpPort = DefaultTcpPort;
            UdpPort = DefaultUdpPort;
            Version = DefaultConfigVersion;
            RegisterToLobby = DefaultRegisterToLobby;
        }

        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }


        [JsonProperty("udpPort")]
        public int UdpPort { get; set; }

        [JsonProperty("tcpPort")]
        public int TcpPort { get; set; }

        [JsonProperty("maxClients")]
        public int MaxClients { get; set; }

        [JsonProperty("configVersion")]
        public int Version { get; set; }

        [JsonProperty("registerToLobby")]
        [JsonConverter(typeof(BoolConverter))]
        public bool RegisterToLobby { get; set; }
    }
}
