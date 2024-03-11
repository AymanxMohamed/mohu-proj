using Microsoft.PowerPlatform.Dataverse.Client;
using MOHU.Integration.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Infrastructure.Persistence
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
                var clientId = "ec070189-1252-4ba0-9b8c-7fdd2477b02d";
                var clientSecret = "eu-8Q~gah3Lph_ye.wg.IEm1pHzaU5ndY3rQnazP";
                var url = "https://mohudev.crm4.dynamics.com/";
                var connectionString = $"AuthType={authType};ClientId={clientId};Url={url};ClientSecret={clientSecret};";
                _serviceClient = new ServiceClient(connectionString);
            }
            return _serviceClient;

        }
    }

}
