using Microsoft.Extensions.Options;
using Microsoft.PowerPlatform.Dataverse.Client;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Infrastructure.Settings;

namespace MOHU.Integration.Infrastructure.Persistence
{
    public class CrmContext : ICrmContext
    {
        private ServiceClient? _serviceClient;
        public ServiceClient ServiceClient => GetServiceClient();
        
        private readonly CrmContextSettings _crmContextSettings;

        public CrmContext(IOptions<CrmContextSettings> crmContextSettings)
        {
            _crmContextSettings = crmContextSettings.Value;
        }


        private ServiceClient GetServiceClient()
        {
            if (_serviceClient != null) return _serviceClient;
            
            _serviceClient = new ServiceClient(_crmContextSettings.GetConnectionString());
            
            return _serviceClient;
        }
    }
}
