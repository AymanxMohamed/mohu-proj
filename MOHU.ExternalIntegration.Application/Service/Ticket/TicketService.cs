using Microsoft.Extensions.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto.Ticket;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Application.Service.Ticket
{
    public class TicketService : ITicketService
    {
        private readonly ICrmContext crmContext;
        private readonly IConfiguration _configuration;
        public TicketService(ICrmContext crmContext, IConfiguration configuration)
        {
            this.crmContext = crmContext;
            this._configuration = configuration;
        }

        public async Task<bool> CancelTicket(CancelTicketResponse ticket)
        {
            if (ticket.TicketId == Guid.Empty)
            {
                new BadRequestException("Please Enter Ticket id ");
            }

            var TicketidExist = await CheckTicketIdExist(ticket.TicketId);

            if (TicketidExist == false)
            {
                new BadRequestException("This Ticket Case is not exist");
            }

            if (TicketidExist == true)
            {
                var Ticket = new Entity(Incident.EntityLogicalName)
                {
                    Id = ticket.TicketId
                };
                Ticket.Attributes.Add(Incident.Fields.Status,
               new OptionSetValue(Convert.ToInt32(ticket.Cancel)));

                var portalStatusId = _configuration["PortalStatus"];
                var StatusReasonId = _configuration["StatusReason"];

                Ticket.Attributes.Add(Incident.Fields.PortalStatus,
              new EntityReference(Incident.EntityLogicalName, Guid.Parse(portalStatusId)));

                Ticket.Attributes.Add(Incident.Fields.StatusReason,
             new EntityReference(Incident.EntityLogicalName, Guid.Parse(StatusReasonId)));
                crmContext.ServiceClient.Update(Ticket);

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
            filter.AddCondition(new ConditionExpression(Incident.Fields.Id, ConditionOperator.Equal, TicketId));
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
