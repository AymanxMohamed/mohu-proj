using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Config
{
    public class MemoryCacheConfig
    {
        // TODO: Add the rest of properties
        // TODO: Configure it using IOptions
        public int ExpirationInMinutes { get; set; }
    }
}
