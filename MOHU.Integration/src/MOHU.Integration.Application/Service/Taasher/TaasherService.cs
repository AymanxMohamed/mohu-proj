using MOHU.Integration.Contracts.Dto;
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

        public async Task<bool> UpdateStatus(UpdateStatusRequest request)
        {

            var ticketStatusRequest = new UpdateTicketStatusRequest
            {
                TicketId = request.TicketId,
                CustomerId = request.CustomerId,
                IntegrationStatus = request.IntegrationStatus,
                FlagLogicalName = Incident.Fields.IsTashirUpdated,
                Resolution = request.Resolution,
                ResolutionDate = request.ResolutionDate

            };
            await _ticketService.UpdateStatus(ticketStatusRequest);

            return true;
         
        }

    }
}
