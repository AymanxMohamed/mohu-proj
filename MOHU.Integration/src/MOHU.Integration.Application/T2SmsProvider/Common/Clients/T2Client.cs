using Core.Infrastructure.Integrations.Clients;
using Microsoft.Extensions.Logging;

namespace MOHU.Integration.Application.T2SmsProvider.Common.Clients;

internal class T2Client(
    T2ApiSettings t2ApiSettings,
    ILogger<T2Client> logger)
    : RestClientService(t2ApiSettings, logger), IT2Client;