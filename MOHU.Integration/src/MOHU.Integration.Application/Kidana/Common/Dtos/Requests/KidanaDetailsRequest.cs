using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Dtos.Requests
{
    public class KidanaDetailsRequest
    {
        [JsonProperty("ticketid")]
        public string TicketId { get; init; } = null!;
    }
}
