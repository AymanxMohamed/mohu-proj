using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.CaseTypes;
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

            mainCategoryLink.Columns.AddColumn(ldv_casecategory.Fields.ldv_name);

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

        public Task<SubmitTicketResponse> SubmitTicketAsync(Guid customerId, SubmitTicketRequest request)
        {
            throw new NotImplementedException();
        }


        public async Task<List<TicketTypeResponse>> GetTicketTypes()
        {
            var ticketTypes = new List<TicketTypeResponse>();

            ticketTypes.AddRange(await GetTypes());

            GetTypesCategories(ticketTypes);

            GetTypeCategorySubCategories(ticketTypes);

            return await Task.FromResult(ticketTypes);
        }
        bool _IsArabic => true;
        private async Task<List<TicketTypeResponse>> GetTypes()
        {

            var result = new List<TicketTypeResponse>();


            result = new List<TicketTypeResponse>();

            var ticketTypeQuery = new QueryExpression(RequestType.EntityLogicalName);

            ticketTypeQuery.ColumnSet = new ColumnSet(
                        RequestType.Fields.PrimaryKey,
                        RequestType.Fields.ServiceArabicName,
                        RequestType.Fields.ServiceEnglishName,
                        RequestType.Fields.PrimaryName

                );
            var filter = new FilterExpression(LogicalOperator.And);
            ticketTypeQuery.Criteria.AddFilter(filter);
            filter.AddCondition(new ConditionExpression(RequestType.Fields.ShowonPortal, ConditionOperator.Equal, true));
            var response = _crmContext.ServiceClient.RetrieveMultiple(ticketTypeQuery).Entities.ToList();
            if (response != null && response.Any())
            {

                result.AddRange(response.Select(t => new TicketTypeResponse
                {
                    Id = t.Id,
                    Name = _IsArabic ? t.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname)
                    : t.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname),

                }));

            }


            return await Task.FromResult(result);
        }

        ///get category 
        private void GetTypesCategories(List<TicketTypeResponse> ticketTypes)
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
            ticketTypes.ForEach(t =>
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

                executeMultipleRequest.Requests.AddRange(new RetrieveMultipleRequest { Query = categoryQuery });

            });

            if (!executeMultipleRequest.Requests.Any())
                return;
            var categoryResponse = (ExecuteMultipleResponse)_crmContext.ServiceClient
                .Execute(executeMultipleRequest);

            if (categoryResponse.IsFaulted)
                return;

            for (var i = 0; i < categoryResponse.Responses.Count; i++)
            {
                var responseItem =
                    (EntityCollection)categoryResponse.Responses[i]?.Response.Results.Values.FirstOrDefault();
                if (responseItem.Entities != null)
                {
                    var categories = responseItem.Entities.Select(c => new Contracts.Dto.CaseTypes.TicketCategoryDto
                    {

                        Id = c.Id,
                        Name = _IsArabic ?
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


        }


        private void GetTypeCategorySubCategories(List<TicketTypeResponse> ticketTypes)
        {

            try
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

                foreach (var ticketType in ticketTypes)
                {
                    foreach (var ticketCategory in ticketType.Categories)
                    {

                        var subCategoryQuery = new QueryExpression(ldv_casecategory.EntityLogicalName)
                        {
                            NoLock = true,
                            ColumnSet = new ColumnSet(
                                ldv_casecategory.Fields.TicketType,
                                ldv_casecategory.Fields.ldv_arabicname,
                                ldv_casecategory.Fields.ldv_englishname,
                               ldv_casecategory.Fields.ParentCategory

                            ),
                        };
                        var filter = new FilterExpression(LogicalOperator.And);
                        subCategoryQuery.Criteria.AddFilter(filter);
                        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.TicketType,
                            ConditionOperator.Equal, ticketType.Id));
                        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.ShowOnPortal,
                              ConditionOperator.Equal, true));

                        filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.ParentCategory,
                             ConditionOperator.Equal, ticketCategory.Id));

                        executeMultipleRequest.Requests.AddRange(new RetrieveMultipleRequest { Query = subCategoryQuery });


                        //  }


                    }
                }

                if (!executeMultipleRequest.Requests.Any())
                    return;

                var categoryResponse = (ExecuteMultipleResponse)_crmContext.ServiceClient
              .Execute(executeMultipleRequest);

                if (categoryResponse.IsFaulted)
                    return;

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
                    var subCategories = group.Select(e => new TicketSubCategoryDto
                    {
                        Id = e.Id,
                        Name = _IsArabic ?
                            e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname) :
                            e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname),
                        //  ParentCategoryId = e.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.Id).Id,
                        ParentCategoryId = e.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.ParentCategory).Id,

                    }).ToList();

                    (ticketTypes.FirstOrDefault(c => c.Id == subCategories?.FirstOrDefault().TicketTypeId)?.Categories)
                        .FirstOrDefault(c => c.Id == subCategories.FirstOrDefault()?.ParentCategoryId)
                       ?.SubCategories.AddRange(subCategories);

                    GetAllSecondarySubTypesBySubCategory(subCategories);
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred isssss : " + ex.Message);
            }


        }

        ///secondary sub 
        private void GetAllSecondarySubTypesBySubCategory(List<TicketSubCategoryDto> ticketSubCategories)
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
            foreach (var subCategory in ticketSubCategories)
            {
                var query = new QueryExpression(ldv_casecategory.EntityLogicalName)
                {
                    NoLock = true,
                    ColumnSet = new ColumnSet(
                           ldv_casecategory.Fields.TicketType,
                            ldv_casecategory.Fields.ldv_arabicname,
                             ldv_casecategory.Fields.ldv_englishname


                        ),
                };
                query.Criteria.AddCondition(new ConditionExpression(ldv_casecategory.Fields.SubCategory, ConditionOperator.Equal, subCategory.Id));
                executeMultipleRequest.Requests.AddRange(new RetrieveMultipleRequest
                {
                    Query = query
                });

            }

            if (!executeMultipleRequest.Requests.Any())
                return;

            var SecondarySubResponse = (ExecuteMultipleResponse)_crmContext.ServiceClient.Execute(executeMultipleRequest);
            var entitiesList = new List<Entity>();
            foreach (var t in SecondarySubResponse.Responses)
            {
                if (t.Fault == null)
                {
                    var responseItem = (EntityCollection)t?.Response.Results.Values.FirstOrDefault();
                    entitiesList.AddRange(responseItem.Entities);
                }
                else
                {
                    throw new NotFoundException();
                }
            }
            foreach (var item in entitiesList)
            {
                if (item.Attributes.Contains(ldv_casecategory.Fields.SubCategory))
                {
                    var subCategory = ticketSubCategories.
                   Where(e => e.Id == item.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.SubCategory).Id).FirstOrDefault();


                    if (subCategory != null)
                    {
                        var secondarysubcategory = new SecondarySubCategoryDto
                        {
                            Id = item.Id,
                            Name = IsArabic ?
                            item.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname) :
                            item.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname),

                        };

                        subCategory.secondarySubCategories.Add(secondarysubcategory);

                    }
                }

            }

        }












































    }
}
