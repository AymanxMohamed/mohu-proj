using Microsoft.Extensions.Options;
using Microsoft.PowerPlatform.Dataverse.Client;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Infrastructure.Settings;

namespace MOHU.Integration.Infrastructure.Persistence
{
    public class CrmContext(IOptions<CrmContextSettings> crmContextSettings) : ICrmContext
    {
        private ServiceClient? _serviceClient;
        public ServiceClient ServiceClient => GetServiceClient();
        private ServiceClient GetServiceClient()
        {
            if (_serviceClient != null) return _serviceClient;
            _serviceClient = new ServiceClient(crmContextSettings.Value.GetConnectionString());
            return _serviceClient;
        }
    }
}