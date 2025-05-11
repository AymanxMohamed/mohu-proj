using Core.Infrastructure.Integrations.Clients.Settings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Clients
{
    public class KidanaApiSettings : RestClientSettings
    {

        public const string SectionName = nameof(KidanaApiSettings);

        public string ValidateTicketEndpoint { get; init; } = null!;
        public Dictionary<string, ParameterValue> DefaultParams { get; init; } = new();

    }

}
