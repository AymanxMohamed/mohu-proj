using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Common
{
    public  class HelperMethod: IHelperMethod
    {

        private readonly ICrmContext _crmContext;
        public HelperMethod(ICrmContext crmContext)
        {

            _crmContext = crmContext;

        }

        public async Task<bool> CheckTicketIdExist(Guid TicketId)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Incident.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Incident.Fields.Id, ConditionOperator.Equal, TicketId));
            queryContact.Criteria.AddFilter(filter);
            var response = (await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckCustomerExist(Guid CustId)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Incident.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Incident.Fields.CustomerId, ConditionOperator.Equal, CustId));
            queryContact.Criteria.AddFilter(filter);
            var response = (await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }




    }
}
