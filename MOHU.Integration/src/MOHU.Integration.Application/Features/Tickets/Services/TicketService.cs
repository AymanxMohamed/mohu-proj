using Microsoft.Xrm.Sdk.Messages;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Interface.Cache;
using MOHU.Integration.Contracts.Logging;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService(
    ICrmContext crmContext,
    IAppLogger logger,
    ICommonService commonService,
    IValidator<SubmitTicketRequest> validator,
    ICacheService cacheService,
    IDocumentService documentService,
    IStringLocalizer stringLocalizer,
    IRequestInfo requestInfo,
    IValidator<CreateHootSuiteTicketRequest> createHootSuiteTicketValidator)
    : ITicketService
{
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
            Incident.Fields.Title,
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

        var caseTypeLink = query.AddLink(ldv_service.EntityLogicalName, Incident.Fields.ldv_serviceid, ldv_service.Fields.Id, JoinOperator.LeftOuter);
        caseTypeLink.EntityAlias = Globals.LinkEntityConsts.CaseTypeLink;

        caseTypeLink.Columns.AddColumns(ldv_service.Fields.ldv_name_en, ldv_service.Fields.ldv_name_ar);

        var mainCategoryLink = query.AddLink(ldv_casecategory.EntityLogicalName, Incident.Fields.ldv_MainCategoryid, ldv_casecategory.Fields.Id, JoinOperator.LeftOuter);
        mainCategoryLink.EntityAlias = Globals.LinkEntityConsts.MainCategoryLink;

        mainCategoryLink.Columns.AddColumns(ldv_casecategory.Fields.ldv_arabicname, ldv_casecategory.Fields.ldv_englishname);

        var ticketCollection = await crmContext.ServiceClient.RetrieveMultipleAsync(query);
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
            Incident.Fields.Title,
            Incident.Fields.ldv_serviceid,
            Incident.Fields.ldv_MainCategoryid,
            Incident.Fields.ldv_SubCategoryid,
            Incident.Fields.ldv_SecondarySubCategoryid,
            Incident.Fields.ldv_Description,
            Incident.Fields.ldv_portalstatusid,
            Incident.Fields.ldv_Locationcode,
            Incident.Fields.ldv_Seasoncode,
            Incident.Fields.ldv_ClosureDate,
            Incident.Fields.ldv_Beneficiarytypecode,
            Incident.Fields.CreatedOn
        );
        
        var filter = new FilterExpression(LogicalOperator.And);
        query.Criteria.AddFilter(filter);

        filter.AddCondition(new ConditionExpression(Incident.Fields.Title, ConditionOperator.Equal, ticketNumber));
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


        var ticket = (await crmContext.ServiceClient.RetrieveMultipleAsync(query)).Entities?.FirstOrDefault();

        if (ticket is null)
            throw new NotFoundException("Ticket does not exist");

        result = await MapTicketToDetailsDto(ticket);

        var documents = await documentService.GetFilesInFolderAsync(result?.Id.ToString());

        foreach (var file in documents.Files)
            result.Documents.Add(new Contracts.Dto.Document.DocumentDto { Id = file.Id, Name = file.Name });

        return result;
    }

    public async Task<List<TicketTypeResponse>> GetTicketTypesAsync()
    {
        var ticketTypes = new List<TicketTypeResponse>();

        ticketTypes.AddRange(await GetTypesAsync());

        await GetTypesCategoriesAsync(ticketTypes);

        await GetCategorySubCategoriesAsync(ticketTypes);

        return ticketTypes;
    }
        
    public async Task<bool> UpdateStatusAsync(UpdateTicketStatusRequest request)
    {
        if (request.TicketId == Guid.Empty)
            throw new NotFoundException(stringLocalizer[ErrorMessageCodes.TicketIdisRequired]);
            
        await crmContext.ServiceClient.UpdateAsync(request.ToTicketEntity());
            
        return true;
    }

    public async Task<Guid> GetCategoryRequestType(Guid categoryId)
    {

        var categoryQuery = new QueryExpression(ldv_casecategory.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(

                ldv_casecategory.Fields.TicketType,
                ldv_casecategory.Fields.ldv_arabicname,
                ldv_casecategory.Fields.ldv_englishname
            ),
        };
        var filter = new FilterExpression(LogicalOperator.And);
        categoryQuery.Criteria.AddFilter(filter);
        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.Id,
            ConditionOperator.Equal,
            categoryId));

        var result = await crmContext.ServiceClient.RetrieveMultipleAsync(categoryQuery);
        var category = result.Entities[0];
        if (category.Contains(ldv_casecategory.Fields.TicketType) && category[ldv_casecategory.Fields.TicketType] is EntityReference parentServiceRef)
        {
            Guid parentServiceId = parentServiceRef.Id; 
            string parentServiceEntity = parentServiceRef.LogicalName;
            return parentServiceId;

        }
        return Guid.Empty;
    }
    public async Task<Guid> GetParentCategory(Guid categoryId)
    {
        var categoryQuery = new QueryExpression(ldv_casecategory.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(

               ldv_casecategory.Fields.ldv_casecategoryId,
               ldv_casecategory.Fields.ldv_arabicname,
               ldv_casecategory.Fields.ldv_englishname
           ),
        };
        var filter = new FilterExpression(LogicalOperator.And);
        categoryQuery.Criteria.AddFilter(filter);
        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.Id,
            ConditionOperator.Equal,
            categoryId));
        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.StateCode,
          ConditionOperator.Equal,
          0));

        var result = await crmContext.ServiceClient.RetrieveMultipleAsync(categoryQuery);
        var category = result.Entities[0];
        if (category.Contains(ldv_casecategory.Fields.ldv_casecategoryId) && category[ldv_casecategory.Fields.ldv_casecategoryId] is Guid parentCategoryGuid)
        {
            return parentCategoryGuid;

        }
        return Guid.Empty;
    }
    private async Task<List<TicketTypeResponse>> GetTypesAsync()
    {
        var cacheKey = "CaseTypes";
        var caseTypeEntities = await cacheService.GetAsync<List<Entity>>(cacheKey);
        var result = new List<TicketTypeResponse>();
        if (caseTypeEntities is null)
        {
            var ticketTypeQuery = new QueryExpression(RequestType.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(
                    RequestType.Fields.PrimaryKey,
                    RequestType.Fields.ServiceArabicName,
                    RequestType.Fields.ServiceEnglishName
                )
            };
            var filter = new FilterExpression(LogicalOperator.And);
            ticketTypeQuery.Criteria.AddFilter(filter);
            filter.AddCondition(new ConditionExpression(RequestType.Fields.ShowonPortal, ConditionOperator.Equal, true));
            filter.AddCondition(new ConditionExpression(RequestType.Fields.Status, ConditionOperator.Equal, 0));
            var response = (await crmContext.ServiceClient.RetrieveMultipleAsync(ticketTypeQuery)).Entities.ToList();
            if (response != null && response.Count != 0)
            {

                result.AddRange(response.Select(t => new TicketTypeResponse
                {
                    Id = t.Id,
                    Name = LanguageHelper.IsArabic ? t.GetAttributeValue<string>(ldv_service.Fields.ldv_name_ar)
                        : t.GetAttributeValue<string>(ldv_service.Fields.ldv_name_en),

                }));

            }
            await cacheService.SetAsync(cacheKey, response);
            caseTypeEntities = response;

        }
        else
        {
            result.AddRange(caseTypeEntities.Select(t => new TicketTypeResponse
            {
                Id = t.Id,
                Name = LanguageHelper.IsArabic ? t.GetAttributeValue<string>(ldv_service.Fields.ldv_name_ar)
                    : t.GetAttributeValue<string>(ldv_service.Fields.ldv_name_en),

            }));
        }

        return result;
    }


    private async Task GetTypesCategoriesAsync(List<TicketTypeResponse> ticketTypes)
    {
        var executeMultipleRequest = new ExecuteMultipleRequest
        {
            Requests = new OrganizationRequestCollection(),
            Settings = new ExecuteMultipleSettings
            {
                ContinueOnError = false,
                ReturnResponses = true
            }
        };
        var languageKey = LanguageHelper.IsArabic ? "ar" : "en";

        ticketTypes.ForEach(async t =>
        {
            var cacheKey = $"CaseType-{t.Id}-Categories_{languageKey}_Origin-{requestInfo.Origin}";
            var resultFromCache = await cacheService.GetAsync<List<TicketCategoryDto>>(cacheKey);
            if (resultFromCache is not null)
                t.Categories.AddRange(resultFromCache);
            else
            {
                var categoryQuery = new QueryExpression(ldv_casecategory.EntityLogicalName)
                {
                    NoLock = true,
                    ColumnSet = new ColumnSet(

                        ldv_casecategory.Fields.TicketType,
                        ldv_casecategory.Fields.ldv_arabicname,
                        ldv_casecategory.Fields.ldv_englishname
                    ),
                };
                var filter = new FilterExpression(LogicalOperator.And);
                categoryQuery.Criteria.AddFilter(filter);
                filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.TicketType,
                    ConditionOperator.Equal,
                    t.Id));
                filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.SubCategory, ConditionOperator.Null));
                filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.ParentCategory, ConditionOperator.Null));
                filter.AddCondition(new ConditionExpression("ldv_availableforcode", ConditionOperator.ContainValues, requestInfo.Origin));


                executeMultipleRequest.Requests.AddRange(new RetrieveMultipleRequest { Query = categoryQuery });
            }


        });

        if (!executeMultipleRequest.Requests.Any())
            return;
        var categoryResponse = (ExecuteMultipleResponse)await crmContext.ServiceClient
            .ExecuteAsync(executeMultipleRequest);

        if (categoryResponse.IsFaulted)
            return;

        for (var i = 0; i < categoryResponse.Responses.Count; i++)
        {
            var responseItem =
                (EntityCollection)categoryResponse.Responses[i]?.Response.Results.Values.FirstOrDefault();
            if (responseItem.Entities != null)
            {
                var categories = responseItem.Entities.Select(c => new TicketCategoryDto
                {

                    Id = c.Id,
                    Name = LanguageHelper.IsArabic ?
                        c.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname) :
                        c.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname)
                });
                var ticketTypeId = responseItem.Entities.FirstOrDefault()
                    ?.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.TicketType).Id;
                if (ticketTypeId != null)
                {
                    ticketTypes.FirstOrDefault(t => t.Id == ticketTypeId).Categories = categories.ToList();
                }
            }
        }
        foreach (var ticketType in ticketTypes)
        {
            await cacheService.SetAsync($"CaseType-{ticketType.Id}-Categories_{languageKey}", ticketType.Categories);
        }
    }
    private async Task GetCategorySubCategoriesAsync(List<TicketTypeResponse> ticketTypes)
    {
        try
        {
            var languageKey = LanguageHelper.IsArabic ? "ar" : "en";

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                }
            };

            foreach (var ticketType in ticketTypes)
            {
                foreach (var ticketCategory in ticketType.Categories)
                {
                    var resultFromCache = await cacheService.GetAsync<List<TicketSubCategoryDto>>($"CaseCategory-{ticketCategory.Id}-SubCategories_{languageKey}");

                    if (resultFromCache is not null)
                        ticketCategory.SubCategories = resultFromCache;
                    else
                    {
                        var subCategoryQuery = new QueryExpression(ldv_casecategory.EntityLogicalName)
                        {
                            NoLock = true,
                            ColumnSet = new ColumnSet(ldv_casecategory.Fields.TicketType,
                                ldv_casecategory.Fields.ldv_arabicname,
                                ldv_casecategory.Fields.ldv_englishname,
                                ldv_casecategory.Fields.SubCategory,
                                ldv_casecategory.Fields.ParentCategory),
                        };
                        var filter = new FilterExpression(LogicalOperator.And);
                        subCategoryQuery.Criteria.AddFilter(filter);
                        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.TicketType,
                            ConditionOperator.Equal, ticketType.Id));

                        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.ParentCategory,
                            ConditionOperator.Equal, ticketCategory.Id));


                        executeMultipleRequest.Requests.AddRange(new RetrieveMultipleRequest { Query = subCategoryQuery });
                    }
                }
            }

            if (!executeMultipleRequest.Requests.Any())
                return;

            var categoryResponse = (ExecuteMultipleResponse)await crmContext.ServiceClient
                .ExecuteAsync(executeMultipleRequest);

            if (categoryResponse.IsFaulted)
                throw new Exception(categoryResponse.Responses?.FirstOrDefault()?.Fault?.Message);

            var entitiesList = new List<Entity>();

            foreach (var t in categoryResponse.Responses)
            {
                var responseItem =
                    (EntityCollection)t?.Response.Results.Values.FirstOrDefault();
                entitiesList.AddRange(responseItem.Entities);

            }
            if (entitiesList.Count == 0)
                return;
            var subCategoriesGroupedByCategory = entitiesList.
                GroupBy(c => c.Attributes[ldv_casecategory.Fields.ParentCategory]);

            foreach (var group in subCategoriesGroupedByCategory)
            {
                var subCategories = group.Where(e => !e.Attributes.Contains(ldv_casecategory.Fields.SubCategory)).Select(e => new TicketSubCategoryDto
                {
                    Id = e.Id,
                    Name = LanguageHelper.IsArabic ?
                        e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname) :
                        e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname),
                    TicketTypeId = e.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.TicketType).Id,
                    ParentCategoryId = e.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.ParentCategory).Id,


                }).ToList();
                foreach (var subCategory in subCategories)
                {
                    subCategory.SecondarySubCategories = group.Where(e => e.Attributes.Contains(ldv_casecategory.Fields.SubCategory) && e.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.SubCategory).Id == subCategory.Id)
                        .Select(e => new SecondarySubCategoryDto
                        {
                            Id = e.Id,
                            Name = LanguageHelper.IsArabic ?
                                e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname) :
                                e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname),
                        }).ToList();
                }

                (ticketTypes.FirstOrDefault(c => c.Id == subCategories?.FirstOrDefault().TicketTypeId)?.Categories)
                    .FirstOrDefault(c => c.Id == subCategories.FirstOrDefault()?.ParentCategoryId)
                    ?.SubCategories.AddRange(subCategories);

                await cacheService.SetAsync($"{subCategories.FirstOrDefault().ParentCategoryId}-SubCategories_{languageKey}", subCategories);

            }



        }
        catch (Exception ex)
        {
            await logger.LogError(ex);
        }

    }
    private TicketDto MapTicketToDto(Entity entity)
    {
        var result = new TicketDto
        {
            Id = entity.Id,
            TicketNumber = entity.GetAttributeValue<string>(Incident.Fields.Title),
            TicketType = LanguageHelper.IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_ar}")?.Value.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_en}")?.Value.ToString(),
            Status = LanguageHelper.IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_ar}")?.Value.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_en}")?.Value.ToString(),
            Category = LanguageHelper.IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value.ToString(),
            CreationOn = entity.GetAttributeValue<DateTime>(Incident.Fields.CreatedOn)
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
            Description = entity.GetAttributeValue<string>(Incident.Fields.ldv_Description),
            TicketNumber = entity.GetAttributeValue<string>(Incident.Fields.Title),
            CreatedOn = entity.GetAttributeValue<DateTime>(Incident.Fields.CreatedOn),
            Resolution = entity.GetAttributeValue<string>(Incident.Fields.ldv_ClosureReason),
            ResolutionDate = entity.GetAttributeValue<DateTime?>(Incident.Fields.ldv_ClosureDate),
            TicketType = LanguageHelper.IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_ar}")?.Value?.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.CaseTypeLink}.{ldv_service.Fields.ldv_name_en}")?.Value?.ToString(),
            Status = LanguageHelper.IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_ar}")?.Value?.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.PortalStatusLink}.{ldv_servicesubstatus.Fields.ldv_name_en}")?.Value?.ToString(),
            Category = LanguageHelper.IsArabic ? entity?.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value?.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.MainCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value?.ToString(),
            SubCategory = LanguageHelper.IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SubCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value?.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SubCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value?.ToString(),
            SecondarySubCategory = LanguageHelper.IsArabic ? entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SecondarySubCategoryLink}.{ldv_casecategory.Fields.ldv_arabicname}")?.Value?.ToString() :
                entity.GetAttributeValue<AliasedValue>($"{Globals.LinkEntityConsts.SecondarySubCategoryLink}.{ldv_casecategory.Fields.ldv_englishname}")?.Value?.ToString(),
        };
        if (entity.Contains(Incident.Fields.ldv_Locationcode))
            result.Location = await GetNameFromOptionSetAsync(entity, Incident.Fields.ldv_Locationcode, LanguageHelper.IsArabic ? "ar" : "en");

        if (entity.Contains(Incident.Fields.ldv_Beneficiarytypecode))
            result.BeneficiaryType = await GetNameFromOptionSetAsync(entity, Incident.Fields.ldv_Beneficiarytypecode, LanguageHelper.IsArabic ? "ar" : "en");
        if (entity.Contains(Incident.Fields.ldv_ClosureDate))
            result.ResolutionDate = entity.GetAttributeValue<DateTime>(Incident.Fields.ldv_ClosureDate);
        return result;
    }
    private async Task<string> GetNameFromOptionSetAsync(Entity entity, string fieldLogicalName, string language)
    {
        var name = string.Empty;
        var options = await commonService.GetOptionSet(entity.LogicalName, fieldLogicalName, language);
        name = options.FirstOrDefault(x => x.Value == entity.GetAttributeValue<OptionSetValue>(fieldLogicalName)?.Value)?.Name;
        return name;
    }
    private async Task<Guid> GetTicketTypeProcessAsync(Guid ticketTypeId)
    {
        var ticketTypeEntity = await crmContext.ServiceClient.RetrieveAsync(ldv_service.EntityLogicalName, ticketTypeId, new ColumnSet(ldv_service.Fields.ldv_processid));
        var processId = ticketTypeEntity.GetAttributeValue<EntityReference>(ldv_service.Fields.ldv_processid).Id;
        return processId;
    }
    private async Task<bool> IsTicketExists(Guid ticketId)
    {
        var ticketQuery = new QueryExpression
        {
            EntityName = Incident.EntityLogicalName,
            NoLock = true
        };
        var filter = new FilterExpression(LogicalOperator.And);
        filter.AddCondition(new ConditionExpression(Incident.Fields.Id, ConditionOperator.Equal, ticketId));
        ticketQuery.Criteria.AddFilter(filter);
        var result = await crmContext.ServiceClient.RetrieveMultipleAsync(ticketQuery);
        return result.Entities.Any();
    }

    public async Task<Guid> GetTicketByIntegrationTicketNumberAsync(string integrationTicketNumber, string ticketNumberSchemaName)
    {
        var query = new QueryExpression(Incident.EntityLogicalName)
        {
            NoLock = true,
            TopCount = 1
        };

        var filter = new FilterExpression(LogicalOperator.And);
        query.Criteria.AddFilter(filter);
        filter.AddCondition(new ConditionExpression(ticketNumberSchemaName, ConditionOperator.Equal, integrationTicketNumber.ToString()));
        var entities = (await crmContext.ServiceClient.RetrieveMultipleAsync(query))?.Entities;


        return entities.Count == 0
            ? throw new NotFoundException($"Ticket with #{integrationTicketNumber} was not found")
            : entities.FirstOrDefault().Id;
    }
}