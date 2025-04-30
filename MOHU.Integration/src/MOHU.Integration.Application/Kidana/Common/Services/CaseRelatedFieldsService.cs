using Common.Crm.Infrastructure.Repositories.Concretes;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using MOHU.Integration.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MOHU.Integration.Domain.Entitiy.ldv_caserelatedfields;

namespace MOHU.Integration.Application.Kidana.Common.Services
{
    public class CaseRelatedFieldsService(
        ICrmContext crmContext,
        ILogger<CaseRelatedFieldsService> logger)
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
                [ldv_caserelatedfields.Fields.ExternalStatus] = ParseStatus(data.Status)
            };

            MapOptionalFields(entity, data);
            return entity;
        }

        private static readonly Dictionary<string, OptionSetValue> _statusMappings =
        CreateStatusMappings();

        private static Dictionary<string, OptionSetValue> CreateStatusMappings()
        {
            var mappings = new Dictionary<string, OptionSetValue>(
                StringComparer.OrdinalIgnoreCase
            );

            foreach (ExternalStatus_OptionSet status in Enum.GetValues(typeof(ExternalStatus_OptionSet)))
            {
                var statusName = status.ToString().ToUpper();
                mappings.Add(statusName, new OptionSetValue((int)status));
            }

            return mappings;
        }

        private static OptionSetValue ParseStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentException("Status cannot be null or empty", nameof(status));
            }

            var upperStatus = status.ToUpper();

            if (_statusMappings.TryGetValue(upperStatus, out var optionSet))
            {
                return optionSet;
            }

            throw new ArgumentException($"Invalid ExternalStatus: {status}");
        }

        private static void MapOptionalFields(Entity entity, KidanaDetailsResponse data)
        {
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.Source, data.Source);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.PartySource, data.ZzpartySource);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ExternalStatusDate, data.StatusDate);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ExternalReportDate, data.ReportDate);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.SiteId, data.SiteId);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.KidanaDescription, data.Description);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.CategoryDetails, data.ClassStructureId);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.KidanaExternalTicketId, data.ZzTicketId);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ExternalParty, data.ZzExtParty);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ApplicantName, data.ZzRequestor);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.ApplicantPhoneNumber, data.ApplicantPhoneNumber);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.AssetNumber, data.AssetNumber);
            MapIfNotEmpty(entity, ldv_caserelatedfields.Fields.Location, data.Location);

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
