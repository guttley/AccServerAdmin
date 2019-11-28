using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    [ExcludeFromCodeCoverage]
    public class Driver : IKeyedEntity
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonProperty("playerID")] 
        public string PlayerId { get; set; }

        [JsonProperty("firstName")] 
        public string Firstname { get; set; }

        [JsonProperty("lastName")] 
        public string Lastname { get; set; }

        [JsonIgnore]
        public string Fullname => $"{Firstname} {Lastname}";

        [JsonProperty("nickName")] 
        public string Nickname { get; set; }

        [JsonProperty("shortName")] 
        public string Shortname { get; set; }

        [JsonProperty("driverCategory")]
        public DriverCategory DriverCategory { get; set; }

        [JsonProperty("helmetTemplateKey")]
        public int HelmetTemplateKey { get; set; }

        [JsonProperty("helmetBaseColor")]
        public int HelmetBaseColor { get; set; }

        [JsonProperty("helmetDetailColor")]
        public int HelmetDetailColor { get; set; }

        [JsonProperty("helmetMaterialType")]
        public int HelmetMaterialType { get; set; }

        [JsonProperty("helmetGlassColor")]
        public int HelmetGlassColor { get; set; }

        [JsonProperty("helmetGlassMetallic")]
        public double HelmetGlassMetallic { get; set; }

        [JsonProperty("glovesTemplateKey")]
        public int GlovesTemplateKey { get; set; }

        [JsonProperty("suitTemplateKey")]
        public int SuitTemplateKey { get; set; }

        [JsonProperty("suitDetailColor1")]
        public int SuitDetailColor1 { get; set; }

        [JsonProperty("suitDetailColor2")]
        public int SuitDetailColor2 { get; set; }

        [JsonProperty("aiSkill")]
        public int AiSkill { get; set; }

        [JsonProperty("aiAggro")]
        public int AiAggro { get; set; }

        [JsonProperty("aiRainSkill")]
        public int AiRainSkill { get; set; }

        [JsonProperty("aiConsistency")]
        public int AiConsistency { get; set; }                
    }
}
