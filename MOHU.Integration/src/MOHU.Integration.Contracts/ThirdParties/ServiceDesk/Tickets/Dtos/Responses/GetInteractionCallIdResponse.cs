using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Common.Dtos.Responses;

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

public class GetInteractionCallIdResponse : RootServiceDeskPaginationResponse<InteractionCallIdReadModel>
{
    public TicketResponse ToTicketResponse()
    {
        var interaction = Content.First();
        
        return new TicketResponse
        {
            ReturnCode = ReturnCode,
            Messages = Messages,
            Interaction = new InteractionResponse
            {
                CallId = interaction.CallId,
                CrmNumber = interaction.CrmNumber
            }
        };
    }    
}