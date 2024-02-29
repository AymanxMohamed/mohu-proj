using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.CreateProfile;
using MOHU.Integration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Xml.Linq;
using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using Microsoft.Xrm.Sdk.Messages;
using MOHU.Integration.Contracts.Interface.Cache;
using static Azure.Core.HttpHeader;
using System.Collections;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using Azure;
using MOHU.Integration.Infrastructure.Service;
using MOHU.Integration.Shared;

namespace MOHU.Integration.Application.Service
{

    /// <summary>
    ///  implementation here 
    /// </summary>
    public class CreateProfileService : ICreateProfileService
    {

        private readonly ICrmContext _crmContext;
        private readonly ICacheService<List<TicketType>> _cacheService;

        public CreateProfileService(ICrmContext crmContext, ICacheService<List<TicketType>> cacheService)
        {
            _crmContext = crmContext;
            _cacheService = cacheService;
        }

        public async Task<bool> CreateProfile(CreateProfileResponse model)
        {



            var isExist = await CheckPassportNumberExist(model.PassportNumber);


            var entity = new Entity(Individual.EntityLogicalName);

            if (isExist == false)
            {

                entity.Attributes.Add(Individual.Fields.FirstName, model.FirstName);
                entity.Attributes.Add(Individual.Fields.LastName, model.LastName);
                entity.Attributes.Add(Individual.Fields.Age, model.Age);
                entity.Attributes.Add(Individual.Fields.ArabicName, model.ArabicName);
                entity.Attributes.Add(Individual.Fields.Email, model.Email);
                entity.Attributes.Add(Individual.Fields.MobileNumber, model.MobileNumber);
                entity.Attributes.Add(Individual.Fields.BirthDate, model.DateOfBirth);
                entity.Attributes.Add(Individual.Fields.HijriBirthDate, model.HijriDateofBirth);
                entity.Attributes.Add(Individual.Fields.MobileCountryCode, model.MobileCountryCode);
                entity.Attributes.Add(Individual.Fields.Gender,
                new OptionSetValue(Convert.ToInt32(model.Gender)));
                entity.Attributes.Add(Individual.Fields.Nationality,
                new EntityReference(Individual.EntityLogicalName, model.Nationality));
                entity.Attributes.Add(Individual.Fields.CountryofResidence,
               new EntityReference(Individual.EntityLogicalName, model.CountryOfResidence));

                entity.Attributes.Add(Individual.Fields.IDType,
                    new OptionSetValue(Convert.ToInt32(model.IdType)));
                if (model.IdType == IdTypeEnum.NationalIdentity)
                {
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdTypeEnum.Accommodation)
                {
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                }
                else if (model.IdType == IdTypeEnum.Gulfcitizen)
                {
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }
                else if (model.IdType == IdTypeEnum.Passport)
                {
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }


                _crmContext.ServiceClient.Create(entity);
                return true;

            }
            return false;

        }
        public async Task<bool> CheckPassportNumberExist(string number)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.PassportNumber, ConditionOperator.Equal, number));
            queryContact.Criteria.AddFilter(filter);
            var response = _crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// get ticket types
        /// </summary>
        /// <returns></returns>
        public async Task<List<TicketType>> GetTicketTypes()
        {
            var ticketTypes = new List<TicketType>();

            ticketTypes.AddRange(await GetTypes());

           GetTypesCategories(ticketTypes);

         GetTypeCategorySubCategories(ticketTypes);

            return await Task.FromResult(ticketTypes);
        }
        bool IsArabic => true;
        private async Task<List<TicketType>> GetTypes()
        {

            var result = new List<TicketType>();


            result = new List<TicketType>();

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
          //  filter.AddCondition(new ConditionExpression(RequestType.Fields.ShowonPortal, ConditionOperator.Equal, false));

            //var LinkMainCategory = ticketTypeQuery.AddLink(
            //    ldv_casecategory.EntityLogicalName,
            //    RequestType.Fields.PrimaryKey, /// ldv_subcategoryid
            //    ldv_casecategory.Fields.TicketType,
            //    JoinOperator.LeftOuter);
            //LinkMainCategory.EntityAlias = "ticketTypeQuery";
            //LinkMainCategory.Columns.AddColumns(
            //    ldv_casecategory.Fields.
            //    ldv_englishname, ldv_casecategory.Fields.ldv_arabicname,
            //    ldv_casecategory.Fields.TicketType
            //    );

            //var filter_MainCategory = new FilterExpression(LogicalOperator.And);
            //LinkMainCategory.LinkCriteria.AddFilter(filter_MainCategory);
            //filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.ParentCategory, ConditionOperator.Equal, null));
            //filter.AddCondition(new ConditionExpression(ldv_casecategory.Fields.SubCategory, ConditionOperator.Equal, null));


            //ticketTypeQuery.NoLock = true;

            var response = _crmContext.ServiceClient.RetrieveMultiple(ticketTypeQuery).Entities.ToList();


            //var Tickettype = entity.
            //    Attributes["ldv_complaintrequest1.ldv_complaintrequestid"].GetType().GetProperty("Value")
            //    .GetValue(entity.Attributes["ldv_complaintrequest1.ldv_complaintrequestid"], null);


            //  var GettypesGrouping = response.GroupBy(c => c.Attributes[ldv_casecategory.Fields.TicketType]);

           /// var GettypesGrouping = response.GroupBy(c => c.Attributes["ticketTypeQuery.ldv_tickettypeid"]);

            ///ticketTypeQuery.ldv_tickettypeid => from request 

            if (response != null && response.Any())
            {

                result.AddRange(response.Select(t => new TicketType
                {
                    Id = t.Id,
                    Name = IsArabic ? t.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname)
                    : t.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname),

                }));

            }


            return await Task.FromResult(result);
        }

        ///get category 
        private void GetTypesCategories(List<TicketType> ticketTypes)
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
                 t.Id));  /// category its parent is ticket type 

                var MainCategoryLink = categoryQuery.AddLink(
                 ldv_casecategory.EntityLogicalName,
                 ldv_casecategory.Fields.TicketType, /// ldv_subcategoryid
                 ldv_casecategory.Fields.Id,
                 JoinOperator.LeftOuter);
                MainCategoryLink.EntityAlias = "MainCategoryLink";
                MainCategoryLink.Columns.AddColumns(ldv_casecategory.Fields.
                    ldv_englishname, ldv_casecategory.Fields.ldv_arabicname);
                executeMultipleRequest.Requests.AddRange(new RetrieveMultipleRequest { Query = categoryQuery });



            });

            if (!executeMultipleRequest.Requests.Any())
                return;
            var categoryResponse = (ExecuteMultipleResponse)_crmContext.ServiceClient
                .Execute(executeMultipleRequest);

            if (categoryResponse.IsFaulted) //IsFaulted exist inside ExecuteMultipleResponse class 
                return;

            for (var i = 0; i < categoryResponse.Responses.Count; i++)
            {
                var responseItem =
                    (EntityCollection)categoryResponse.Responses[i]?.Response.Results.Values.FirstOrDefault();
                if (responseItem.Entities != null)
                {
                    var categories = responseItem.Entities.Select(c => new Contracts.Dto.CaseTypes.TicketCategory
                    {

                        Id = c.Id,
                        Name = IsArabic ?
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

       
        private void GetTypeCategorySubCategories(List<TicketType> ticketTypes)
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
                             ConditionOperator.NotEqual, null));

                      



                       var SubCategoryLink = subCategoryQuery.
                            AddLink(
                            ldv_casecategory.EntityLogicalName,
                            ldv_casecategory.Fields.ParentCategory,
                            ldv_casecategory.Fields.Id,
                            JoinOperator.LeftOuter);

                        SubCategoryLink.EntityAlias = "SubCategoryLink";
                        SubCategoryLink.Columns.AddColumns(ldv_casecategory.Fields.ldv_englishname, ldv_casecategory.Fields.ldv_arabicname);




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
                    var subCategories = group.Select(e => new TicketSubCategory
                    {
                        Id = e.Id,
                        Name = IsArabic ?
                            e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname) ://ldv_englishname
                            e.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_arabicname), // ldv_arabicname
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
        private void GetAllSecondarySubTypesBySubCategory(List<TicketSubCategory> ticketSubCategories)
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
               
                var SecondarySubLink = 
                    query.AddLink(
                    ldv_casecategory.EntityLogicalName,
                    ldv_casecategory.Fields.SubCategory, 
                    ldv_casecategory.Fields.Id,
                    JoinOperator.LeftOuter);

                SecondarySubLink.EntityAlias = "SecondarySubLink";
                SecondarySubLink.Columns.AddColumns
                    (ldv_casecategory.Fields.ldv_englishname
                    , ldv_casecategory.Fields.ldv_arabicname);


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
                        var secondarysubcategory = new SecondarySubCategory
                        {
                            Id = item.Id,
                            Name = IsArabic ?
                            item.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname) :
                            item.GetAttributeValue<string>(ldv_casecategory.Fields.ldv_englishname),



                        };

                        subCategory.secondarySubCategories.Add(secondarysubcategory);


                    }
                }

            }

        }




        








    }








}




