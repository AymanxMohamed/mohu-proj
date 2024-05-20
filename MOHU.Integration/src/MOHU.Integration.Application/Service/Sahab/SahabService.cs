using MOHU.Integration.Contracts.Dto.Sahab;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service.Sahab;

public class SahabService(ITicketService ticketService) : ISahabService
{
    public async Task<bool> UpdateStatusAsync(SahabUpdateStatusRequest request)
    {
        var ticketId = await ticketService.GetTicketByIntegrationTicketNumberAsync(
            integrationTicketNumber: request.TicketNumber, 
            ticketNumberSchemaName: Incident.Fields.SahabTicketNumber);

        await ticketService.UpdateStatusAsync(request.ToUpdateTicketStatusRequest(ticketId));
        
        return true;
    }
}