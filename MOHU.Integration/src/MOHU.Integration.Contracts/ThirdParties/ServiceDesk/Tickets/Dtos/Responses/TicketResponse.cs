using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Common.Dtos.Responses;

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

public class TicketResponse : BaseServiceDeskResponse
{
    public InteractionResponse Interaction { get; set; } = null!;
}