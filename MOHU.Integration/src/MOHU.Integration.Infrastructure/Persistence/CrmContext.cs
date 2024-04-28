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
                // production 
                var clientId = "beb8c91e-b78f-4fff-a7f4-f12a3d1b8eb9";
                var clientSecret = "RRR8Q~WogMBTr7yJ6DylLTr72.F2GMW7LXdQfcD5";
                var url = "https://mohu-crm365.crm4.dynamics.com";


                //stage
                //var clientId = "5476a246-c114-482e-a356-531964022997";
                //var clientSecret = "rCX8Q~cxmdBTujQAGhVYnqG35MZSqA5YCMolRdeC";
                //var url = "https://mohustg.crm4.dynamics.com/";
                
                //dev
                //var clientId = "ec070189-1252-4ba0-9b8c-7fdd2477b02d";
                //var clientSecret = "eu-8Q~gah3Lph_ye.wg.IEm1pHzaU5ndY3rQnazP";
                //var url = "https://mohudev.crm4.dynamics.com/";
                var connectionString = $"AuthType={authType};ClientId={clientId};Url={url};ClientSecret={clientSecret};";
                _serviceClient = new ServiceClient(connectionString);
            }
            return _serviceClient;

        }
    }
}
