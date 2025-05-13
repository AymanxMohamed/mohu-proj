using MOHU.Integration.Contracts.Dto.Taasher;
using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Application.Service.Taasher;

public class TaasherService(ITicketService ticketService, IValidator<UpdateTicketStatusData> validator) : ITaasherService
{
    public async Task<bool> UpdateStatusAsync(TaasherUpdateStatusRequest request)
    {

        var results = await validator.ValidateAsync(request);

        if (results?.IsValid == false)
        {
            throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }
        var ticketId = await ticketService.GetTicketIdByIntegrationTicketNumberAsync(
            request.TicketNumber, 
            Incident.Fields.TaasherTicketNumber);
            
        var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsTashirUpdated);
            
        await ticketService.UpdateStatusAsync(ticketStatusRequest);

        return true;
    }
}