using Microsoft.BusinessData.MetadataModel;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto.Kedana;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace MOHU.ExternalIntegration.Application.Service.Kedana
{
    public class KedanaUpdateStatusService : IKedanaUpdateStatusService
    {
        private readonly ICrmContext crmContext;
        public KedanaUpdateStatusService(ICrmContext crmContext)
        {
            this.crmContext = crmContext;
        }

        public async Task<bool> KedanaUpdateStatus(KedanaUpdateStatusResponse model)
        {

            if (model.CustId == Guid.Empty)
            {
                throw new NotFoundException("Customer Id is Required ");
            }
            if (model.TicketId == Guid.Empty)
            {
                throw new NotFoundException("Ticket Id is Required ");
            }

            var TicketidExist = await CheckTicketIdExist(model.TicketId);

            var isCustExist = await CheckCustomerExist(model.CustId);
            if (!isCustExist)
            {
                throw new NotFoundException("This Customer is not Exist  ");
            }
            var query = new QueryExpression()
            {
                EntityName = Incident.EntityLogicalName,
                NoLock = true
            };

            if (TicketidExist == true)
            {

                var Ticket = new Microsoft.Xrm.Sdk.Entity(Incident.EntityLogicalName)
                {
                    Id = model.TicketId
                };
                // Ticket.Attributes.Add(Incident.Fields.ldv_ClosureReason, model.Resolution);
                Ticket.Attributes.Add(Incident.Fields.IntegrationClosureReason, model.Resolution);
                Ticket.Attributes.Add(Incident.Fields.IntegrationClosureDate, model.ResolutionDate);
                Ticket.Attributes.Add(Incident.Fields.IntegrationStatus,
              new OptionSetValue(Convert.ToInt32(model.IntegrationStatus)));


                Ticket.Attributes.Add(Incident.Fields.IsKadanaUpdated, model.IsKadanaUpdated);



                crmContext.ServiceClient.Update(Ticket);

                return true;

            }
            return false;

        }

        // //check CustomerExist 
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
            var response = crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckTicketIdExist(Guid TicketId)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Incident.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(ldv_Incident.Fields.Id, ConditionOperator.Equal, TicketId));
            queryContact.Criteria.AddFilter(filter);
            var response = crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }






    }
}
