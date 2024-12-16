using MOHU.Integration.Contracts.Interface.Cache;

namespace MOHU.Integration.Application.Service
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ICrmContext _crmContext;
        private readonly ICacheService _cacheService;
        public ConfigurationService(ICrmContext crmContext, ICacheService cacheService)
        {
            _crmContext = crmContext;
            _cacheService = cacheService;
        }

        public async Task<string> GetConfigurationValueAsync(string key)
        {
            var cacheKey = $"Configuration_{key}";
            var resultFromCache = await _cacheService.GetAsync<string>(cacheKey);
            if (resultFromCache is null)
            {
                var query = new QueryExpression(ldv_configuration.EntityLogicalName)
                {
                    TopCount = 1,
                    NoLock = true
                };
                query.ColumnSet.AddColumn(ldv_configuration.Fields.ldv_Value);
                query.Criteria.AddCondition(ldv_configuration.Fields.ldv_name, ConditionOperator.Equal, key);

                var result = (await _crmContext.ServiceClient.RetrieveMultipleAsync(query))?.Entities?.FirstOrDefault();

                resultFromCache = result?.GetAttributeValue<string>(ldv_configuration.Fields.ldv_Value) ?? string.Empty;

                await _cacheService.SetAsync(cacheKey, resultFromCache);
            }
            return resultFromCache;

        }
    }
}
