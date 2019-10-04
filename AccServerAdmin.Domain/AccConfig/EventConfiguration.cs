using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the event.json file
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventConfiguration
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        
        [JsonIgnore]
        public Guid ServerId { get; set; }

        [JsonProperty("track")]
        public string Track { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("preRaceWaitingTimeSeconds")]
        public int PreRaceWaitingTimeSeconds { get; set; }

        [JsonProperty("sessionOverTimeSeconds")]
        public int SessionOverTimeSeconds { get; set; }

        [JsonProperty("ambientTemp")]
        public int AmbientTemp { get; set; }

        [JsonProperty("trackTemp")]
        public int TrackTemp { get; set; }

        [JsonProperty("cloudLevel")]
        public double CloudLevel { get; set; }

        [JsonProperty("rain")]
        public double Rain { get; set; }

        [JsonProperty("weatherRandomness")]
        public int WeatherRandomness { get; set; }

        [JsonProperty("configVersion")]
        public int Version { get; set; }

        [JsonProperty("sessions")]
        public List<SessionConfiguration> Sessions { get; set; }

        [JsonProperty("postQualySeconds")]
        public int PostQualySeconds { get; set; }

        [JsonProperty("postRaceSeconds")]
        public int PostRaceSeconds { get; set; }

    }
}
