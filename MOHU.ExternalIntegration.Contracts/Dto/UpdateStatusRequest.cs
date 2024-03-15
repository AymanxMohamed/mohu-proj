using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MOHU.ExternalIntegration.Contracts.Enum;

namespace MOHU.ExternalIntegration.Contracts.Dto
{


    public class UpdateStatusRequest
    {

        public Guid CustomerId { get; set; }
        public Guid TicketId { get; set; }
        [MaxLength(400)]
        public string Resolution { get; set; }
        public DateTime? ResolutionDate { get; set; }

        public IntegrationStatus IntegrationStatus { get; set; }

    }


}
