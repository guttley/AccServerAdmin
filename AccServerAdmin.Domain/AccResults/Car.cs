using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class Car
    {
        [JsonProperty("carId")]
        public long CarId { get; set; }

        [JsonProperty("raceNumber")]
        public long RaceNumber { get; set; }

        [JsonProperty("carModel")]
        public long CarModel { get; set; }

        [JsonProperty("cupCategory")]
        public long CupCategory { get; set; }

        [JsonProperty("teamName")]
        public string TeamName { get; set; }

        [JsonProperty("nationality")]
        public long Nationality { get; set; }

        /*
        [JsonProperty("carGuid")]
        public long CarGuid { get; set; }

        [JsonProperty("teamGuid")]
        public long TeamGuid { get; set; }
        */

        [JsonProperty("drivers")]
        public List<Driver> Drivers { get; set; }
    }
}
