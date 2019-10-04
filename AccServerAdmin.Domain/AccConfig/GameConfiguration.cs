using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the settings.json file
    /// </summary>
    /// <example>
    [ExcludeFromCodeCoverage]
    public class GameConfiguration : IKeyedEntity
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }

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

        [JsonProperty("spectatorSlots")]
        public int SpectatorSlots { get; set; }

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
    }
}
