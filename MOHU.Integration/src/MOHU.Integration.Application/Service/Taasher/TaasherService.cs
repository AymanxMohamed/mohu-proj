using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Taasher;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service.Taasher
{
    public class TaasherService : ITaasherService
    {

        private readonly ITicketService _ticketService;
        public TaasherService(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<bool> UpdateStatusAsync(TaasherUpdateStatusRequest request)
        {
            var ticketId = await _ticketService.GetTicketByIntegrationTicketNumberAsync("ldv_taasherticketnumber", request.TicketNumber);
            var ticketStatusRequest = new UpdateTicketStatusRequest
            {
                TicketId = ticketId,
                IntegrationStatus = request.IntegrationStatus,
                FlagLogicalName = Incident.Fields.IsTashirUpdated,
                Resolution = request.Resolution,
                ResolutionDate = request.ResolutionDate

            };
            await _ticketService.UpdateStatusAsync(ticketStatusRequest);

            return true;
         
        }

    }
}
