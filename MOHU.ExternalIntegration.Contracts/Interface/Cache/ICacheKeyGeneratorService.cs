using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface.Cache
{
    public interface ICacheKeyGeneratorService
    {
        Task<string> GenerateKey(string key, string language);
    }
}
