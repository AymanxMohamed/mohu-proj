using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Kidana;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service.Kidana
{
    public class KidanaService : IKidanaService 
    {
        private readonly ITicketService _ticketService;
        public KidanaService(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<bool> UpdateStatusAsync(KidanaUpdateStatusRequest request)
        {
            var ticketId = await _ticketService.GetTicketByIntegrationTicketNumberAsync(request.TicketId.ToString());

            var ticketStatusRequest = new UpdateTicketStatusRequest
            {
                TicketId = ticketId,
                IntegrationStatus = request.IntegrationStatus,
                FlagLogicalName = Incident.Fields.IsKadanaUpdated,
                Resolution = request.Resolution,
                ResolutionDate = request.ResolutionDate
            };
            await _ticketService.UpdateStatusAsync(ticketStatusRequest);

            return true;

        }

    }
}
