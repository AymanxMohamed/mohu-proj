using MOHU.Integration.Contracts.Dto.Taasher;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service.Taasher;

public class TaasherService(ITicketService ticketService) : ITaasherService
{
    public async Task<bool> UpdateStatusAsync(TaasherUpdateStatusRequest request)
    {
        var ticketId = await ticketService.GetTicketByIntegrationTicketNumberAsync(
            request.TicketNumber, 
            Incident.Fields.TaasherTicketNumber);
            
        var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsTashirUpdated);
            
        await ticketService.UpdateStatusAsync(ticketStatusRequest);

        return true;
    }
}