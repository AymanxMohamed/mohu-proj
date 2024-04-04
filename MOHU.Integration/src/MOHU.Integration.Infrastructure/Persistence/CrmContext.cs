using Microsoft.PowerPlatform.Dataverse.Client;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.Infrastructure.Persistence
{
    public class CrmContext : ICrmContext
    {
        private ServiceClient? _serviceClient;
        public ServiceClient ServiceClient => GetServiceClient();


        private ServiceClient GetServiceClient()
        {
            if (_serviceClient == null)
            {
                var authType = "ClientSecret";
                var clientId = "beb8c91e-b78f-4fff-a7f4-f12a3d1b8eb9";
                var clientSecret = "RRR8Q~WogMBTr7yJ6DylLTr72.F2GMW7LXdQfcD5";
                var url = "https://mohu.crm4.dynamics.com/";
                var connectionString = $"AuthType={authType};ClientId={clientId};Url={url};ClientSecret={clientSecret};";
                _serviceClient = new ServiceClient(connectionString);
            }
            return _serviceClient;

        }
    }
}
