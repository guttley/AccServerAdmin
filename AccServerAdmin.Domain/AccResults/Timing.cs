using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class Timing
    {
        [JsonProperty("lastLap")]
        public long LastLap { get; set; }

        [JsonProperty("lastSplits")]
        public List<long> LastSplits { get; set; }

        [JsonProperty("bestLap")]
        public long BestLap { get; set; }

        [JsonProperty("bestSplits")]
        public List<long> BestSplits { get; set; }

        [JsonProperty("TotalTime")]
        public long TotalTime { get; set; }

        [JsonProperty("lapCount")]
        public long LapCount { get; set; }

        [JsonProperty("lastSplitId")]
        public long LastSplitId { get; set; }
    }
}
