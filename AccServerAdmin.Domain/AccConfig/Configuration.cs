﻿using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the configuration.json file
    /// </summary>
    /// <example>
    /// {
    ///   "udpPort": 9431,
    ///   "tcpPort": 9432,
    ///   "maxClients": 10,
    ///   "configVersion": 1,
    ///   "registerToLobby": 1
    /// }
    /// </example>
    public class Configuration
    {
        [JsonProperty("udpPort")]
        public ushort UdpPort { get; set; }

        [JsonProperty("tcpPort")]
        public ushort TcpPort { get; set; }

        [JsonProperty("maxClients")]
        public int MaxClients { get; set; }

        [JsonProperty("configVersion")]
        public string Version { get; set; }
    }
}
