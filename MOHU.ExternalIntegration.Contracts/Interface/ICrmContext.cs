using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface ICrmContext
    {
        ServiceClient ServiceClient { get; }
    }
}
