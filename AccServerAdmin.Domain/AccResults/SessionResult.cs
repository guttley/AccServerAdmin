using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class SessionResult
    {
        [JsonProperty("bestlap")]
        public long Bestlap { get; set; }

        [JsonProperty("bestSplits")]
        public List<long> BestSplits { get; set; }

        [JsonProperty("isWetSession")]
        public long IsWetSession { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("leaderBoardLines")]
        public List<LeaderBoardLine> Leaderboard { get; set; }
    }
}
