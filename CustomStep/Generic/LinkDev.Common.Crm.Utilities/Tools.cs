//using LinkDev.Common.Crm.Logger;
using LinkDev.Common.Crm.Logger;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

namespace LinkDev.Common.Crm.Utilities
{
    public class Tools
    {
        #region Variables
        #endregion

        #region Constructors
        #endregion

        #region Public

        public DateTime CancelOnXdays(int days, DateTime date, IOrganizationService organizationService)
        {
            if (days < 0)
            {
                throw new System.Exception("number of days should be greater than zero");
            }

            DateTime result = date;

            var vacations = GetVacations(organizationService).Entities.ToList();
            //string magicvacations = String.Empty;
            //foreach (var vac in vacations)
            //{
            //    magicvacations += ((DateTime) vac.Attributes["new_vacationstartdate"]).Date.ToString();
            //}
            ////throw new InvalidPluginExecutionException(magicvacations);
            var OrderedVacations = vacations.OrderBy(x => ((DateTime)x.Attributes["new_vacationstartdate"]));

            int count = days;
            bool isConflictWithVacation = false;
            while (count > 0)
            {
                isConflictWithVacation = false;
                result = result.AddDays(1);
                if (result.DayOfWeek == DayOfWeek.Friday)
                {
                    result = result.AddDays(2);
                }

                else if (result.DayOfWeek == DayOfWeek.Saturday)
                {
                    result = result.AddDays(1);
                }
                foreach (var vacation in OrderedVacations)
                {
                    if (result.Date == ((DateTime)vacation.Attributes["new_vacationstartdate"]).ToLocalTime().Date)
                    {
                        isConflictWithVacation = true;
                        break;
                    }
                }
                if (!isConflictWithVacation)
                    count--;
            }
            return result;
        }

        public EntityCollection GetVacations(IOrganizationService organizationService)
        {
            //Get vacationDays to be put in consideration for calaculating cancel days
            var qEnewVacationdays = new QueryExpression
            {
                EntityName = "new_vacationdays"
            };
            qEnewVacationdays.ColumnSet.AddColumns("new_name", "createdon", "new_vacationstartdate",
                "new_vacationenddate", "new_vacationdaysid");
            qEnewVacationdays.AddOrder("new_vacationstartdate", OrderType.Ascending);

            EntityCollection vacations = organizationService.RetrieveMultiple(qEnewVacationdays);
            return vacations;
        }

        /// <summary>
        /// Retrieve a CRM Entity's primarykey and primaryfield
        /// </summary>
        /// <param name="myMetadataService"></param>
        /// <param name="entityName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="primaryField"></param>
        public static void GetEntityInfo(string entityName, out string primaryKey, out string primaryField, IOrganizationService organizationService)
        {
            var retrieveEntityMetadata = new Microsoft.Xrm.Sdk.Messages.RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = entityName
            };
            primaryKey = "";
            primaryField = "";
            if (entityName != "")
            {
                var selectedEntity =
                    organizationService.Execute(retrieveEntityMetadata);
                if (selectedEntity.Results.Count > 0)
                {
                    EntityMetadata entityMetadata =
                        selectedEntity.Results["EntityMetadata"] as EntityMetadata;
                    primaryKey = entityMetadata.PrimaryIdAttribute;
                    primaryField = entityMetadata.PrimaryNameAttribute;
                }
            }
        }


        /// <summary>
        /// Retrieve lookup field name of 1 to many relationship
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityName"></param>
        /// <param name="relationName"></param>
        public static string ConvertGregDateToHijriDate(string greg)
        {
            string[] allFormats ={"yyyy/MM/dd","yyyy/M/d",
            "dd/MM/yyyy","d/M/yyyy",
            "dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd",
            "yyyy-M-d","dd-MM-yyyy","d-M-yyyy","dd-mm-yyyy",
            "dd-M-yyyy","d-MM-yyyy","yyyy MM dd",
            "yyyy M d","dd MM yyyy","d M yyyy",
            "dd M yyyy","d MM yyyy", "MM/dd/yyyy"};

            CultureInfo arCul;
            CultureInfo enCul;

            arCul = new CultureInfo("ar-SA");
            enCul = new CultureInfo("en-US");

            var h = new UmAlQuraCalendar();
            var g = new GregorianCalendar(GregorianCalendarTypes.USEnglish);

            arCul.DateTimeFormat.Calendar = h;
            if (greg.Length <= 0)
            {
                return "";
            }
            try
            {
                DateTime tempDate = DateTime.ParseExact(greg, allFormats,
                    enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                //return tempDate.ToString("dd/MM/yyyy", arCul.DateTimeFormat);
                return tempDate.ToString("yyyy-MM-dd", arCul.DateTimeFormat);
            }
            catch (System.Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Retrieve lookup field name of 1 to many relationship
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityName"></param>
        /// <param name="relationName"></param>
        public static OneToManyRelationshipMetadata GetRelationAttribute(IOrganizationService service, string entityName, string relationName)
        {
            var entityProperties = new MetadataPropertiesExpression
            {
                AllProperties = false
            };
            entityProperties.PropertyNames.Add("ManyToOneRelationships");

            var entityFilter = new MetadataFilterExpression(LogicalOperator.And);
            entityFilter.Conditions
                  .Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityName));

            var relationFilter = new MetadataFilterExpression(LogicalOperator.And);
            relationFilter.Conditions
                  .Add(new MetadataConditionExpression("SchemaName", MetadataConditionOperator.Equals, relationName));


            var relationProperties = new MetadataPropertiesExpression
            {
                AllProperties = true
            };
            //relationProperties.PropertyNames.AddRange(attribute.ToString());

            var entityQueryExpression = new EntityQueryExpression
            {
                Criteria = entityFilter,
                Properties = entityProperties,
                RelationshipQuery = new RelationshipQueryExpression
                {
                    Properties = relationProperties,
                    Criteria = relationFilter
                }
            };

            var retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest
            {
                Query = entityQueryExpression,
                ClientVersionStamp = null
            };

            var response = ((RetrieveMetadataChangesResponse)service.Execute(retrieveMetadataChangesRequest)).EntityMetadata;

            if (response != null)
            {
                var metadata = response.FirstOrDefault();

                RelationshipMetadataBase relationMetadata = null;

                if (metadata != null)
                {
                    if (metadata.OneToManyRelationships != null)
                    {
                        relationMetadata = metadata.OneToManyRelationships.FirstOrDefault();
                    }
                    if (metadata.ManyToOneRelationships != null)
                    {
                        relationMetadata = metadata.ManyToOneRelationships.FirstOrDefault();
                    }
                    if (metadata.ManyToManyRelationships != null)
                    {
                        relationMetadata = metadata.ManyToManyRelationships.FirstOrDefault();
                    }

                    return (OneToManyRelationshipMetadata)relationMetadata;
                }
            }

            return null;
        }



        /// <summary>
        /// Search for entity with given criteria and once found it, it will update the given entity with the result
        /// </summary>
        /// <param name="context_entity"></param>
        /// <param name="entityLogicalNameToSearchIn"></param>
        /// <param name="fieldLogicalNameToSearchOn"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="entityRefThatWillContainResult"></param>
        /// <param name="tracer"></param>
        /// <param name="organizationService"></param>
        /// <returns></returns>
        public static bool QuickFind(EntityReference context_entity, string entityLogicalNameToSearchIn, string fieldLogicalNameToSearchOn, string searchCriteria, string fieldLogicalNameWillContainResult, IOrganizationService organizationService, bool isTheSearchCriteriaLogicalNameField = false)
        {
            var foundSomething = false;
            EntityReference entityRefrenceFound = null;
            object searchCriteriaObject = null;
            if (isTheSearchCriteriaLogicalNameField)
            {
                var results =
                    organizationService.Retrieve(context_entity.LogicalName, context_entity.Id, new ColumnSet() { Columns = { searchCriteria } });

                if (!results.Contains(searchCriteria))
                    return foundSomething;

                searchCriteriaObject = results[searchCriteria];

                entityRefrenceFound =
                    QuickFind(entityLogicalNameToSearchIn, fieldLogicalNameToSearchOn, searchCriteriaObject, organizationService);
            }
            else
            {
                entityRefrenceFound =
                     QuickFind(entityLogicalNameToSearchIn, fieldLogicalNameToSearchOn, searchCriteria, organizationService);
            }

            if (entityRefrenceFound != null && entityRefrenceFound.Id != Guid.Empty)
            {
                var entityToUpdate = new Entity(context_entity.LogicalName);
                entityToUpdate.Id = context_entity.Id;
                entityToUpdate.Attributes.Add(fieldLogicalNameWillContainResult, entityRefrenceFound);
                organizationService.Update(entityToUpdate);
                foundSomething = true;
            }
            return foundSomething;
        }

        public static EntityReference QuickFind(EntityReference context_entity, string entityLogicalNameToSearchIn, string fieldLogicalNameToSearchOn, string searchCriteria, ref StringBuilder tracer, IOrganizationService organizationService)
        {
            EntityReference entityRefrenceFound = null;
            object searchCriteriaObject = null;

            var results =
                organizationService.Retrieve(context_entity.LogicalName, context_entity.Id, new ColumnSet() { Columns = { searchCriteria } });

            if (!results.Contains(searchCriteria))
                return entityRefrenceFound;

            searchCriteriaObject = results[searchCriteria];

            entityRefrenceFound =
                QuickFind(entityLogicalNameToSearchIn, fieldLogicalNameToSearchOn, searchCriteriaObject, organizationService);

            return entityRefrenceFound;
        }


        public static EntityReference QuickFind(string entityLogicalNameToSearchIn, string fieldLogicalNameToSearchOn, object searchCriteria, IOrganizationService organizationService)
        {
            EntityReference entityRefrence = null;

            // Safety First :)
            //
            if (string.IsNullOrEmpty(entityLogicalNameToSearchIn) || string.IsNullOrEmpty(fieldLogicalNameToSearchOn) || searchCriteria == null)
                return entityRefrence;

            var paramobject = new object[] { };

            var query = new QueryExpression(entityLogicalNameToSearchIn);

            if (searchCriteria is EntityReference)
            {
                paramobject = new object[] { (searchCriteria as EntityReference).Id };
            }
            else if (searchCriteria is OptionSetValue)
            {
                paramobject = new object[] { (searchCriteria as OptionSetValue).Value };
            }
            else
            {
                paramobject = new object[] { searchCriteria };
            }
            query.Criteria.AddCondition(fieldLogicalNameToSearchOn, ConditionOperator.Equal, paramobject);

            var req = organizationService.RetrieveMultiple(query);

            if (req.Entities.Count > 1)
            {
                throw new InvalidOperationException($"Found more than one record with value '{searchCriteria.ToString()}' for attribute '{searchCriteria}'");
            }
            else if (req.Entities.Count <= 0)
            {

            }
            else
            {
                entityRefrence = req.Entities[0].ToEntityReference();
            }

            return entityRefrence;
        }

        /// <summary>
        /// Associate a record from entity to another entity that has relationship Many to Many with that record entity
        /// </summary>
        /// <param name="commonEntityBetweenSrsDstLogicalName"></param>
        /// <param name="destinationM2MRelationName"></param>
        /// <param name="destinationIntersectEntityName"></param>
        /// <param name="destination"></param>
        /// <param name="associatedEntitiesToSource"></param>
        /// <param name="clearAnyRecordsInDestination"></param>
        /// <param name="organizationService"></param>
        public static void AssociateEntityToOnther(string commonEntityBetweenSrsDstLogicalName, string destinationM2MRelationName, string destinationIntersectEntityName, EntityReference destination, EntityReferenceCollection associatedEntitiesToSource, bool clearAnyRecordsInDestination, IOrganizationService organizationService)
        {
            var CompanyActivitiesInstanceRef = new EntityReference();
            var companyCreationRequestCol = new ColumnSet();
            var IATCol = new ColumnSet();

            var associatedEntitiesToDistination =
                GetAssociatedEntities(destination, destinationIntersectEntityName, commonEntityBetweenSrsDstLogicalName, organizationService);

            var entitiesToAssociate =
                GetDistinctItemsInY(associatedEntitiesToDistination, associatedEntitiesToSource);

            if (clearAnyRecordsInDestination)
            {
                var entitiesToDisassociate =
                    GetDistinctItemsInY(associatedEntitiesToSource, associatedEntitiesToDistination);

                if (entitiesToDisassociate != null && entitiesToDisassociate.Count > 0)
                    organizationService.Disassociate(destination.LogicalName, destination.Id, new Relationship(destinationM2MRelationName), entitiesToDisassociate);
            }

            if (entitiesToAssociate != null && entitiesToAssociate.Count > 0)
                organizationService.Associate(destination.LogicalName, destination.Id, new Relationship(destinationM2MRelationName), entitiesToAssociate);
        }


        /// <summary>
        /// Associate existence records from source to destination if there a common entity between
        /// </summary>
        /// <param name="commonEntityBetweenSrsDstLogicalName">Common entity logical name</param>
        /// <param name="source">source logical name</param>
        /// <param name="sourceM2MRelationName">relathionship name between both source and common entity</param>
        /// <param name="sourceIntersectEntityName"></param>
        /// <param name="destination"></param>
        /// <param name="destinationM2MRelationName"></param>
        /// <param name="destinationIntersectEntityName"></param>
        /// <param name="clearAnyRecordsInDestination">will remove any records in destination not in source</param>
        /// <param name="organizationService"></param>
        public static void MoveM2MRecordsFromRelationtoAnother(string commonEntityBetweenSrsDstLogicalName, EntityReference source, string sourceM2MRelationName, string sourceIntersectEntityName, EntityReference destination, string destinationM2MRelationName, string destinationIntersectEntityName, bool clearAnyRecordsInDestination, IOrganizationService organizationService)
        {
            var CompanyActivitiesInstanceRef = new EntityReference();
            var companyCreationRequestCol = new ColumnSet();
            var IATCol = new ColumnSet();

            var associatedEntitiesToSource =
                GetAssociatedEntities(source, sourceIntersectEntityName, commonEntityBetweenSrsDstLogicalName, organizationService);

            var associatedEntitiesToDistination =
                GetAssociatedEntities(destination, destinationIntersectEntityName, commonEntityBetweenSrsDstLogicalName, organizationService);

            var entitiesToAssociate =
                GetDistinctItemsInY(associatedEntitiesToDistination, associatedEntitiesToSource);

            if (clearAnyRecordsInDestination)
            {
                var entitiesToDisassociate =
                    GetDistinctItemsInY(associatedEntitiesToSource, associatedEntitiesToDistination);

                if (entitiesToDisassociate != null && entitiesToDisassociate.Count > 0)
                    organizationService.Disassociate(destination.LogicalName, destination.Id, new Relationship(destinationM2MRelationName), entitiesToDisassociate);
            }

            if (entitiesToAssociate != null && entitiesToAssociate.Count > 0)
                organizationService.Associate(destination.LogicalName, destination.Id, new Relationship(destinationM2MRelationName), entitiesToAssociate);
        }

        public static EntityReferenceCollection GetAssociatedEntities(EntityReference target, string intersectEntityName, string otherEntityLogicalName, IOrganizationService organizationService)
        {
            var result = new EntityReferenceCollection();
            var targetFieldNameinM2MEntity = target.LogicalName + "id";
            var otherEntityFieldNameinM2MEntity = otherEntityLogicalName + "id";

            // Get current associated activites given request
            var query = new QueryExpression(intersectEntityName);
            query.ColumnSet = new ColumnSet(true);

            var filter = new FilterExpression();
            filter.Conditions.Add(new ConditionExpression(targetFieldNameinM2MEntity, ConditionOperator.Equal, target.Id));
            query.Criteria.AddFilter(filter);

            var associatedEntitities =
                organizationService.RetrieveMultiple(query);

            if (associatedEntitities != null && associatedEntitities.Entities != null && associatedEntitities.Entities.Count > 0)
            {
                foreach (var item in associatedEntitities.Entities)
                {
                    if (item.Contains(otherEntityFieldNameinM2MEntity))
                    {
                        var guid = (Guid)item[otherEntityFieldNameinM2MEntity];
                        result.Add(new EntityReference(otherEntityLogicalName, guid));
                    }
                }
            }

            return result;
        }

        public static EntityReferenceCollection GetRelatedRecords(EntityReference entity1Reference, string entity2LogicalName, string entity1LookupLogicalNameAtEntity2, IOrganizationService organizationService)
        {
            var result = new EntityReferenceCollection();
            var query = new QueryExpression()
            {
                EntityName = entity2LogicalName,
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(entity1LookupLogicalNameAtEntity2,ConditionOperator.Equal,entity1Reference.Id)
                    }
                }
            };

            var retrievedEntities = organizationService.RetrieveMultiple(query);

            foreach (var item in retrievedEntities.Entities)
            {
                result.Add(item.ToEntityReference());
            }

            return result;
        }


        public static EntityReferenceCollection GetDistinctItemsInY(EntityReferenceCollection x, EntityReferenceCollection y)
        {
            var res = new EntityReferenceCollection();

            foreach (var ys in y)
            {
                var found = false;
                foreach (var xs in x)
                {
                    if (xs.Equals(ys))
                    {
                        found = true;
                        continue;
                    }
                }
                if (found)
                    continue;

                res.Add(ys);
            }

            return res;
        }
        public static string CreateXml(XmlDocument doc, string cookie, int page, int count)
        {
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;

            if (cookie != null)
            {
                XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                attrs.Append(pagingAttr);
            }

            XmlAttribute pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(page);
            attrs.Append(pageAttr);

            XmlAttribute countAttr = doc.CreateAttribute("count");
            countAttr.Value = System.Convert.ToString(count);
            attrs.Append(countAttr);

            StringBuilder sb = new StringBuilder(1024);
            StringWriter stringWriter = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(stringWriter);
            doc.WriteTo(writer);
            writer.Close();

            return sb.ToString();
        }
        public static string CreateXml(string xml, string cookie, int page, int count)
        {
            StringReader stringReader = new StringReader(xml);
            XmlTextReader reader = new XmlTextReader(stringReader);

            // Load document
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml(doc, cookie, page, count);
        }
        public static void UpdateStatus(string entityName, Guid guid, int state, int status, IOrganizationService organizationService)
        {

            var moniker = new EntityReference();
            moniker.LogicalName = entityName;
            moniker.Id = guid;

            var request = new OrganizationRequest() { RequestName = "SetState" };
            request["EntityMoniker"] = moniker;
            OptionSetValue _state = new OptionSetValue(state);
            OptionSetValue _status = new OptionSetValue(status);
            request["State"] = _state;
            request["Status"] = _status;
            organizationService.Execute(request);
        }

        public static double GetDistanceBetween2Sites(double lat1, double lon1, double lat2, double lon2)
        {

            double dist = (((Math.Acos(Math.Sin((Math.PI * lat1 / 180)) * Math.Sin((Math.PI * lat2 / 180)) + Math.Cos((Math.PI * lat1 / 180)) * Math.Cos((Math.PI * lat2 / 180)) * Math.Cos((Math.PI * (lon1 - lon2) / 180)))) * 180) / Math.PI) * 60 * 1.1515 * 1.609344; ;

            return dist;
        }

        /// <summary>
        /// Get MiMEType of given filename with its extension
        /// </summary>
        /// <param name="fileName">ex: paper.pdf</param>
        /// <returns></returns>
        public static string GetMimeType(string fileName)
        {
            // Default unknown mimeType
            //
            var mimeType = "application/unknown";

            // Get extension from file name example if file name is somefilename.pdf then
            // extension will be pdf
            //
            var ext = System.IO.Path.GetExtension(fileName).ToLower();

            // talk to windows to get the mimeType for the extension
            //
            var regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            // if windows knows it then return the mimeType
            if (regKey?.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();


            return mimeType;

        }

        public static string ExtractPlainTextFromEmailDescription(string emailDescription)
        {

            string ExtractedEmailText = Regex.Replace(emailDescription, "(&lt;(((?!/&gt;).)*)/&gt;)|(&lt;(((?!&gt;).)*)&gt;)|(<(((?!/>).)*)/>)|(<(((?!>).)*)>)", String.Empty);
            ExtractedEmailText = Regex.Replace(ExtractedEmailText, "&amp;nbsp;", String.Empty);
            ExtractedEmailText = Regex.Replace(ExtractedEmailText, "&nbsp;", String.Empty);
            ExtractedEmailText = Regex.Replace(ExtractedEmailText, "&quot;", "'");
            return ExtractedEmailText;
        }

        public static void Update1toMEntitiesWithGivenValue(IOrganizationService organizationService, EntityReference contextEntity, string valueToUpdateRelatedEntities, string relatedEntitiesLogicalName, string relatedEntitieslookupToParentEntity, string fieldNeededToBeUpdatedInRelatedEntities, bool overrideIfFoundValue)
        {
            EntityReference ValuetoUpdateRelatedEntitiesValue = Tools.GetFieldValue(contextEntity, valueToUpdateRelatedEntities, organizationService);
            var subVenuesExpression = new QueryExpression()
            {
                EntityName = relatedEntitiesLogicalName,
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(relatedEntitieslookupToParentEntity,ConditionOperator.Equal,contextEntity.Id )
                    }
                }
            };

            var RelatedEntities = organizationService.RetrieveMultiple(subVenuesExpression);

            var entiteisToBeUpdated = new EntityCollection();
            foreach (var item in RelatedEntities.Entities)
            {
                if ((item.Contains(fieldNeededToBeUpdatedInRelatedEntities) && overrideIfFoundValue) ||
                    !item.Contains(fieldNeededToBeUpdatedInRelatedEntities))
                {
                    entiteisToBeUpdated.Entities.Add(
                    new Entity()
                    {
                        LogicalName = item.LogicalName,
                        Id = item.Id,
                        Attributes = new AttributeCollection()
                        {
                               new KeyValuePair<string, object>(fieldNeededToBeUpdatedInRelatedEntities, ValuetoUpdateRelatedEntitiesValue )
                        }
                    });
                }
            }
            foreach (var item in entiteisToBeUpdated.Entities)
            {
                organizationService.Update(item);
            }
        }

        public static void MapValue(IOrganizationService organizationService, bool clearOldValues, string values, EntityReference targetEntity, string targetFieldSchemaName)
        {
            var AllValues = values.Split(',');
            if (AllValues.Length > 0)
            {
                var _collection = new OptionSetValueCollection();

                if (!clearOldValues)
                {
                    var retrievedTargetEntity =
                        organizationService.Retrieve(targetEntity.LogicalName, targetEntity.Id, new ColumnSet(new[] { targetFieldSchemaName }));
                    if (retrievedTargetEntity.Contains(targetFieldSchemaName))
                    {
                        _collection = retrievedTargetEntity[targetFieldSchemaName] as OptionSetValueCollection;
                    }
                }

                foreach (var item in AllValues)
                {
                    var optionSet = new OptionSetValue(int.Parse(item));
                    if (!_collection.Contains(optionSet))
                        _collection.Add(optionSet);
                }

                Entity updatedEntity = new Entity(targetEntity.LogicalName, targetEntity.Id);
                updatedEntity.Attributes.Add(targetFieldSchemaName, _collection);
                organizationService.Update(updatedEntity);
            }
        }
        #endregion

        #region Private
        private static EntityReference GetFieldValue(EntityReference primaryEntity, string lookupName, IOrganizationService organizationService)
        {
            Entity currEntity = new Entity();
            string fieldValue = string.Empty;

            ColumnSet retrievedCols = new ColumnSet(new string[] { lookupName });

            currEntity = organizationService.Retrieve(
                primaryEntity.LogicalName,
                primaryEntity.Id,
                retrievedCols);

            if (currEntity.Attributes.Contains(lookupName))
                return currEntity.Attributes[lookupName] as EntityReference;

            return null;
        }
        #endregion

        public static void UpdateRecordsFrom1ToMWithGivenValues(IOrganizationService organizationService,
                                                    string entityLogicalName,
                                                    string entityId,
                                                    string related1ToMEntitySchemaName,
                                                    bool isLookup, string lookupSchemaName, string entityIdValue, string entitySchemaName,
                                                    bool isOptionSet, string optionSetCodeValue, string OptionSetSchemaName,
                                                    bool isTwoOption, string twoOptionCodeValue,
                                                    bool isString, string stringCodeValue,
                                                    bool isDate, DateTime dateCodeValue,
                                                    bool isCurrency, Money currencyValue,
                                                     ILogger tracer)
        {
            if (related1ToMEntitySchemaName != null && related1ToMEntitySchemaName != string.Empty
                && entityId != null && entityId != string.Empty
                && related1ToMEntitySchemaName != null && related1ToMEntitySchemaName != string.Empty)
            {
                var relatedEntityQuery = new QueryExpression(related1ToMEntitySchemaName);
                relatedEntityQuery.Criteria.AddCondition(entityLogicalName + "id", ConditionOperator.Equal, new Guid(entityId));
                relatedEntityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0); //active
                EntityCollection relatedEntities = organizationService.RetrieveMultiple(relatedEntityQuery);

                if (!relatedEntities.Entities.Any())
                    tracer.LogComment(LinkDev.Common.Crm.Logger.LoggerHandler.GetMethodFullName(), $" There are no related entities for {related1ToMEntitySchemaName}", Logger.SeverityLevel.Info);

                //var multipleRequests = new ExecuteMultipleRequest
                //{
                //    Settings = new ExecuteMultipleSettings()
                //    {
                //        ContinueOnError = false,
                //        ReturnResponses = true
                //    },
                //    Requests = new OrganizationRequestCollection()
                //};
                foreach (Entity entity in relatedEntities.Entities)
                {
                    Entity targetEntity = new Entity(entity.LogicalName, entity.Id);
                    if (isOptionSet)
                    {
                        if (OptionSetSchemaName != null && OptionSetSchemaName != string.Empty && optionSetCodeValue != null && optionSetCodeValue != string.Empty)
                        {
                            //entity.Attributes.Add(OptionSetSchemaName, new OptionSetValue(int.Parse(optionSetCodeValue)));
                            targetEntity.Attributes.Add(OptionSetSchemaName, new OptionSetValue(int.Parse(optionSetCodeValue)));
                        }
                    }
                    if (isLookup)
                    {
                        if (lookupSchemaName != null && lookupSchemaName != string.Empty &&
                            entitySchemaName != null && entitySchemaName != string.Empty &&
                            entityIdValue != null && entityIdValue != string.Empty)
                        {
                            //lookup schemaname , entity schemaname ,entity id
                            targetEntity.Attributes.Add(lookupSchemaName, new EntityReference(entitySchemaName, new Guid(entityIdValue)));
                        }
                    }
                    if (isTwoOption || isString || isDate || isCurrency)
                    {
                        throw new System.Exception(string.Format($"Not working yet"));
                    }
                    organizationService.Update(targetEntity);
                    //UpdateRequest updateRequest = new UpdateRequest { Target = targetEntity };
                    //multipleRequests.Requests.Add(updateRequest);
                }

                //if (multipleRequests.Requests.Count > 0)
                //{
                //    tracer.LogComment(LoggerHandler.GetMethodFullName(), $"Request: {new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(multipleRequests)}", Logger.SeverityLevel.Info);

                //    var multipleResponses = (ExecuteMultipleResponse)organizationService.Execute(multipleRequests);

                //    tracer.LogComment(LoggerHandler.GetMethodFullName(), LoggerHandler.ExtractInfo(multipleResponses), Logger.SeverityLevel.Info);
                //}
            }
        }


        public static void SetServiceSetting(
            ILogger tracer,
            Entity entity,
            ServiceSettingEntityConditions conditions, IOrganizationService organizationService)
        {
            Guid serviceSettingId = Guid.Empty;

            if (string.IsNullOrWhiteSpace(conditions.DependentFieldName) && conditions.DependentFieldType == 0)
            {
                serviceSettingId = conditions.ServiceSettingConditions.FirstOrDefault().Value;
            }
            else
            {
                // check dependent field type and convert its value to string

                if (!entity.Attributes.ContainsKey(conditions.DependentFieldName))
                    throw new InvalidOperationException($"Field '{conditions.DependentFieldName}' does not contain data.");

                string dependentFieldValue = "";

                switch (conditions.DependentFieldType)
                {
                    case CrmFieldType.OptionSet:
                        dependentFieldValue = entity.GetAttributeValue<OptionSetValue>(conditions.DependentFieldName).Value.ToString();
                        break;
                    case CrmFieldType.Lookup:
                        dependentFieldValue = entity.GetAttributeValue<EntityReference>(conditions.DependentFieldName).Id.ToString();
                        break;
                }

                tracer.LogComment(
                    LoggerHandler.GetMethodFullName(),
                    $"Dependent field value: {dependentFieldValue}",
                    Logger.SeverityLevel.Info);

                // get service setting id based on dependent field value

                serviceSettingId = conditions.ServiceSettingConditions.FirstOrDefault(c => c.Key == dependentFieldValue).Value;
            }

            if (serviceSettingId == null || serviceSettingId == Guid.Empty)
                throw new InvalidOperationException("Dependent field value does not match in the service setting dictionary.");

            tracer.LogComment(
                LoggerHandler.GetMethodFullName(),
                $"Service setting id: {serviceSettingId}",
                Logger.SeverityLevel.Info);

            // get service setting entity
            var serviceEntity = organizationService.Retrieve("ldv_service", serviceSettingId, new ColumnSet("ldv_isrequiredpayment"));

            // set service setting to entity and update entity
            entity[conditions.ServiceSettingFieldName] = new EntityReference("ldv_service", serviceSettingId);

            // set is required payment to entity
            if (serviceEntity.GetAttributeValue<bool>("ldv_isrequiredpayment"))
                entity["ldv_isrequiredpayment"] = serviceEntity.GetAttributeValue<bool>("ldv_isrequiredpayment");

            #region retrieve PBF
            string fetchXml = @"<fetch>
                                  <entity name='ldv_service' >
                                    <attribute name='ldv_processid' />
                                    <filter>
                                      <condition attribute='ldv_serviceid' operator='eq' value='" + serviceSettingId + @"' />
                                    </filter>
                                  </entity>
                                </fetch>";
            EntityCollection BPFCollection = organizationService.RetrieveMultiple(new FetchExpression(fetchXml));
            if (BPFCollection.Entities.Count > 0)
            {
                RetrieveEntityRequest req = new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.All,
                    LogicalName = entity.LogicalName,
                    RetrieveAsIfPublished = true
                };
                RetrieveEntityResponse res = (RetrieveEntityResponse)organizationService.Execute(req);
                EntityMetadata currentEntity = res.EntityMetadata;
                if (currentEntity.Attributes.Select(w => w.LogicalName).Contains("ldv_processid") && BPFCollection[0].Contains("ldv_processid"))
                {
                    entity["ldv_processid"] = new EntityReference(((EntityReference)BPFCollection[0]["ldv_processid"]).LogicalName, ((EntityReference)BPFCollection[0]["ldv_processid"]).Id);
                    entity["processid"] = ((EntityReference)BPFCollection[0]["ldv_processid"]).Id;
                }
            }
            #endregion
        }


        public static void SetCurrentProcess(
            ILogger tracer,
            Entity entity,
            IOrganizationService organizationService)
        {
            Guid serviceSettingId = Guid.Empty;

            if (entity.GetAttributeValue<EntityReference>("ldv_servicesettingsid") != null)
                serviceSettingId = entity.GetAttributeValue<EntityReference>("ldv_servicesettingsid").Id;

            if (serviceSettingId == null || serviceSettingId == Guid.Empty)
                throw new InvalidOperationException("Service settings does not contain data.");

            tracer.LogComment(
                LoggerHandler.GetMethodFullName(),
                $"Service setting id: {serviceSettingId}",
                Logger.SeverityLevel.Info);

            #region retrieve PBF
            string fetchXml = @"<fetch>
                                  <entity name='ldv_service' >
                                    <attribute name='ldv_processid' />
                                    <filter>
                                      <condition attribute='ldv_serviceid' operator='eq' value='" + serviceSettingId + @"' />
                                    </filter>
                                  </entity>
                                </fetch>";

            EntityCollection BPFCollection = organizationService.RetrieveMultiple(new FetchExpression(fetchXml));
            if (BPFCollection.Entities.Count > 0)
            {
                RetrieveEntityRequest req = new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.All,
                    LogicalName = entity.LogicalName,
                    RetrieveAsIfPublished = true
                };
                RetrieveEntityResponse res = (RetrieveEntityResponse)organizationService.Execute(req);
                EntityMetadata currentEntity = res.EntityMetadata;
                if (currentEntity.Attributes.Select(w => w.LogicalName).Contains("ldv_processid") && BPFCollection[0].Contains("ldv_processid"))
                {
                    var setProcessRequest = new SetProcessRequest
                    {
                        Target = entity.ToEntityReference(),
                        NewProcess = new EntityReference(((EntityReference)BPFCollection[0]["ldv_processid"]).LogicalName, ((EntityReference)BPFCollection[0]["ldv_processid"]).Id)
                    };

                    organizationService.Execute(setProcessRequest);
                }
            }
            #endregion
        }
    }

    #region dependencies

    [DataContract]
    public class ServiceSettingEntityConditions
    {
        [DataMember]
        public string DependentFieldName { get; set; }
        [DataMember]
        public CrmFieldType DependentFieldType { get; set; }
        [DataMember]
        public string ServiceSettingFieldName { get; set; }
        [DataMember]
        public Dictionary<string, Guid> ServiceSettingConditions { get; set; }
    }
    public enum CrmFieldType
    {
        OptionSet = 1,
        Lookup = 2
    }

    #endregion
}
