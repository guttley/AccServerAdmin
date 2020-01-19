using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class Lap
    {
        [JsonProperty("carId")]
        public long CarId { get; set; }

        [JsonProperty("driverIndex")]
        public long DriverIndex { get; set; }

        [JsonProperty("laptime")]
        public long LapTime { get; set; }

        [JsonProperty("isValidForBest")]
        public bool IsValidForBest { get; set; }

        [JsonProperty("splits")]
        public List<long> Splits { get; set; }
    }
}
