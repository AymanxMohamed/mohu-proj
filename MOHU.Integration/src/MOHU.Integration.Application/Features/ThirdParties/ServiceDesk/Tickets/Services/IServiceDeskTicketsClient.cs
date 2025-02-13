using MOHU.Integration.Contracts.Dto.ServiceDeskProxy;
using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;
using SDIntegraion;

namespace MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Tickets.Services;

public interface IServiceDeskTicketsClient
{
    public Task<TicketResponse> GetOrCreateServiceDeskTicket(ServiceDeskRequest request);
    
    public Task<TicketResponse> UpdateTicket(ServiceDeskRequestUpdate request, string callId);

    Task<object> UpdateTicketOldVersion(ServiceDeskRequestUpdate request, string callId);
}