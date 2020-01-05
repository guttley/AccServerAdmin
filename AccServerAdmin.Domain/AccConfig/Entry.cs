using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    public class Entry : IKeyedEntity
    {
        private List<Driver> _drivers;

        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid EntryListId { get; set; }

        [JsonIgnore]
        public List<DriverEntry> Entries { get; set; }

        [JsonProperty("drivers")]
        public List<Driver> Drivers
        {
            get
            {
                if (Entries != null)
                {
                    return Entries.Select(e => e.Driver).ToList();
                }

                return _drivers ??= new List<Driver>();
            }
            set => _drivers = value;
        }

        [JsonProperty("customCar")]
        public string CustomCar { get; set; } = string.Empty;

        [JsonProperty("raceNumber")]
        public int RaceNumber { get; set; }

        [JsonProperty("defaultGridPosition")] 
        public int DefaultGridPosition { get; set; }

        [JsonProperty("forcedCarModel")] 
        public CarModel ForcedCarModel { get; set; } = CarModel.NotForced;

        [JsonProperty("overrideDriverInfo")]
        [JsonConverter(typeof(BoolConverter))]
        public bool OverrideDriverInfo { get; set; }

        [JsonProperty("isServerAdmin")]
        [JsonConverter(typeof(BoolConverter))]
        public bool ServerAdmin { get; set; }

        [JsonProperty("overrideCarModelForCustomCar")]
        [JsonConverter(typeof(BoolConverter))]
        public bool OverrideCarModelForCustomCar { get; set; }

        [JsonProperty("ballastKG")]
        public int Ballast { get; set; }

        [JsonProperty("restrictor")]
        public int Restrictor { get; set; }

        [JsonProperty("configVersion")] 
        public int ConfigVersion { get; set; } = 1;
    }
}
