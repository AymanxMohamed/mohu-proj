using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface.Common
{
    public interface IConfigurationService
    {
        Task<string> GetConfigurationValueAsync(string key);
    }
}
