using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service.Kedana
{
    public class KedanaService : IKedanaService 
    {
        private readonly ITicketService _ticketService;
        public KedanaService(ITicketService ticketService)
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
                FlagLogicalName = Incident.Fields.IsKadanaUpdated,
                Resolution = request.Resolution,
                ResolutionDate = request.ResolutionDate

            };
            await _ticketService.UpdateStatus(ticketStatusRequest);

            return true;

        }

    }
}
