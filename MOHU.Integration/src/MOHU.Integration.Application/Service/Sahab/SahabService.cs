using MOHU.Integration.Contracts.Dto.Sahab;

namespace MOHU.Integration.Application.Service.Sahab;

public class SahabService(ITicketService ticketService) : ISahabService
{
    public async Task<bool> UpdateStatusAsync(SahabUpdateStatusRequest request)
    {
        var ticketId = await ticketService.GetTicketByIntegrationTicketNumberAsync(
            integrationTicketNumber: request.TicketNumber, 
            ticketNumberSchemaName: Incident.Fields.SahabTicketNumber);
        
        return  await ticketService.UpdateStatusAsync(request.ToUpdateTicketStatusRequest(ticketId));
    }
}