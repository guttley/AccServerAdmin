using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AccServerAdmin.Domain.AccConfig
{
    [ExcludeFromCodeCoverage]
    public class AssistRules
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }

        [JsonProperty("stabilityControlLevelMax")]
        public int StabilityControlLevelMax { get; set; }

        [JsonProperty("disableAutosteer")]
        public bool DisableAutosteer { get; set; }

        [JsonProperty("disableAutoLights")]
        public bool DisableAutoLights { get; set; }

        [JsonProperty("disableAutoWiper")]
        public bool DisableAutoWiper { get; set; }

        [JsonProperty("disableAutoEngineStart")]
        public bool DisableAutoEngineStart { get; set; }

        [JsonProperty("disableAutoPitLimiter")]
        public bool DisableAutoPitLimiter { get; set; }

        [JsonProperty("disableAutoGear")]
        public bool DisableAutoGear { get; set; }

        [JsonProperty("disableAutoClutch")]
        public bool DisableAutoClutch { get; set; }

        [JsonProperty("disableIdealLine")]
        public bool DisableIdealLine { get; set; }

        public static AssistRules CreateDefault()
        {
            return new AssistRules
            {
                StabilityControlLevelMax = 25,
                DisableAutosteer = true,
                DisableAutoLights = false,
                DisableAutoWiper = false,
                DisableAutoEngineStart = false,
                DisableAutoPitLimiter = false,
                DisableAutoGear = true,
                DisableAutoClutch = false,
                DisableIdealLine = true,
            };
        }


    }
}
