using Core.Domain.ErrorHandling.Exceptions;
using MOHU.Integration.Contracts.Dto.Kidana;

namespace MOHU.Integration.Application.Service.Kidana;

public class KidanaService(ITicketService ticketService , IValidator<UpdateTicketStatusData> validator) : IKidanaService
{
    public async Task<bool> UpdateStatusAsync(KidanaUpdateStatusRequest request)
    {
        var results = await validator.ValidateAsync(request);

        if (results?.IsValid == false)
        {
            throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }
        var ticketId = await ticketService.GetTicketByIntegrationTicketNumberAsync(
            request.TicketId.ToString(), 
            Incident.Fields.KidanaTicketNumber);
            
        var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsKadanaUpdated);
            
        await ticketService.UpdateStatusAsync(ticketStatusRequest);

        return true;
    }
}