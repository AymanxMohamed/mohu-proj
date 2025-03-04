using MOHU.Integration.Contracts.Interface.Cache;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Service;

public class ConfigurationService(ICrmContext crmContext, ICacheService cacheService) : IConfigurationService
{
    public async Task<TValue?> GetConfigurationValueAsync<TValue>(string key)
    {
        var value = await GetConfigurationValueAsync(key);

        return string.IsNullOrWhiteSpace(value) ? default : JsonConvert.DeserializeObject<TValue>(value);
    }
    
    public async Task<string> GetConfigurationValueAsync(string key)
    {
        var cacheKey = $"Configuration_{key}";
            
        var resultFromCache = await cacheService.GetAsync<string>(cacheKey);
            
        if (resultFromCache is not null) return resultFromCache;
            
        var query = new QueryExpression(ldv_configuration.EntityLogicalName)
        {
            TopCount = 1,
            NoLock = true
        };
            
        query.ColumnSet.AddColumn(ldv_configuration.Fields.ldv_Value);
            
        query.Criteria.AddCondition(ldv_configuration.Fields.ldv_name, ConditionOperator.Equal, key);

        var result = (await crmContext.ServiceClient.RetrieveMultipleAsync(query))?.Entities?.FirstOrDefault();

        resultFromCache = result?.GetAttributeValue<string>(ldv_configuration.Fields.ldv_Value) ?? string.Empty;

        await cacheService.SetAsync(cacheKey, resultFromCache);
        return resultFromCache;
    }

    public async Task SetOrUpdateConfigurationValueAsync(string key, string value)
    {
        var cacheKey = $"Configuration_{key}";
    
        var query = new QueryExpression(ldv_configuration.EntityLogicalName)
        {
            TopCount = 1,
            NoLock = true
        };

        query.ColumnSet.AddColumn(ldv_configuration.Fields.ldv_Value);
        query.Criteria.AddCondition(ldv_configuration.Fields.ldv_name, ConditionOperator.Equal, key);

        var existingRecord = (await crmContext.ServiceClient.RetrieveMultipleAsync(query))?.Entities?.FirstOrDefault();

        if (existingRecord is not null)
        {
            existingRecord[ldv_configuration.Fields.ldv_Value] = value;
            await crmContext.ServiceClient.UpdateAsync(existingRecord);
        }
        else
        {
            var newRecord = new Entity(ldv_configuration.EntityLogicalName)
            {
                [ldv_configuration.Fields.ldv_name] = key,
                [ldv_configuration.Fields.ldv_Value] = value
            };

            await crmContext.ServiceClient.CreateAsync(newRecord);
        }

        await cacheService.SetAsync(cacheKey, value);
    }
}