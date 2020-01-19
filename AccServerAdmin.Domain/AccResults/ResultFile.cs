using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class ResultFile
    {
        [JsonProperty("sessionType")]
        public string SessionType { get; set; }

        [JsonProperty("trackName")]
        public string TrackName { get; set; }

        [JsonProperty("sessionIndex")]
        public long SessionIndex { get; set; }

        [JsonProperty("sessionResult")]
        public SessionResult SessionResult { get; set; }

        [JsonProperty("laps")]
        public List<Lap> Laps { get; set; }

        [JsonProperty("penalties")]
        public List<Penalty> Penalties { get; set; }
    }
}
