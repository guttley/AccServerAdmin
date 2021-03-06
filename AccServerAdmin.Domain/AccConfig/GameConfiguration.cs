﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the settings.json file
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GameCfg : IKeyedEntity
    {
        public const string DefaultServerName = "New Server";
        public const string DefaultServerPassword = "D3f@u1tAdm1n";
        public const int DefaultTrackMedalsRequirement = 3;
        public const int DefaultConfigVersion = 1;
        public const int DefaultRacecraftRatingRequirement = 70;
        public const int DefaultSafteyRatingRequirement = 0;
        public const int DefaultCarSlots = 30;
        public const string DefaultSpectatorPassword = "spectator";
        public const bool DefaultDumpLeaderboards = true;
        public const bool DefaultDumpEntryList = true;
        public const bool DefaultIsRaceLocked = false;
        public const bool DefaultAutoDq = false;
        public const bool DefaultShortFormationLap = false;
        public const bool DefaultRandomTrackWhenEmpty = false;

        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }

        [JsonProperty("carGroup")]
        [JsonConverter(typeof(CarGroupConverter))]
        public CarGroup CarGroup { get; set; }

        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("adminPassword")]
        public string AdminPassword { get; set; }

        [JsonProperty("trackMedalsRequirement")]
        public int TrackMedalsRequirement { get; set; }

        [JsonProperty("safetyRatingRequirement")]
        public int SafetyRatingRequirement { get; set; }

        [JsonProperty("configVersion")]
        public int Version { get; set; }

        [JsonProperty("racecraftRatingRequirement")]
        public int RacecraftRatingRequirement { get; set; }

        [JsonProperty("spectatorPassword")]
        public string SpectatorPassword { get; set; }

        [JsonProperty("allowAutoDQ")]
        [JsonConverter(typeof(BoolConverter))]
        public bool AllowAutoDisqualification { get; set; }

        [JsonProperty("randomizeTrackWhenEmpty")]
        [JsonConverter(typeof(BoolConverter))]
        public bool RandomizeTrackWhenEmpty { get; set; }

        [JsonProperty("shortFormationLap")]
        [JsonConverter(typeof(BoolConverter))]
        public bool ShortFormationLap { get; set; }

        [JsonProperty("isRaceLocked")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsRaceLocked { get; set; }

        [JsonProperty("dumpEntryList")]
        [JsonConverter(typeof(BoolConverter))]
        public bool DumpEntryList { get; set; }

        [JsonProperty("dumpLeaderboards")]
        [JsonConverter(typeof(BoolConverter))]
        public bool DumpLeaderboards { get; set; }

        [JsonProperty("maxCarSlots")]
        public int MaxCarSlots { get; set; }

        [JsonProperty("centralEntryListPath")]
        public string CentralEntryListPath { get; set; }

        [JsonProperty("formationLapType")]
        public FormationLap FormationLap { get; set; }

        public static GameCfg CreateDefault()
        {
            var gameCfg = new GameCfg
            {
                ServerName = DefaultServerName,
                Password = string.Empty,
                AdminPassword = DefaultServerPassword,
                TrackMedalsRequirement = DefaultTrackMedalsRequirement,
                Version = DefaultConfigVersion,
                RacecraftRatingRequirement = DefaultRacecraftRatingRequirement,
                SafetyRatingRequirement = DefaultSafteyRatingRequirement,
                SpectatorPassword = DefaultSpectatorPassword,
                DumpLeaderboards = DefaultDumpLeaderboards,
                DumpEntryList = DefaultDumpEntryList,
                IsRaceLocked = DefaultIsRaceLocked,
                AllowAutoDisqualification = DefaultAutoDq,
                ShortFormationLap = DefaultShortFormationLap,
                RandomizeTrackWhenEmpty = DefaultRandomTrackWhenEmpty,
                MaxCarSlots = DefaultCarSlots,
                FormationLap = FormationLap.Default,
                CentralEntryListPath = string.Empty
            };

            return gameCfg;
        }

    }
}
