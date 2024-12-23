using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Dto.Ticket
{
    public class SubmitTicketResponse
    {

        public SubmitTicketResponse()
        {
        }

        private SubmitTicketResponse(Entity caseEntity)
        {
            TicketId = caseEntity.Id;
            TicketNumber = caseEntity.GetAttributeValue<string>(Incident.Fields.Title);
        }

        public Guid TicketId { get; set; }
        public string TicketNumber { get; set; } = null!;

        public static SubmitTicketResponse Create(Entity caseEntity) => new(caseEntity);
    }
}
