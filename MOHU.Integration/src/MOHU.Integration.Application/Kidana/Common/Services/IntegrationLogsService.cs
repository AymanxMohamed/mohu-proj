using Common.Crm.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using MOHU.Integration.Infrastructure.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Services
{
    public class IntegrationLogsService(ICrmContext crmContext,
        ILogger<IntegrationLogsService> logger)

    {
        public Guid CreateCompleteLog(
         string ticketId,
         object request,
         ErrorOr<KidanaDetailsResponse> result,
         ldv_integrationlogs.IntegrationTypeCode_OptionSet integrationType,
         ldv_integrationlogs.IntegrationOperationCode_OptionSet operation)
        {
            try
            {
                Entity log = new Entity(ldv_integrationlogs.EntityLogicalName)
                {
                    [ldv_integrationlogs.Fields.Name] = $"Kidana_{ticketId}",
                    [ldv_integrationlogs.Fields.IntegrationTypeCode] = new OptionSetValue((int)integrationType),
                    [ldv_integrationlogs.Fields.IntegrationOperationCode] = new OptionSetValue((int)operation),
                    [ldv_integrationlogs.Fields.ExternalTicketNumber] = ticketId,
                    [ldv_integrationlogs.Fields.ExternalTicketId] = ticketId,
                    [ldv_integrationlogs.Fields.ApiRequest] = JsonConvert.SerializeObject(request),
                    [ldv_integrationlogs.Fields.Trace] = result.Match(
                         success => JsonConvert.SerializeObject(success),
                         error => JsonConvert.SerializeObject(error))
                };

                return crmContext.ServiceClient.Create(log);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to create integration log for ticket {TicketId}", ticketId);
                return Guid.Empty;

            }

        }

        public async Task UpdateLogWithCaseRelatedFieldsReference(Guid logId, Guid caseId)
        {
            try
            {
                var updateEntity = new Entity(ldv_integrationlogs.EntityLogicalName, logId)
                {
                    [ldv_integrationlogs.Fields.CaseRelatedFieldsId] =
                        new EntityReference(ldv_caserelatedfields.EntityLogicalName, caseId)
                };

                await crmContext.ServiceClient.UpdateAsync(updateEntity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update integration log with Case Related Fields ID");
            }
        }
    }
}
