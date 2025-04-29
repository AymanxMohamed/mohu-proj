using Common.Crm.Infrastructure.Repositories.Concretes;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using MOHU.Integration.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Services
{
    public class CaseRelatedFieldsService(
        ICrmContext crmContext,
        ILogger<KidanaDetailsService> logger)
    {
        public async Task<ErrorOr<Guid>> CreateCrmRecord(KidanaDetailsResponse data, string ticketId)
        {
            try
            {
                
                var existingResult = await GetExistingRecordId(ticketId);

                if (existingResult.IsError)
                {
                    return existingResult.Errors;
                }

                if (existingResult.Value.HasValue)
                {
                    logger.LogInformation("Existing CRM record found for ticket {TicketId}", ticketId);
                    return existingResult.Value.Value;
                }

                var entity = CreateEntity(data, ticketId);

                var recordId = await crmContext.ServiceClient.CreateAsync(entity);
                return recordId;
            }
            catch (Exception ex)
            {
                LogAndReturnError(ex, ticketId);
                return Error.Unexpected("CRM_CREATION_FAILED", ex.Message);
            }
        }

        #region Private Methods


        private async Task<ErrorOr<Guid?>> GetExistingRecordId(string ticketId)
        {
            try
            {
                var query = BuildExistingRecordQuery(ticketId);
                var result = await crmContext.ServiceClient.RetrieveMultipleAsync(query);
                return result.Entities.FirstOrDefault()?.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking for existing record");
                return Error.Unexpected("EXISTING_RECORD_CHECK_FAILED", ex.Message);
            }
        }

        private static QueryExpression BuildExistingRecordQuery(string ticketId) => new(ldv_caserelatedfields.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(ldv_caserelatedfields.Fields.Id),
            Criteria = new FilterExpression
            {
                Conditions =
            {
                new ConditionExpression(
                    ldv_caserelatedfields.Fields.Name,
                    ConditionOperator.Equal,
                    $"KIDANA_{ticketId}")
            }
            },
            TopCount = 1
        };

        private Entity CreateEntity(KidanaDetailsResponse data, string ticketId)
        {
            var entity = new Entity(ldv_caserelatedfields.EntityLogicalName)
            {
                [ldv_caserelatedfields.Fields.Name] = $"KIDANA_{ticketId}",
                [ldv_caserelatedfields.Fields.ExternalStatus] = ParseStatus(data.ExternalStatus)
            };

            MapOptionalFields(entity, data);
            return entity;
        }

        private static OptionSetValue ParseStatus(string status)
        {
            return status?.ToUpper() switch
            {
                "CANCELLED" => new((int)ldv_caserelatedfields.ExternalStatus_OptionSet.CANCELLED),
                "CLOSED" => new((int)ldv_caserelatedfields.ExternalStatus_OptionSet.CLOSED),
                "REJECTED" => new((int)ldv_caserelatedfields.ExternalStatus_OptionSet.REJECTED),
                "OOS" => new((int)ldv_caserelatedfields.ExternalStatus_OptionSet.OOS),
                "OOSC" => new((int)ldv_caserelatedfields.ExternalStatus_OptionSet.OOSC),
                "RESOLVED" => new((int)ldv_caserelatedfields.ExternalStatus_OptionSet.RESOLVED),
                _ => throw new ArgumentException($"Invalid ExternalStatus: {status}")
            };
        }

        private static void MapOptionalFields(Entity entity, KidanaDetailsResponse data)
        {
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.Source, data.Source);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.PartySource, data.PartySource);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ExternalStatusDate, data.ExternalStatusDate);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ExternalReportDate, data.ExternalReportDate);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.SiteId, data.SiteId);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.KidanaDescription, data.KidanaDescription);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.CategoryDetails, data.CategoryDetails);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.KidanaExternalTicketId, data.KidanaExternalTicketId);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ExternalParty, data.ExternalParty);
        }

        private static void MapIfNotEmpty(Entity entity, string field, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                entity[field] = value;
        }

        private void LogAndReturnError(Exception ex, string ticketId)
        {
            logger.LogError(ex, "CRM record creation failed for ticket {TicketId}", ticketId);
        }

        #endregion
    }
}
