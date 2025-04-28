using Core.Infrastructure.Integrations.Clients.Settings;

namespace MOHU.Integration.Application.T2SmsProvider.Common.Clients;

public class T2ApiSettings : RestClientSettings
{
    public const string SectionName = nameof(T2ApiSettings);

    public required string UserName { get; init; }

    public required string Password { get; init; }

    public required string Sender { get; init; }
}