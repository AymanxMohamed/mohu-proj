using MOHU.Integration.Contracts.Interface.Common;

namespace MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Tickets.Services;

public partial class ServiceDeskTicketsClient(IHttpClientFactory httpClientFactory, IConfigurationService configurationService) 
    : IServiceDeskTicketsClient
{
}