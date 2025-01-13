using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.ServiceDeskProxy
{
    public class InteractionUpdate
    {
        [JsonProperty("comment")]///for update
        public string comment { get; set; }
    }
}
