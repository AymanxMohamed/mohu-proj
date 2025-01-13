using MOHU.Integration.Application.Validators;
using MOHU.Integration.Contracts.Dto.ServiceDesk;
using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Application.Service.ServiceDesk;

public class ServiceDeskService(ITicketService ticketService, IValidator<ServiceDeskUpdateStatusRequest> validator) : IServiceDeskService
{
    public async Task<bool> UpdateStatusAsync(ServiceDeskUpdateStatusRequest request)
    {
        //var validatationMessage = request.Validate();
        //if (validatationMessage != null) { 
        //    throw new BadRequestException(validatationMessage);
        //}
        var results = await validator.ValidateAsync(request);

        if (results?.IsValid == false)
        {
            throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }
        var ticketId = await ticketService
            .GetTicketByIntegrationTicketNumberAsync(request.TicketNumber, Incident.Fields.ServiceDeskTicketNumber);

        var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsServiceDeskUpdated);

        await ticketService.UpdateStatusAsync(ticketStatusRequest);

        return true;
    }


    
}