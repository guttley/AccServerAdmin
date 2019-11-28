using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    public class EntryList
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }

        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }

        [JsonProperty("forceEntryList")]
        [JsonConverter(typeof(BoolConverter))]
        public bool ForceEntryList { get; set; }

        [JsonProperty("configVersion")] 
        public int ConfigVersion { get; set; } = 1;

        public static EntryList CreateDefault()
        {
            return new EntryList
            {
                Entries =  new List<Entry>()
            };
        }
    }
}
