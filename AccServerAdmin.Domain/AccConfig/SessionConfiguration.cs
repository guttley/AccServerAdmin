
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the session of the session list from the event.json file
    /// </summary>
    /// <example>
    /// [
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
    /// ]
    /// </example>
    [ExcludeFromCodeCoverage]
    public class SessionConfiguration
    {
        [JsonProperty("hourOfDay")]
        public int HourOfDay { get; set; }

        [JsonProperty("dayOfWeekend")]
        public int DayOfWeekend { get; set; }

        [JsonProperty("timeMultiplier")]
        public int TimeMultiplier { get; set; }

        [JsonProperty("sessionType")]
        public string SessionType { get; set; }

        [JsonProperty("sessionDurationMinutes")]
        public int SessionDurationMinutes { get; set; }
    }
}
