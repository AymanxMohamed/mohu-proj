using MOHU.Integration.Contracts.Dto.ServiceDesk;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service.ServiceDesk
{
    public class ServiceDeskService  : IServiceDeskService
    {
        private readonly ITicketService _ticketService;
        public ServiceDeskService(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<bool> UpdateStatusAsync(ServiceDeskUpdateStatusRequest request)
        {
            var ticketId = await _ticketService.GetTicketByIntegrationTicketNumberAsync(request.TicketNumber);

            var ticketStatusRequest = new UpdateTicketStatusRequest
            {
                TicketId = ticketId,
                IntegrationStatus = request.IntegrationStatus,
                FlagLogicalName = Incident.Fields.IsServiceDeskUpdated,
                Resolution = request.Resolution,
                ResolutionDate = request.ResolutionDate

            };
            await _ticketService.UpdateStatusAsync(ticketStatusRequest);

            return true;

        }


    }
}
