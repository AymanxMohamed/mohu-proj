using Core.Infrastructure.Integrations.Clients.Settings;
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
        public bool UseFileClients { get; init; }
        public string TestDataPath { get; init; } = "D:/Git Mapping/MOHU.Integration/src/MOHU.Integration.WebApi/Files/Kidana/Data/Kidana.json";
    }
}
