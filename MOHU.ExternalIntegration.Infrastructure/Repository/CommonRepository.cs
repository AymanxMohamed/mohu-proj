using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Contracts.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Infrastructure.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ICrmContext _crmContext;

        public CommonRepository(ICrmContext crmContext)
        {
            _crmContext = crmContext;
        }

        public async Task<IEnumerable<LookupValueDto>> GetLookups(string entityName, string language)
        {
            var primaryField = await GetEntityPrimaryField(entityName);
            var query = new QueryExpression(entityName)
            {
                ColumnSet = new ColumnSet(primaryField),
                NoLock = true
            };

            var result = await _crmContext.ServiceClient.RetrieveMultipleAsync(query);
            var lookups = new List<LookupValueDto>();

            foreach (var record in result.Entities)
            {
                var lookup = new LookupValueDto
                {
                    Id = record.Id,
                    Name = record.GetAttributeValue<string>(primaryField)
                };

                lookups.Add(lookup);
            }

            return lookups;
        }

        public async Task<IEnumerable<OptionDto>> GetOptionSet(string entityName, string optionSetName, string language)
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
            return result;
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
