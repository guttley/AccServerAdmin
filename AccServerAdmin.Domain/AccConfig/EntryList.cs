using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    public class EntryList
    {
        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }

        [JsonProperty("forceEntryList")]
        [JsonConverter(typeof(BoolConverter))]
        public bool ForceEntryList { get; set; }

        [JsonProperty("configVersion")] 
        public int ConfigVersion { get; set; } = 1;
    }
}
