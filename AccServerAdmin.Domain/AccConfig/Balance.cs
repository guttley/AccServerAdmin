using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    public class BalanceOfPerformance : IKeyedEntity
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ServerId { get; set; }

        [JsonProperty("track")]
        public string Track { get; set; }

        [JsonProperty("carModel")] 
        public CarModel Car { get; set; }

        [JsonProperty("ballastKg")]
        public int Ballast { get; set; }

        [JsonProperty("restrictor")]
        public int Restrictor { get; set; }
    }
}
