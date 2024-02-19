using Microsoft.Extensions.Caching.Memory;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Infrastructure.Service
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ICrmContext _crmContext;
        private readonly IMemoryCache _memoryCache;
        public ConfigurationService(ICrmContext crmContext, IMemoryCache memoryCache)
        {
            _crmContext = crmContext;
            _memoryCache = memoryCache;
        }

        public async Task<string> GetConfigurationValueAsync(string key)
        {
            var query = new QueryExpression(ldv_configuration.EntityLogicalName)
            {
                TopCount = 1,
                NoLock = true
            };
            query.ColumnSet.AddColumn(ldv_configuration.Fields.ldv_Value);
            query.Criteria.AddCondition(ldv_configuration.Fields.ldv_name, ConditionOperator.Equal, key);

            var result = (await _crmContext.ServiceClient.RetrieveMultipleAsync(query))?.Entities?.FirstOrDefault();
            return result?.GetAttributeValue<string>(ldv_configuration.Fields.ldv_Value) ?? string.Empty;
        }
    }
}
