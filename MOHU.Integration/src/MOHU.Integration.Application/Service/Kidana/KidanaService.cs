using MOHU.Integration.Contracts.Dto.Kidana;

namespace MOHU.Integration.Application.Service.Kidana;

public class KidanaService(ITicketService ticketService) : IKidanaService
{
    public async Task<bool> UpdateStatusAsync(KidanaUpdateStatusRequest request)
    {
        var ticketId = await ticketService.GetTicketByIntegrationTicketNumberAsync(
            request.TicketId.ToString(), 
            Incident.Fields.KidanaTicketNumber);
            
        var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsKadanaUpdated);
            
        await ticketService.UpdateStatusAsync(ticketStatusRequest);

        return true;
    }
}