using MOHU.Integration.Contracts.Dto.ServiceDesk;

namespace MOHU.Integration.Application.Service.ServiceDesk;

public class ServiceDeskService(ITicketService ticketService) : IServiceDeskService
{
    public async Task<bool> UpdateStatusAsync(ServiceDeskUpdateStatusRequest request)
    {
        var ticketId = await ticketService
            .GetTicketByIntegrationTicketNumberAsync(request.TicketNumber, Incident.Fields.ServiceDeskTicketNumber);

        var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsServiceDeskUpdated);
            
        await ticketService.UpdateStatusAsync(ticketStatusRequest);

        return true;

    }
}