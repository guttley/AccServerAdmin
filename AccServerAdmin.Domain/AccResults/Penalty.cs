using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class Penalty
    {
        [JsonProperty("carId")]
        public long CarId { get; set; }

        [JsonProperty("driverIndex")]
        public long DriverIndex { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("penalty")]
        public string PenaltyPenalty { get; set; }

        [JsonProperty("penaltyValue")]
        public long PenaltyValue { get; set; }

        [JsonProperty("violationInLap")]
        public long ViolationInLap { get; set; }

        [JsonProperty("clearedInLap")]
        public long ClearedInLap { get; set; }
    }
}
