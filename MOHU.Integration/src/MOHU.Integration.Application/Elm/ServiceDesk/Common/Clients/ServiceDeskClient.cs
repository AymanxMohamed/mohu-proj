using Core.Infrastructure.Integrations.Clients;
using Microsoft.Extensions.Logging;

namespace MOHU.Integration.Application.Elm.ServiceDesk.Common.Clients;

internal class ServiceDeskClient(
    ServiceDeskApiSettings serviceDeskApiSettings,
    ILogger<ServiceDeskClient> logger)
    : RestClientService(serviceDeskApiSettings, logger), IServiceDeskClient;