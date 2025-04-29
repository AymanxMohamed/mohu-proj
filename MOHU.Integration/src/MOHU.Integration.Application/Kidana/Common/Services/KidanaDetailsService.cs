using Common.Crm.Infrastructure.Repositories.Concretes;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Clients;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Services
{
    public class KidanaDetailsService(
            IKidanaClient client,
            CaseRelatedFieldsService caseRelatedFieldsService,
            ILogger<KidanaDetailsService> logger) : IKidanaDetailsService
    {
        public async Task<ErrorOr<TicketValidationResult>> ValidateTicketWithCrmCheck(string ticketId)
        {
            var kidanaResponse =  client.ValidateTicket(ticketId);
            
            return await kidanaResponse.MatchAsync(
                async response => await ProcessValidResponse(response, ticketId),
                error => Task.FromResult<ErrorOr<TicketValidationResult>>(error)
            );
        }

        private async Task<ErrorOr<TicketValidationResult>> ProcessValidResponse(
            KidanaResponseBase<KidanaDetailsResponse> response,
            string ticketId)
        {
            if (response.Data == null)
            {
                return new TicketValidationResult
                {
                    TicketExists = false,
                    Status = "Not Found"
                };
            }

            var result = new TicketValidationResult
            {
                TicketExists = true,
                Status = response.Data.ExternalStatus
            };

            if (response.Data.ExternalStatus?.Equals("Closed", StringComparison.OrdinalIgnoreCase) == true)
            {
                var crmResult = await caseRelatedFieldsService.CreateCrmRecord(response.Data, ticketId);

                result.CrmRecordId = crmResult.IsError ? null : crmResult.Value;
                result.CrmRecordName = crmResult.IsError ? null : $"KIDANA_{ticketId}";

                if (crmResult.IsError)
                {
                    logger.LogError("CRM creation failed: {Error}", crmResult.FirstError.Description);
                }
            }

            return result;
        }

    }
}
