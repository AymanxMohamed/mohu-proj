using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Cache;
using MOHU.Integration.Contracts.Interface.Common;

namespace MOHU.Integration.Application.Service
{
    public class CommonService : ICommonService
    {
        private readonly ICrmContext _crmContext;
        private readonly ICacheService _cacheService;
        private readonly ICacheKeyGeneratorService _cacheKeyGeneratorService;
        public CommonService(ICrmContext crmContext, ICacheService cacheService /*,ICacheKeyGeneratorService cacheKeyGeneratorService*/)
        {
            _crmContext = crmContext;
            _cacheService = cacheService;
            //_cacheKeyGeneratorService = cacheKeyGeneratorService;
        }

        public async Task<IEnumerable<LookupValueDto>> GetLookups(string entityName, string language)
        {
            var primaryField = await GetEntityPrimaryField(entityName);

            var cacheKey = $"{entityName}-{primaryField}_{language}";

            var resultFromCache = await _cacheService.GetAsync<IEnumerable<LookupValueDto>>(cacheKey);
            if(resultFromCache is null)
            {
                var query = new QueryExpression(entityName)
                {
                    ColumnSet = new ColumnSet(primaryField),
                    NoLock = true
                };

                var result = await _crmContext.ServiceClient.RetrieveMultipleAsync(query);
                var lookups = new List<LookupValueDto>();

                foreach (var record in result.Entities)
                {
                    var concatenatedName = record.GetAttributeValue<string>(primaryField).Split('-');
                    var lookup = new LookupValueDto
                    {
                        Id = record.Id,
                        Name = !language.Contains("ar") ? concatenatedName?.FirstOrDefault() : concatenatedName?.LastOrDefault()
                    };

                    lookups.Add(lookup);
                }
                await _cacheService.SetAsync(cacheKey, lookups);
                resultFromCache = lookups;
            }
           return resultFromCache;
        }

        public async Task<IEnumerable<OptionDto>> GetOptionSet(string entityName, string optionSetName, string language)
        {
            var cacheKey = $"{entityName}-{optionSetName}_{language}";
            var resumtFromCache = await _cacheService.GetAsync<IEnumerable<OptionDto>>(cacheKey);
            if(resumtFromCache is null)
            {
                var result = new List<OptionDto>();
                var attributeRequest = new RetrieveAttributeRequest
                {
                    EntityLogicalName = entityName,
                    LogicalName = optionSetName,
                    RetrieveAsIfPublished = true
                };

                var attributeResponse = (RetrieveAttributeResponse)await _crmContext.ServiceClient.ExecuteAsync(attributeRequest);
                var attributeMetadata = (EnumAttributeMetadata)attributeResponse.AttributeMetadata;

                var optionList = (from o in attributeMetadata.OptionSet.Options
                                  select new { o.Value, Text = o.Label }).ToList();
                foreach (var item in optionList)
                {
                    var data = new OptionDto
                    {
                        Name = language.Contains("ar") && item.Text.LocalizedLabels.FirstOrDefault(x => x.LanguageCode == 1025) is not null
                            ? item.Text.LocalizedLabels.FirstOrDefault(x => x.LanguageCode == 1025)?.Label
                            : item.Text.LocalizedLabels.FirstOrDefault(x => x.LanguageCode == 1033)?.Label,

                        Value = item.Value
                    };
                    result.Add(data);
                }
               await _cacheService.SetAsync(cacheKey, result);
                resumtFromCache = result;
            }
     
            return resumtFromCache;
        }

        private async Task<string> GetEntityPrimaryField(string entityName)
        {
            var request = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = entityName
            };
            RetrieveEntityResponse response = (RetrieveEntityResponse)await _crmContext.ServiceClient.ExecuteAsync(request);
            return response.EntityMetadata.PrimaryNameAttribute;
        }
    }
}

