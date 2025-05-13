using MOHU.Integration.Contracts.Dto.ServiceDesk;

namespace MOHU.Integration.Application.Service.ServiceDesk;

public class ServiceDeskService(ITicketService ticketService, IValidator<UpdateTicketStatusData> validator) 
    : IServiceDeskService
{
    public async Task<bool> UpdateStatusAsync(ServiceDeskUpdateStatusRequest request)
    {
        var results = await validator.ValidateAsync(request);

        if (results?.IsValid == false)
        {
            throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }
        
        var ticketId = await ticketService
            .GetTicketIdByIntegrationTicketNumberAsync(request.Title, Incident.Fields.Title);

        var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsServiceDeskUpdated);

        await ticketService.UpdateStatusAsync(ticketStatusRequest);

        return true;
    }
    
    public async Task<Guid> GetTicketBySdNumber(string sdNumber)
    {
        var ticketId = await ticketService
            .GetTicketIdByIntegrationTicketNumberAsync(sdNumber, Incident.Fields.ServiceDeskTicketNumber);

        return ticketId;
    }
}