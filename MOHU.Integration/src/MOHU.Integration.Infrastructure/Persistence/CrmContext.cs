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
                var clientId = "";
                var clientSecret = "";
                var url = "";
                var connectionString = $"AuthType={authType};ClientId={clientId};Url={url};ClientSecret={clientSecret};";
                _serviceClient = new ServiceClient(connectionString);
            }
            return _serviceClient;

        }
    }
}
