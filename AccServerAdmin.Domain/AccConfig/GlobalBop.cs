using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain.AccConfig
{
    public class GlobalBop
    {
        [JsonProperty("entries")] 
        public List<BalanceOfPerformance> BopList { get; set; }

    }
}
