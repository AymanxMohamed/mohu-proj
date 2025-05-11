using Common.Crm.Infrastructure.Repositories.Concretes;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Clients;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Infrastructure.Persistence;
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
            IntegrationLogsService integrationLogsService,
            ILogger<KidanaDetailsService> logger) : IKidanaDetailsService
    {

        public async Task<ErrorOr<TicketValidationResult>> ValidateTicketWithCrmCheck(string ticketId)
        {
            var kidanaResponse =  client.ValidateTicket(ticketId);

            return await kidanaResponse.MatchAsync(
            async response => await ProcessValidResponse(response.Result, ticketId, response.LogId),
            errors => HandleErrorResponse(errors)
        );
        }

        private Task<ErrorOr<TicketValidationResult>> HandleErrorResponse(List<Error> errors)
        {
            var notFoundError = errors.FirstOrDefault(e => e.Code == "TICKET_NOT_FOUND");

            if (notFoundError != null)
            {
                return Task.FromResult<ErrorOr<TicketValidationResult>>(
                    new TicketValidationResult
                    {
                        TicketExists = false,
                        Status = "Not Found"
                    }
                );
            }

            // For other errors, return the first error or combine them
            return Task.FromResult<ErrorOr<TicketValidationResult>>(
                errors.Count > 0
                    ? errors[0]
                    : Error.Unexpected("UNKNOWN_ERROR", "Unknown error occurred")
            );
        }

        private async Task<ErrorOr<TicketValidationResult>> ProcessValidResponse(
            KidanaDetailsResponse response,
            string ticketId,
            Guid logId)
        {

            var result = new TicketValidationResult
            {
                TicketExists = true,
                Status = response.Status
            };

            if (response.Status != null &&
                ldv_caserelatedfields.ClosedStatuses.Values.Contains(response.Status))
            {
                result.IsClosed = true;

                var crmResult = await caseRelatedFieldsService.CreateCrmRecord(response, ticketId);

                if (crmResult.IsError)
                {
                    logger.LogError("CRM creation failed: {Error}", crmResult.FirstError.Description);
                }
                else
                {
                    result.CrmRecordId = crmResult.IsError ? null : crmResult.Value;
                    result.CrmRecordName = crmResult.IsError ? null : $"KIDANA_{ticketId}";
                    await integrationLogsService.UpdateLogWithCaseRelatedFieldsReference(logId, crmResult.Value);
                }

            }

            return result;
        }

    }
}
