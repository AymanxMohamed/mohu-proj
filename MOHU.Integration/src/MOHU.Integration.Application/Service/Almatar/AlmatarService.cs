using MOHU.Integration.Application.Features.Tickets.Services;
using MOHU.Integration.Contracts.Dto.Almatar;
using MOHU.Integration.Contracts.Dto.Sahab;
using MOHU.Integration.Contracts.Dto.Taasher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Service.Almatar
{
    public class AlmatarService(ITicketService _ticketService, IValidator<UpdateTicketStatusData> _updateTicketValidator) : IAlmatarService
    {
        public async Task<bool> UpdateStatusAsync(AlmatarUpdateStatusRequest request)
        {

            var results = await _updateTicketValidator.ValidateAsync(request);

            if (results?.IsValid == false)
            {
                throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
            }
            var ticketId = await _ticketService.GetTicketIdByIntegrationTicketNumberAsync(
                request.TicketNumber,
            Incident.Fields.AlMatarTicketNumber);

            var ticketStatusRequest = request.ToUpdateRequest(ticketId, Incident.Fields.IsAlmatarUpdated);

            await _ticketService.UpdateStatusAsync(ticketStatusRequest);

            return true;
        }

       
    }
}
