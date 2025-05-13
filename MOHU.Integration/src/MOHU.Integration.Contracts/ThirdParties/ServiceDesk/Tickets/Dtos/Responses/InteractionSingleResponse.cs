using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Common.Dtos.Responses;

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

public class InteractionSingleResponse : BaseServiceDeskResponse
{
    public InteractionCallIdReadModel? Interaction { get; set; }
    
    public TicketResponse ToTicketResponse()
    {
        if (Interaction == null)
        {
            throw new InvalidOperationException("InteractionRoot is null");
        }
        
        return new TicketResponse
        {
            ReturnCode = ReturnCode,
            Messages = Messages,
            Interaction = new InteractionResponse
            {
                CallId = Interaction.CallId,
                CrmNumber = Interaction.CrmNumber
            }
        };
    } 
}