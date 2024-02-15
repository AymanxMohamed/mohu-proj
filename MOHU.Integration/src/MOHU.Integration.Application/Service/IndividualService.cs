using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.Application.Service
{
    public class IndividualService : IIndividualService
    {
        private readonly ICrmContext _crmContext;

        public IndividualService(ICrmContext crmContext)
        {
            _crmContext = crmContext;
        }

        public async Task<LookupDto?> GetIndividualByMobileNumberAsync(string mobileNumber)
        {
            var query = new QueryExpression("contact")
            {
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            query.Criteria.AddFilter(filter);
            var result = await _crmContext.ServiceClient.RetrieveMultipleAsync(query);
            
            if (result.Entities.Any())
                return new LookupDto { Id = result.Entities.FirstOrDefault().Id, EntityLogicalName = result.Entities.FirstOrDefault().LogicalName };
           
            return null;
        }

        public async Task<LookupDto> CreateIndividualAsync(string mobileNumber)
        {
            var entity = new Entity("contact");
            entity.Attributes.Add("mobilephone", mobileNumber);
            var individualId = await _crmContext.ServiceClient.CreateAsync(entity);
            return new LookupDto { Id = individualId, EntityLogicalName = entity.LogicalName };
        }
    }
}
