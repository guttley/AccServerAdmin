using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class Driver
    {
        [JsonProperty("playerID")]
        public string PlayerId { get; set; }

        [JsonProperty("firstName")]
        public string Firstname { get; set; } = string.Empty;

        [JsonProperty("lastName")]
        public string Lastname { get; set; } = string.Empty;

        [JsonIgnore]
        public string Fullname => $"{Firstname} {Lastname}";

        [JsonProperty("shortName")]
        public string Shortname { get; set; } = string.Empty;
    }
}
