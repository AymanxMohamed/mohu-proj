using Core.Infrastructure.Integrations.Clients.Settings;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;

public class ElmInformationCenterApiSettings : RestClientSettings
{
    public const string SectionName = nameof(ElmInformationCenterApiSettings);

    public bool UseFileClients { get; init; }

    public string LookupsMainCollection { get; set; } = LookupsConstants.MainCollectionName;
}