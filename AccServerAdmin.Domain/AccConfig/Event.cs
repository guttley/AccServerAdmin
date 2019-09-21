using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the event.json file
    /// </summary>
    /// <example>
    ///{
    ///  "track": "misano",
    ///  "eventType": "E_3h",
    ///  "preRaceWaitingTimeSeconds": 15,
    ///  "sessionOverTimeSeconds": 120,
    ///  "ambientTemp": 26,
    ///  "trackTemp": 30,
    ///  "cloudLevel": 0.3,
    ///  "rain": 0,
    ///  "weatherRandomness": 1,
    ///  "configVersion": 1,
    ///  "sessions": [
    ///   {
    ///     "hourOfDay": 10,
    ///     "dayOfWeekend": 1,
    ///     "timeMultiplier": 1,
    ///     "sessionType": "P",
    ///     "sessionDurationMinutes": 20
    ///   },
    ///   {
    ///     "hourOfDay": 17,
    ///     "dayOfWeekend": 2,
    ///     "timeMultiplier": 8,
    ///     "sessionType": "Q",
    ///     "sessionDurationMinutes": 10
    ///   },
    ///   {
    ///     "hourOfDay": 16,
    ///     "dayOfWeekend": 3,
    ///     "timeMultiplier": 3,
    ///     "sessionType": "Q",
    ///     "sessionDurationMinutes": 20
    ///   }
    ///  ],
    ///  "postQualySeconds": 0,
    ///  "postRaceSeconds": 0
    ///}
    /// </example>
    [ExcludeFromCodeCoverage]
    public class Event
    {
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
        public decimal cloudLevel { get; set; }

        [JsonProperty("rain")]
        public decimal Rain { get; set; }

        [JsonProperty("weatherRandomness")]
        public int WeatherRandomness { get; set; }

        [JsonProperty("configVersion")]
        public string Version { get; set; }

        [JsonProperty("sessions")]
        public List<Session> Sessions { get; set; }

        [JsonProperty("postQualySeconds")]
        public int PostQualySeconds { get; set; }

        [JsonProperty("postRaceSeconds")]
        public int PostRaceSeconds { get; set; }

    }
}
