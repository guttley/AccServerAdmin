using System;
using System.Collections.Generic;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Persistence.EventConfig
{
    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class EventConfigRepository : BaseConfigRepository<EventConfiguration>
    {
        private const string ConfigDir = "cfg";
        private const string Filename = "event.json";

        public const string DefaultTrack = "misano";
        public const string DefaultEventType = "E_3h";
        public const int DefaultPreRaceWaitingTimeSeconds = 15;
        public const int DefaultSessionOverTimeSeconds = 120;
        public const int DefaultAmbientTemp = 26;
        public const int DefaultTrackTemp = 30;
        public const double DefaultCloudLevel = 0.3d;
        public const double DefaultRain = 0;
        public const int DefaultWeatherRandomness = 1;
        public const int DefaultConfigVersion = 1;
        public const int DefaultPostQualySeconds = 0;
        public const int DefaultPostRaceSeconds = 0;

        public EventConfigRepository(
            IDirectory directory,
            IFile file,
            IJsonConverter converter) 
            : base(directory, file, converter, ConfigDir, Filename)
        {
        }

        /// <inheritdoc />
        public override EventConfiguration New()
        {
            return new EventConfiguration
            {
                Track = DefaultTrack,
                EventType = DefaultEventType,
                PreRaceWaitingTimeSeconds = DefaultPreRaceWaitingTimeSeconds,
                SessionOverTimeSeconds = DefaultSessionOverTimeSeconds,
                AmbientTemp = DefaultAmbientTemp,
                TrackTemp = DefaultTrackTemp,
                CloudLevel = DefaultCloudLevel, 
                Rain = DefaultRain,
                WeatherRandomness = DefaultWeatherRandomness,
                Version = DefaultConfigVersion,
                PostQualySeconds = DefaultPostQualySeconds,
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
        }
    }
}
