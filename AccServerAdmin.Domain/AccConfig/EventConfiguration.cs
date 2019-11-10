using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the event.json file
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventCfg
    {
        public const string DefaultTrack = "misano";
        public const string DefaultEventType = "E_3h";
        public const int DefaultPreRaceWaitingTimeSeconds = 15;
        public const int DefaultSessionOverTimeSeconds = 120;
        public const int DefaultAmbientTemp = 26;
        public const double DefaultCloudLevel = 0.3d;
        public const double DefaultRain = 0;
        public const int DefaultWeatherRandomness = 1;
        public const int DefaultConfigVersion = 1;
        public const int DefaultPostQuallySeconds = 90;
        public const int DefaultPostRaceSeconds = 125;

        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [JsonProperty("cloudLevel")]
        public double CloudLevel { get; set; }

        [JsonProperty("rain")]
        public double Rain { get; set; }

        [JsonProperty("weatherRandomness")]
        public int WeatherRandomness { get; set; }

        [JsonProperty("configVersion")]
        public int Version { get; set; }

        [JsonProperty("postQualySeconds")]
        public int PostQualySeconds { get; set; }

        [JsonProperty("postRaceSeconds")]
        public int PostRaceSeconds { get; set; }

        [JsonProperty("sessions")]
        public List<SessionConfiguration> Sessions { get; set; }


        public static EventCfg CreateDefault()
        {
            var eventCfg = new EventCfg
            {
                Track = DefaultTrack,
                EventType = DefaultEventType,
                PreRaceWaitingTimeSeconds = DefaultPreRaceWaitingTimeSeconds,
                SessionOverTimeSeconds = DefaultSessionOverTimeSeconds,
                AmbientTemp = DefaultAmbientTemp,
                CloudLevel = DefaultCloudLevel,
                Rain = DefaultRain,
                WeatherRandomness = DefaultWeatherRandomness,
                Version = DefaultConfigVersion,
                PostQualySeconds = DefaultPostQuallySeconds,
                PostRaceSeconds = DefaultPostRaceSeconds,
                Sessions = new List<SessionConfiguration>
                {
                    new SessionConfiguration
                    {
                        SessionType = SessionType.Practice,
                        DayOfWeekend = 1,
                        HourOfDay = 10,
                        TimeMultiplier = 1,
                        SessionDurationMinutes = 20
                    },
                    new SessionConfiguration
                    {
                        SessionType = SessionType.Qually,
                        DayOfWeekend = 2,
                        HourOfDay = 15,
                        TimeMultiplier = 1,
                        SessionDurationMinutes = 10
                    },
                    new SessionConfiguration
                    {
                        SessionType = SessionType.Race,
                        DayOfWeekend = 3,
                        HourOfDay = 14,
                        TimeMultiplier = 1,
                        SessionDurationMinutes = 20
                    },
                }
            };

            return eventCfg;
        }

    }
}
