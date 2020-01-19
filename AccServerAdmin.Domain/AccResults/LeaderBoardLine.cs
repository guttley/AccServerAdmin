using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccResults
{
    public class LeaderBoardLine
    {
        [JsonProperty("car")]
        public Car Car { get; set; }

        [JsonProperty("currentDriver")]
        public Driver CurrentDriver { get; set; }

        [JsonProperty("currentDriverIndex")]
        public int CurrentDriverIndex { get; set; }

        [JsonProperty("timing")]
        public Timing Timing { get; set; }

        [JsonProperty("missingMandatoryPitstop")]
        public int MissingMandatoryPitstop { get; set; }

        [JsonProperty("driverTotalTimes")]
        public List<double> DriverTotalTimes { get; set; }
    }
}
