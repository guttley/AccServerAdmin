using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    /// <summary>
    /// Model for the session of the session list from the event.json file
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SessionConfiguration
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid EventId { get; set; }

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
