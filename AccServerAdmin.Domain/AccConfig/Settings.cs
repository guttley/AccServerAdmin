using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the settings.json file
    /// </summary>
    /// <example>
    /// {
    ///   "serverName": "My Server Name",
    ///   "password": "",
    ///   "adminPassword": "",
    ///   "trackMedalsRequirement": 0,
    ///   "safetyRatingRequirement": -1,
    ///   "configVersion": 1,
    ///   "racecraftRatingRequirement": 0,
    ///   "spectatorSlots": 0,
    ///   "spectatorPassword": "",
    ///   "dumpLeaderboards": 0,
    ///   "isRaceLocked": 0
    /// }
    /// </example>
    [ExcludeFromCodeCoverage]
    public class Settings
    {
        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("adminPassword")]
        public string AdminPassword { get; set; }

        [JsonProperty("trackMedalsRequirement")]
        public int TrackMedalsRequirement { get; set; }

        [JsonProperty("configVersion")]
        public string Version { get; set; }

        [JsonProperty("racecraftRatingRequirement")]
        public int RacecraftRatingRequirement { get; set; }

        [JsonProperty("spectatorSlots")]
        public int spectatorSlots { get; set; }

        [JsonProperty("spectatorPassword")]
        public string SpectatorPassword { get; set; }

        [JsonProperty("dumpLeaderboards")]
        public int DumpLeaderboards { get; set; }

        [JsonProperty("isRaceLocked")]
        public int isRaceLocked { get; set; }

    }
}
