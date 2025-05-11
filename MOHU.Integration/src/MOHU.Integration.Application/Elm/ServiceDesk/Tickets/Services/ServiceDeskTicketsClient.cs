namespace MOHU.Integration.Application.Elm.ServiceDesk.Tickets.Services;

public partial class ServiceDeskTicketsClient(IHttpClientFactory httpClientFactory, IConfigurationService configurationService) 
    : IServiceDeskTicketsClient;