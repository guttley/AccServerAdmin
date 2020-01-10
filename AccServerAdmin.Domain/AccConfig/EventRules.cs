using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AccServerAdmin.Domain.AccConfig
{
    [ExcludeFromCodeCoverage]
    public class EventRules
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }

        [JsonProperty("qualifyStandingType")]
        public QualifyStanding QualifyType { get; set; }

        [JsonProperty("pitWindowLengthSec")]
        public int PitWindowLength { get; set; }

        [JsonProperty("driverStintTimeSec")]
        public int DriverStintTime { get; set; }

        [JsonProperty("mandatoryPitstopCount")]
        public int MandatoryPitstopCount { get; set; }

        [JsonProperty("maxTotalDrivingTime")]
        public int MaxTotalDrivingTime { get; set; }

        [JsonProperty("maxDriversCount")]
        public int MaxDriversCount { get; set; }

        [JsonProperty("isRefuellingAllowedInRace")]
        public bool RefuellingAllowedInRace { get; set; }

        [JsonProperty("isRefuellingTimeFixed")]
        public bool RefuellingTimeFixed { get; set; }

        [JsonProperty("isMandatoryPitstopRefuellingRequired")]
        public bool MandatoryPitstopRefuellingRequired { get; set; }

        [JsonProperty("isMandatoryPitstopTyreChangeRequired")]
        public bool MandatoryPitstopTyreChangeRequired { get; set; }

        [JsonProperty("isMandatoryPitstopSwapDriverRequired")]
        public bool MandatoryPitstopSwapDriverRequired { get; set; }

        public static EventRules CreateDefault()
        {
            return new EventRules
            {
                QualifyType = QualifyStanding.FastestLap,
                PitWindowLength = -1,
                DriverStintTime = -1,
                MandatoryPitstopCount = 0,
                MaxTotalDrivingTime = -1,
                MaxDriversCount = 1,
                RefuellingAllowedInRace = true,
                RefuellingTimeFixed = false,
                MandatoryPitstopRefuellingRequired = false,
                MandatoryPitstopTyreChangeRequired = false,
                MandatoryPitstopSwapDriverRequired = false
            };
        }


    }
}
