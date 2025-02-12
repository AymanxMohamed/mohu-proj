using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Common.Dtos.Responses;

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

public class GetInteractionCallIdResponse : RootServiceDeskPaginationResponse<InteractionRootResponse>
{
    public TicketResponse ToTicketResponse()
    {
        var interactionRoot = Content?.FirstOrDefault();

        if (interactionRoot == null)
        {
            throw new InvalidOperationException("InteractionRoot is null");
        }
        
        return new TicketResponse
        {
            ReturnCode = ReturnCode,
            Messages = Messages,
            Interaction = new InteractionResponse
            {
                CallID = interactionRoot.Interaction.CallID,
                CRMNumber = interactionRoot.Interaction.CRMNumber
            }
        };
    }    
}