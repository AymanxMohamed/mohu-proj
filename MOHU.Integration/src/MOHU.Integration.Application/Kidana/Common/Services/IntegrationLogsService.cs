using Common.Crm.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Services
{
    public class IntegrationLogsService
    {
        public Entity CreateCompleteLog(
         string ticketId,
         object request,
         ErrorOr<KidanaResponseBase<KidanaDetailsResponse>> result,
         ldv_integrationlogs.IntegrationTypeCode_OptionSet integrationType,
         ldv_integrationlogs.IntegrationOperationCode_OptionSet operation)
        {
            return new Entity(ldv_integrationlogs.EntityLogicalName)
            {
                [ldv_integrationlogs.Fields.Name] = $"Kidana_{ticketId}",
                [ldv_integrationlogs.Fields.IntegrationTypeCode] = new OptionSetValue((int)integrationType),
                [ldv_integrationlogs.Fields.IntegrationOperationCode] = new OptionSetValue((int)operation),
                [ldv_integrationlogs.Fields.ExternalTicketNumber] = ticketId,
                [ldv_integrationlogs.Fields.ApiRequest] = JsonConvert.SerializeObject(request),
                [ldv_integrationlogs.Fields.Trace] = result.Match(
                    success => JsonConvert.SerializeObject(success),
                    error => JsonConvert.SerializeObject(error))
            };
        }

    }
}
