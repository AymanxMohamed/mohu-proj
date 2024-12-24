using Common.Crm.Infrastructure.Common.Interfaces;

namespace Common.Crm.Infrastructure.HttpClients
{
    public class CrmApiHttpClientService : BaseHttpClientService, ICrmApiHttpClientService
    {
        public CrmApiHttpClientService(string baseUrl, string crmDomain) : base(baseUrl)
        {
            HttpClient.DefaultRequestHeaders.Referrer = new Uri(crmDomain);
        }
    }
}