﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the configuration.json file
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NetworkCfg
    {
        public const int DefaultMaxConnections = 30;
        public const int DefaultUdpPort = 9331;
        public const int DefaultTcpPort = 9332;
        public const int DefaultConfigVersion = 1;
        public const bool DefaultRegisterToLobby = true;

        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }
        
        [JsonProperty("udpPort")]
        public int UdpPort { get; set; }

        [JsonProperty("tcpPort")]
        public int TcpPort { get; set; }

        [JsonProperty("maxConnections")]
        public int MaxConnections { get; set; }

        [JsonProperty("configVersion")]
        public int Version { get; set; }

        [JsonProperty("registerToLobby")]
        [JsonConverter(typeof(BoolConverter))]
        public bool RegisterToLobby { get; set; }

        public static NetworkCfg CreateDefault()
        {
            var networkCfg = new NetworkCfg
            {
                MaxConnections = DefaultMaxConnections,
                TcpPort = DefaultTcpPort,
                UdpPort = DefaultUdpPort,
                Version = DefaultConfigVersion,
                RegisterToLobby = DefaultRegisterToLobby
            };

            return networkCfg;
        }

    }
}
