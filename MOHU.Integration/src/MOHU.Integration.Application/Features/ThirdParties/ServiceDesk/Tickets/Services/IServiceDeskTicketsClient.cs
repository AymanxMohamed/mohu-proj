using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;
using SDIntegraion;

namespace MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Tickets.Services;

public interface IServiceDeskTicketsClient
{
    public Task<TicketResponse> GetOrCreateServiceDeskTicket(ServiceDeskRequest request);
}