using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Shared;

namespace MOHU.Integration.Application.Service
{
    public class TicketService : ITicketService
    {
        bool IsArabic => true;
        private readonly ICrmContext _crmContext;
        private readonly ICommonRepository _commonRepository;
        public TicketService(ICrmContext crmContext, ICommonRepository commonRepository)
        {
            _crmContext = crmContext;
            _commonRepository = commonRepository;
        }
        public async Task<TicketListResponse> GetAllTicketsAsync(Guid customerId, int pageNumber = 1, int pageSize = 10)
        {
            var result = new TicketListResponse();
            var query = new QueryExpression()
            {
                EntityName = Incident.EntityLogicalName,
                PageInfo = new PagingInfo
                {
                    ReturnTotalRecordCount = true,
                    PageNumber = pageNumber,
                    Count = pageSize
                },
                NoLock = true
            };

            query.AddOrder(Incident.Fields.CreatedOn, OrderType.Descending);

            var filter = new FilterExpression(LogicalOperator.And);

            query.Criteria.AddFilter(filter);

            filter.AddCondition(new ConditionExpression(Incident.Fields.CustomerId, ConditionOperator.Equal, customerId));
            query.ColumnSet = new ColumnSet(
                Incident.Fields.TicketNumber,
                Incident.Fields.ldv_serviceid,
                Incident.Fields.ldv_MainCategoryid,
                Incident.Fields.ldv_portalstatusid,
                Incident.Fields.StatusCode,
                Incident.Fields.CaseOriginCode
                );

            var portalStatusLink = query.AddLink(
                ldv_servicesubstatus.EntityLogicalName,
                Incident.Fields.ldv_portalstatusid,
                ldv_servicesubstatus.Fields.Id,
                JoinOperator.LeftOuter);
            portalStatusLink.EntityAlias = Globals.LinkEntityConsts.PortalStatusLink;

            portalStatusLink.Columns.AddColumns(ldv_servicesubstatus.Fields.ldv_name_ar, ldv_servicesubstatus.Fields.ldv_name_en);

            var caseTypeLink = query.AddLink(ldv_service.EntityLogicalName,Incident.Fields.ldv_serviceid,ldv_service.Fields.Id, JoinOperator.LeftOuter);
            caseTypeLink.EntityAlias = Globals.LinkEntityConsts.CaseTypeLink;

            caseTypeLink.Columns.AddColumns(ldv_service.Fields.ldv_name_en, ldv_service.Fields.ldv_name_ar);

            var mainCategoryLink = query.AddLink(ldv_casecategory.EntityLogicalName, Incident.Fields.ldv_MainCategoryid, ldv_casecategory.Fields.Id, JoinOperator.LeftOuter);
            mainCategoryLink.EntityAlias = Globals.LinkEntityConsts.MainCategoryLink;

            mainCategoryLink.Columns.AddColumn(ldv_casecategory.Fields.ldv_name) ;

            var ticketCollection = await _crmContext.ServiceClient.RetrieveMultipleAsync(query);
            result.TotalRecords = ticketCollection.TotalRecordCount;

            foreach (var entity in ticketCollection.Entities)
                result.Tickets.Add(MapTicketToDto(entity));

            return result;
        }
        public async Task<TicketDetailsResponse> GetTicketDetailsAsync(Guid customerId, string ticketNumber)
        {
            var result = new TicketDetailsResponse();

            var query = new QueryExpression(Incident.EntityLogicalName)
            {
                NoLock = true
            };

            query.ColumnSet.AddColumns(
                Incident.Fields.TicketNumber,
                Incident.Fields.ldv_serviceid,
                Incident.Fields.ldv_MainCategoryid,
                Incident.Fields.ldv_SubCategoryid,
                Incident.Fields.ldv_SecondarySubCategoryid,
                Incident.Fields.ldv_Description,
                Incident.Fields.ldv_portalstatusid,
                Incident.Fields.ldv_Locationcode,
                Incident.Fields.ldv_Seasoncode,
                Incident.Fields.ldv_ClosureDate,
                Incident.Fields.ldv_Beneficiarytypecode
                );
            var filter = new FilterExpression(LogicalOperator.And);
            query.Criteria.AddFilter(filter);

            filter.AddCondition(new ConditionExpression(Incident.Fields.TicketNumber, ConditionOperator.Equal, ticketNumber));
            filter.AddCondition(new ConditionExpression(Incident.Fields.CustomerId, ConditionOperator.Equal, customerId));

            var portalStatusLink = query.AddLink(
              ldv_servicesubstatus.EntityLogicalName,
              Incident.Fields.ldv_portalstatusid,
              ldv_servicesubstatus.Fields.Id,
              JoinOperator.LeftOuter);
            portalStatusLink.EntityAlias = Globals.LinkEntityConsts.PortalStatusLink;

            portalStatusLink.Columns.AddColumns(ldv_servicesubstatus.Fields.ldv_name_ar, ldv_servicesubstatus.Fields.ldv_name_en);

            var caseTypeLink = query.AddLink(ldv_service.EntityLogicalName, Incident.Fields.ldv_serviceid, ldv_service.Fields.Id, JoinOperator.LeftOuter);
            caseTypeLink.EntityAlias = Globals.LinkEntityConsts.CaseTypeLink;

            caseTypeLink.Columns.AddColumns(ldv_service.Fields.ldv_name_en, ldv_service.Fields.ldv_name_ar);

            var mainCategoryLink = query.AddLink(ldv_casecategory.EntityLogicalName, Incident.Fields.ldv_MainCategoryid, ldv_casecategory.Fields.Id, JoinOperator.LeftOuter);
            mainCategoryLink.EntityAlias = Globals.LinkEntityConsts.MainCategoryLink;

            mainCategoryLink.Columns.AddColumns(ldv_casecategory.Fields.ldv_englishname, ldv_casecategory.Fields.ldv_arabicname);

            var subCategoryLink = query.AddLink(ldv_casecategory.EntityLogicalName, Incident.Fields.ldv_SubCategoryid, ldv_casecategory.Fields.Id, JoinOperator.LeftOuter);
            subCategoryLink.EntityAlias = Globals.LinkEntityConsts.SubCategoryLink;

            subCategoryLink.Columns.AddColumns(ldv_casecategory.Fields.ldv_englishname, ldv_casecategory.Fields.ldv_arabicname);

            var secondarySubCategoryLink = query.AddLink(ldv_casecategory.EntityLogicalName, Incident.Fields.ldv_SecondarySubCategoryid, ldv_casecategory.Fields.Id, JoinOperator.LeftOuter);

            secondarySubCategoryLink.EntityAlias = Globals.LinkEntityConsts.SecondarySubCategoryLink;

            secondarySubCategoryLink.Columns.AddColumns(ldv_casecategory.Fields.ldv_englishname, ldv_casecategory.Fields.ldv_arabicname);

          
            var ticket = (await _crmContext.ServiceClient.RetrieveMultipleAsync(query)).Entities?.FirstOrDefault();

            if (ticket is null)
                throw new NotFoundException();

            result = await MapTicketToDetailsDto(ticket);
            return result;
        }
        private TicketDto MapTicketToDto(Entity entity)
        {
            var result = new TicketDto
            {
                Id = entity.Id,
                TicketNumber = entity.GetAttributeValue<string>(Incident.Fields.TicketNumber),
                TicketType = IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_ar}")?.Value.ToString() :
                            entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_en}")?.Value.ToString(),
                Status = IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_ar}")?.Value.ToString() :
                            entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_en}")?.Value.ToString(),
                Category = IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value.ToString() :
                            entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value.ToString(),
            };
            return result;
        }


        private async Task<TicketDetailsResponse?> MapTicketToDetailsDto(Entity entity)
        {
            if (entity is null)
                return null;

            var result = new TicketDetailsResponse
            {
                Id = entity.Id,
                TicketNumber = entity.GetAttributeValue<string>(Incident.Fields.TicketNumber),
                CreatedOn = entity.GetAttributeValue<DateTime>(Incident.Fields.CreatedOn),
                Resolution = entity.GetAttributeValue<string>(Incident.Fields.ldv_ClosureReason),
                ResolutionDate = entity.GetAttributeValue<DateTime>(Incident.Fields.ldv_ClosureDate),
                TicketType = IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_ar}")?.Value?.ToString() :
                           entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_en}")?.Value?.ToString(),
                Status = IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_ar}")?.Value?.ToString() :
                           entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_en}")?.Value?.ToString(),
                Category = IsArabic ? entity?.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value?.ToString() :
                           entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value?.ToString(),
                SubCategory = IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SubCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value?.ToString() :
                           entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SubCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value?.ToString(),
                SecondarySubCategory = IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SecondarySubCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value?.ToString() :
                           entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SecondarySubCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value?.ToString(),
            };
            if (entity.Contains(Incident.Fields.ldv_Locationcode))
                result.Location = await GetNameFromOptionSetAsync(entity, Incident.Fields.ldv_Locationcode, "ar");
            
            if (entity.Contains(Incident.Fields.ldv_Beneficiarytypecode))
                result.BeneficiaryType = await GetNameFromOptionSetAsync(entity, Incident.Fields.ldv_Beneficiarytypecode, "ar");

            return result;
        }
        private async Task<string> GetNameFromOptionSetAsync(Entity entity, string fieldLogicalName, string language)
        {
            var name = string.Empty;
            var options = await _commonRepository.GetOptionSet(entity.LogicalName, fieldLogicalName, language);
            name = options.FirstOrDefault(x => x.Value == entity.GetAttributeValue<OptionSetValue>(Incident.Fields.ldv_Locationcode)?.Value)?.Name;
            return name;
        }
    }
}
