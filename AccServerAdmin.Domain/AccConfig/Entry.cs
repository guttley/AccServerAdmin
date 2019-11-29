using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    public class Entry : IKeyedEntity
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid EntryListId { get; set; }

        [JsonProperty("drivers")] 
        public List<Driver> Drivers { get; set; }

        [JsonProperty("customCar")]
        public string CustomCar { get; set; }

        [JsonProperty("raceNumber")]
        public int RaceNumber { get; set; }

        [JsonProperty("defaultGridPosition")] 
        public int DefaultGridPosition { get; set; }

        [JsonProperty("forcedCarModel")]
        public CarModel ForcedCarModel { get; set; }

        [JsonProperty("overrideDriverInfo")]
        [JsonConverter(typeof(BoolConverter))]
        public bool OverrideDriverInfo { get; set; }

        [JsonProperty("isServerAdmin")]
        [JsonConverter(typeof(BoolConverter))]
        public bool ServerAdmin { get; set; }

        [JsonProperty("overrideCarModelForCustomCar")]
        [JsonConverter(typeof(BoolConverter))]
        public bool OverrideCarModelForCustomCar { get; set; }

        [JsonProperty("configVersion")] 
        public int ConfigVersion { get; set; } = 1;
    }
}
