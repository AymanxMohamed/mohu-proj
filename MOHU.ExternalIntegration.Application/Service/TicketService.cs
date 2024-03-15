using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto;
using MOHU.ExternalIntegration.Contracts.Dto.Ticket;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
using MOHU.ExternalIntegration.Shared;

namespace MOHU.ExternalIntegration.Application.Service
{
    public class TicketService : ITicketService
    {
        private readonly ICrmContext _crmContext;
        private readonly IStringLocalizer _localizer;
        private readonly IConfiguration _configuration;
        public TicketService(ICrmContext crmContext, IConfiguration configuration, IStringLocalizer localizer)
        {
            _crmContext = crmContext;
            _configuration = configuration;
            _localizer = localizer;
        }

        public async Task UpdateStatus(UpdateStatusRequest request)
        {
            if (request.CustomerId == Guid.Empty)
                throw new NotFoundException(_localizer[ErrorMessageCodes.CustomerIdRquired]);

            if (request.TicketId == Guid.Empty)
                throw new NotFoundException(_localizer[ErrorMessageCodes.TicketIdisRequired]);

            var isTicketExists = await IsTicketExists(request.TicketId);

            if (!isTicketExists)
                throw new NotFoundException("Ticket does not exist");

            var entity = new Entity(Incident.EntityLogicalName, request.TicketId);

            entity.Attributes.Add(Incident.Fields.IntegrationClosureReason, request.Resolution);
            entity.Attributes.Add(Incident.Fields.IntegrationClosureDate, request.ResolutionDate);
            entity.Attributes.Add(Incident.Fields.IntegrationStatus,
               new OptionSetValue(Convert.ToInt32(request.IntegrationStatus)));
            entity.Attributes.Add(Incident.Fields.IsServiceDeskUpdated, true);

            await _crmContext.ServiceClient.UpdateAsync(entity);

        }
        public async Task<bool> CancelTicket(CancelTicketResponse ticket)
        {
            if (ticket.TicketId == Guid.Empty)
            {
                new BadRequestException("Please Enter Ticket id ");
            }

            var TicketidExist = await IsTicketExists(ticket.TicketId);

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
                _crmContext.ServiceClient.Update(Ticket);

                return true;
            }
            return false;

        }
        public async Task<bool> IsTicketExists(Guid ticketId)
        {
            var ticketQuery = new QueryExpression
            {
                EntityName = Incident.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Incident.Fields.Id, ConditionOperator.Equal, ticketId));
            ticketQuery.Criteria.AddFilter(filter);
            var result = await _crmContext.ServiceClient.RetrieveMultipleAsync(ticketQuery);
            return result.Entities.Any();
        }
    }
}
