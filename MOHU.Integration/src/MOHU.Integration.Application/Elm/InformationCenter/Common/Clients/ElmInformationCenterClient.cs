using Core.Infrastructure.Integrations.Clients;
using Microsoft.Extensions.Logging;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;

internal class ElmInformationCenterClient(
    ElmInformationCenterApiSettings elmInformationCenterApiSettings,
    ILogger<ElmInformationCenterClient> logger)
    : RestClientService(elmInformationCenterApiSettings, logger), IElmInformationCenterClient;