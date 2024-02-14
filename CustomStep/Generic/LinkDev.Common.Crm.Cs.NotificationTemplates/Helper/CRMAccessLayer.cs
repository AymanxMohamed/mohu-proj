using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk.Discovery;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.IO;
using System.ServiceModel;
//using Microsoft.Xrm.Client.Services;
//using Microsoft.Xrm.Tooling.Connector;
//using Linkdev.CRM.DataContracts;
using LinkDev.Libraries.Common;
//using System.ServiceModel.Description;
//using Linkdev.CRM.DataContracts;
//using Linkdev.CRM.DataContracts;



//using Linkdev.CRM.DataContracts;

//using Linkdev.CRM.DataContracts; //Commented by MN to be checked

namespace Linkdev.MOE.CRM.DAL
{
    public class CRMAccessLayer
    {
        #region parameters/variables
        /// <summary>
        /// CRM data service
        /// </summary>
        private IOrganizationService OrganizationService;
        private CrmLog Logger;
        // private CachedOrganizationService cachedService;
        private IOrganizationService cachedService;



        public IOrganizationService ServiceProxy
        {
            get
            {
                return OrganizationService;
            }
        }

        public string UserLanguage { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="organizationServiceUrl"></param>
        /// <param name="domain"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public CRMAccessLayer(string organizationServiceUrl, string domain, string username, string password)
        {
            // Initialize Logging
            //Logger.InitializeLogging(logConfigPath);

            try
            {


                IServiceManagement<IOrganizationService> orgServiceManagement =
                    ServiceConfigurationFactory.CreateManagement<IOrganizationService>(
                    new Uri(organizationServiceUrl));


                if (domain == "IFD")
                {
                    Uri organizationUri = new Uri(organizationServiceUrl);
                    Uri discoveryUri = new Uri(organizationServiceUrl.Replace("Organization.svc", "discovery.svc"));


                    //to ignore certificates errors
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    ClientCredentials userCredentials = new ClientCredentials();
                    userCredentials.UserName.UserName = username;
                    userCredentials.UserName.Password = password;

                    IServiceConfiguration<IDiscoveryService> discoveryConfiguration = ServiceConfigurationFactory.CreateConfiguration<IDiscoveryService>(discoveryUri);
                    SecurityTokenResponse userResponseWrapper = discoveryConfiguration.Authenticate(userCredentials);
                    var _discServiceProxy = new DiscoveryServiceProxy(discoveryConfiguration, userResponseWrapper);
                    IServiceConfiguration<IOrganizationService> serviceConfiguration = ServiceConfigurationFactory.CreateConfiguration<IOrganizationService>(organizationUri);
                    var serviceProxy = new OrganizationServiceProxy(serviceConfiguration, userResponseWrapper);
                    serviceProxy.EnableProxyTypes();
                    OrganizationService = (IOrganizationService)serviceProxy;
                    // 1- logger
                    Logger = new CrmLog(OrganizationService);
                    // cachedService = new CachedOrganizationService(OrganizationService);
                }
                else
                {

                    System.Net.NetworkCredential netCred = new System.Net.NetworkCredential();
                    netCred.UserName = username;
                    netCred.Password = password;
                    netCred.Domain = domain;

                    Uri organizationUri = new Uri(organizationServiceUrl);
                    ClientCredentials cred = new ClientCredentials();
                    cred.Windows.ClientCredential = netCred;

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    OrganizationService = new
                    OrganizationServiceProxy(organizationUri, null, cred,
                    null);

                    ((OrganizationServiceProxy)OrganizationService).EnableProxyTypes();

                    //cachedService = new CachedOrganizationService(OrganizationService);
                }
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                if (Err.InnerException != null)
                {
                    Logger.Log(Err);
                }
                throw;
            }
        }

        public CRMAccessLayer(IOrganizationService service)
        {
            OrganizationService = service;
            Logger = new CrmLog(OrganizationService);
            // cachedService = new CachedOrganizationService(OrganizationService);
        }

        #endregion

        /// <summary>
        /// validate authentication of logged in user by security token 
        /// </summary>
        /// <returns></returns>
        public bool ValidateAuthentication()
        {
            try
            {
                OrganizationServiceProxy proxy = (OrganizationServiceProxy)OrganizationService;
                if (null != proxy.SecurityTokenResponse &&
                    DateTime.UtcNow.AddMinutes(10) >= proxy.SecurityTokenResponse.Response.Lifetime.Expires)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {

                return false;
            }
        }



        #region  public  Entity RetrieveEntity(string EntityId, string EntityName, string[] columns)
        /// <summary>
        /// this method retrieves a dynamic entity using the record GUID
        /// </summary>
        /// <param name="EntityId"></param>
        /// <param name="EntityName"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Entity RetrieveEntity(string EntityId, string EntityName, string[] columns)
        {
            Entity TargetEntity;

            ColumnSet attributes;

            try
            {
                TargetEntity = new Entity(EntityName);

                if (columns != null && columns.Length > 0)
                    attributes = new ColumnSet(columns);
                else
                    attributes = new ColumnSet() { AllColumns = true };

                TargetEntity = OrganizationService.Retrieve(EntityName, Guid.Parse(EntityId), attributes);

                return TargetEntity;
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }

        }

        public Entity RetrieveEntity(Guid EntityId, string EntityName, string[] columns)
        {
            Entity TargetEntity;

            ColumnSet attributes;

            try
            {
                TargetEntity = new Entity(EntityName);

                if (columns != null && columns.Length > 0)
                    attributes = new ColumnSet(columns);
                else
                    attributes = new ColumnSet() { AllColumns = true };

                TargetEntity = OrganizationService.Retrieve(EntityName, EntityId, attributes);

                return TargetEntity;
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }

        }

        public Entity RetrieveEntityWithRelatedEntities(string EntityId, string EntityName, string RelationShipName, string RelatedEntityName)
        {
            RetrieveRequest retrieveRequest = new RetrieveRequest();
            retrieveRequest.Target = new EntityReference(EntityName, Guid.Parse(EntityId));
            retrieveRequest.ColumnSet = new ColumnSet(true);

            retrieveRequest.RelatedEntitiesQuery = new RelationshipQueryCollection();

            //The name of the relationship
            retrieveRequest.RelatedEntitiesQuery.Add(new Relationship(RelationShipName),

                //Name of the related entities
                new QueryExpression(RelatedEntityName)
                {
                    //The columns to retrieve for the related records
                    ColumnSet = new ColumnSet(true),
                    //Note that you can further filter the related records to be returned by
                    //specifying the Criteria property for the QueryExpression.
                });
            var response = (RetrieveResponse)ServiceProxy.Execute(retrieveRequest);
            var entity = (Entity)response.Entity;

            return entity;
        }

        #endregion
        //TODO:
        #region public  List<BusinessEntity> RetrieveMultiple(string EntityName, string[] Attributes, string[] Values)
        /// <summary>
        /// Generic method that retrieves list of entities using set of attributes
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Attributes"></param>
        /// <param name="Values"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<Entity> RetrieveMultiple(string EntityName, string[] Attributes, object[] Values, string[] Columns, int pageNumber = 1, int count = 20)
        {

            try
            {
                List<Entity> Entities = new List<Entity>();

                if (Attributes.Length != 0 && Values.Length != 0)
                {
                    QueryByAttribute query = new QueryByAttribute(EntityName);

                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }
                    query.Attributes.AddRange(Attributes);
                    query.Values.AddRange(Values);

                    // Execute the retrieval.
                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);

                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Add(item);
                        }
                        return Entities;
                    }
                }
                else
                {
                    QueryExpression query = new QueryExpression(EntityName);
                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }

                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Add(item);
                        }
                        return Entities;
                    }
                }
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }
        /// <summary>
        /// Generic method that list of entities with paging  using set of attributes 
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Attributes"></param>
        /// <param name="Values"></param>
        /// <param name="Columns"></param>
        /// <param name="pageNumber"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityCollection RetrieveMultipleRequests(string EntityName, string[] Attributes, object[] Values, string[] Columns, int pageNumber = 1, int count = 20)
        {

            try
            {
                EntityCollection Entities = new EntityCollection();

                if (Attributes.Length != 0 && Values.Length != 0)
                {
                    QueryByAttribute query = new QueryByAttribute(EntityName);

                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }
                    query.Attributes.AddRange(Attributes);
                    query.Values.AddRange(Values);

                    query.PageInfo = new PagingInfo();
                    query.PageInfo.PageNumber = pageNumber;
                    query.PageInfo.ReturnTotalRecordCount = true;
                    query.PageInfo.Count = count;

                    // Execute the retrieval.
                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);

                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {
                        return retrieved;
                    }
                }
                else
                {
                    QueryExpression query = new QueryExpression(EntityName);
                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }

                    query.PageInfo = new PagingInfo();
                    query.PageInfo.PageNumber = pageNumber;
                    query.PageInfo.ReturnTotalRecordCount = true;
                    query.PageInfo.Count = count;

                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Entities.Add(item);
                        }
                        return Entities;
                    }
                }
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }

        public EntityCollection RetrieveMultipleRequests(string EntityName, string[] Attributes, object[] Values, string[] Columns)
        {

            try
            {
                EntityCollection Entities = new EntityCollection();

                if (Attributes.Length != 0 && Values.Length != 0)
                {
                    QueryByAttribute query = new QueryByAttribute(EntityName);

                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }
                    query.Attributes.AddRange(Attributes);
                    query.Values.AddRange(Values);


                    // Execute the retrieval.
                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);

                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        //foreach (var item in retrieved.Entities)
                        //{
                        //    Entities.Entities.Add(item);
                        //}
                        return retrieved;
                    }
                }
                else
                {
                    QueryExpression query = new QueryExpression(EntityName);
                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }


                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Entities.Add(item);
                        }
                        return Entities;
                    }
                }
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }



        public EntityCollection RetrieveMultipleRequests(String FetchXMLQuery, int pageNumber = 1, int PageSize = 20, String PagingCookie = "")
        {



            EntityCollection pagedInactiveOrders = new EntityCollection();
            //EntityCollection InactiveOrders = new EntityCollection();            


            string xmlOrders = CreateXml(FetchXMLQuery, PagingCookie, pageNumber, PageSize);

            // Excute the fetch query and get the xml result.
            RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
            {
                Query = new FetchExpression(xmlOrders)
            };

            pagedInactiveOrders = ((RetrieveMultipleResponse)OrganizationService.Execute(fetchRequest1)).EntityCollection;


            //pagedInactiveOrders = OrganizationService.RetrieveMultiple
            //    (new FetchExpression(xmlOrders));

            return pagedInactiveOrders;
        }

        // used in paging
        internal string CreateXml(string xml, string cookie, int page, int count)
        {
            StringReader stringReader = new StringReader(xml);
            XmlTextReader reader = new XmlTextReader(stringReader);

            // Load document
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml(doc, cookie, page, count);
        }

        // used in paging
        internal string CreateXml(XmlDocument doc, string cookie, int page, int count)
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


            XmlAttribute TotalRecordsCountAttr = doc.CreateAttribute("returntotalrecordcount");
            TotalRecordsCountAttr.Value = "true";
            attrs.Append(TotalRecordsCountAttr);

            StringBuilder sb = new StringBuilder(1024);
            StringWriter stringWriter = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(stringWriter);
            doc.WriteTo(writer);
            writer.Close();

            return sb.ToString();
        }



        public List<Entity> RetrieveMultiple(QueryBase query, out int recordcount)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                recordcount = retrieved.TotalRecordCount;



                if (retrieved.Entities.Count == 0)
                    return null;
                else
                {

                    foreach (var item in retrieved.Entities)
                    {
                        Entities.Add(item);
                    }
                    return Entities;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public List<Entity> RetrieveMultiple(QueryBase query)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);


                if (retrieved.Entities.Count == 0)
                    return null;
                else
                {

                    foreach (var item in retrieved.Entities)
                    {
                        Entities.Add(item);
                    }
                    return Entities;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    Logger.Log(ex);
                }
                throw;
            }
        }

        public List<Entity> RetrieveMultipleByQueryExpression(QueryExpression query)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);


                if (retrieved.Entities.Count == 0)
                    return null;
                else
                {

                    foreach (var item in retrieved.Entities)
                    {
                        Entities.Add(item);
                    }
                    return Entities;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    Logger.Log(ex);
                }
                throw;
            }
        }
        public List<Entity> RetrieveMultipleWithOutTotalCount(QueryBase query, out int totalNumberOfRecords)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);


                if (retrieved.Entities.Count == 0)
                {
                    totalNumberOfRecords = 0;
                    return null;
                }
                else
                {

                    foreach (var item in retrieved.Entities)
                    {
                        Entities.Add(item);
                    }
                    totalNumberOfRecords = retrieved.TotalRecordCount;
                    return Entities;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    Logger.Log(ex);
                }
                throw;
            }
        }


        public List<Entity> RetrieveMultipleCached(QueryBase query)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = cachedService.RetrieveMultiple(query);


                if (retrieved.Entities.Count == 0)
                    return null;
                else
                {

                    foreach (var item in retrieved.Entities)
                    {
                        Entities.Add(item);
                    }
                    return Entities;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    Logger.Log(ex);
                }
                throw;
            }
        }

        public EntityCollection RetrieveMultipleRequest(QueryBase query)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);

                return retrieved;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    Logger.Log(ex);
                }
                throw;
            }
        }

        public List<Entity> RetrieveMultiple(FetchExpression query)
        {
            try
            {
                //Microsoft.Xrm.Sdk.Client.OrganizationServiceContext a = new Microsoft.Xrm.Sdk.Client.OrganizationServiceContext(OrganizationService);
                //a.CreateQuery

                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);

                if (retrieved.Entities.Count == 0)
                    return null;
                else
                {

                    foreach (var item in retrieved.Entities)
                    {
                        Entities.Add(item);
                    }
                    return Entities;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    Logger.Log(ex);
                }
                throw;
            }
        }

        public List<Entity> RetrieveMultipleCached(FetchExpression query)
        {
            try
            {
                //Microsoft.Xrm.Sdk.Client.OrganizationServiceContext a = new Microsoft.Xrm.Sdk.Client.OrganizationServiceContext(OrganizationService);
                //a.CreateQuery

                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = cachedService.RetrieveMultiple(query);

                if (retrieved.Entities.Count == 0)
                    return null;
                else
                {

                    foreach (var item in retrieved.Entities)
                    {
                        Entities.Add(item);
                    }
                    return Entities;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    Logger.Log(ex);
                }
                throw;
            }
        }

        public bool Associate(string relationshipName, string entity1Name, Guid entity1ID, string entity2Name, Guid entity2ID)
        {
            try
            {
                AssociateRequest req = new AssociateRequest()
                {
                    Target = new EntityReference(entity1Name, entity1ID),
                    RelatedEntities = new EntityReferenceCollection()
                    {
                        new EntityReference(entity2Name, entity2ID)
                    },
                    Relationship = new Relationship(relationshipName)
                };

                OrganizationService.Execute(req);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
        public bool Disassociate(EntityReference parentEntity, EntityReference childEntity, string strEntityRelationshipName)
        {
            try
            {
                // Create a request.
                DisassociateEntitiesRequest request = new DisassociateEntitiesRequest();

                // Assign the request a moniker for both entities that need to be disassociated.
                request.Moniker1 = parentEntity;
                request.Moniker2 = childEntity;

                // Set the relationship name that associates the two entities.
                request.RelationshipName = strEntityRelationshipName.Trim();

                // Execute the request.
                DisassociateEntitiesResponse response = (DisassociateEntitiesResponse)OrganizationService.Execute(request);

                return true;
            }

            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public List<Entity> RetrieveMultipleWithOrder(string EntityName, string[] Attributes, object[] Values, string[] Columns, OrderExpression oExpression)
        {

            try
            {
                List<Entity> Entities = new List<Entity>();

                if (Attributes.Length != 0 && Values.Length != 0)
                {
                    QueryByAttribute query = new QueryByAttribute(EntityName);
                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }
                    query.Attributes.AddRange(Attributes);
                    query.Values.AddRange(Values);
                    query.Orders.AddRange(oExpression);

                    // Execute the retrieval.
                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Add(item);
                        }
                        return Entities;
                    }
                }
                else
                {
                    QueryExpression query = new QueryExpression(EntityName);
                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }

                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Add(item);
                        }
                        return Entities;
                    }
                }
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }

        //public EntityCollection RetrieveMultipleByQueryExpression(QueryExpression query)
        //{
        //    try
        //    {
        //        EntityCollection retrievedData = OrganizationService.RetrieveMultiple(query);
        //        return retrievedData;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }           
        //}
        #endregion


        public SetStateResponse DeactivateEntity(String EntityName, Guid EntityID)
        {
            SetStateRequest setStateReq = new SetStateRequest();
            setStateReq.EntityMoniker = new EntityReference(EntityName, EntityID);
            setStateReq.State = new OptionSetValue((int)0);
            setStateReq.Status = new OptionSetValue(-1);

            SetStateResponse response = (SetStateResponse)OrganizationService.Execute(setStateReq);
            return response;
        }

        #region public  bool UpdateEntity(Entity entity)
        /// <summary>
        /// updates a dynamic entity using CRM Service
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="context"></param>
        /// <returns>bool</returns>
        public bool UpdateEntity(Entity entity)
        {
            try
            {
                OrganizationService.Update(entity);
                return true;
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }
        #endregion


        /// <summary>
        /// create record in the entity using the entity object as input parameter
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Guid CreateEntity(Entity entity)
        {
            try
            {
                return OrganizationService.Create(entity);
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }

        /// <summary>
        /// update status and status reason of the entity 
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="guid"></param>
        /// <param name="state"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(string entityName, string guid, int state, int status)
        {
            try
            {
                Microsoft.Xrm.Sdk.EntityReference moniker = new EntityReference();
                moniker.LogicalName = entityName;
                moniker.Id = new Guid(guid);

                Microsoft.Xrm.Sdk.OrganizationRequest request
                  = new Microsoft.Xrm.Sdk.OrganizationRequest() { RequestName = "SetState" };
                request["EntityMoniker"] = moniker;
                OptionSetValue _state = new OptionSetValue(state);
                OptionSetValue _status = new OptionSetValue(status);
                request["State"] = _state;
                request["Status"] = _status;

                OrganizationService.Execute(request);
                return true;
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }


        public  object RetrieveOptionSetValues(string EntityName, string optionSetSchemaName)
        {
            try
            {
                var attributeRequest = new RetrieveAttributeRequest
                {
                    EntityLogicalName = EntityName,
                    LogicalName = optionSetSchemaName,
                    RetrieveAsIfPublished = true
                };

                var attributeResponse = (RetrieveAttributeResponse)OrganizationService.Execute(attributeRequest);
                var attributeMetadata = (EnumAttributeMetadata)attributeResponse.AttributeMetadata;

                var optionList = (from o in attributeMetadata.OptionSet.Options
                                  select new { Value = o.Value, Text = o.Label.UserLocalizedLabel.Label }).ToList();


                return optionList;
            }
            catch (Exception Err)
            {

                throw;
            }
        }

        #region public string GetUserLanguage(string userId)
        /// <summary>
        /// Gets the current language for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="context"></param>
        /// <returns>string</returns>
        public string GetUserLanguage(string userId)
        {
            string currentLanguage = string.Empty;
            try
            {
                //retrieve user record
                Microsoft.Xrm.Sdk.Query.ColumnSet Fields = new Microsoft.Xrm.Sdk.Query.ColumnSet();
                Fields.AddColumn("uilanguageid");
                //Entity user = OrganiationService.Retrieve(EntityName.usersettings.ToString(),Guid.Parse(userId), Fields);
                Entity user = OrganizationService.Retrieve("usersettings", Guid.Parse(userId), Fields);


                //get language
                currentLanguage = user.Attributes["uilanguageid"].ToString();

                return currentLanguage;
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }
        #endregion

        /// <summary>
        /// create notes for for case entity
        /// </summary>
        /// <param name="strIncidentID"></param>
        /// <param name="strDescription"></param>
        /// <param name="strSubject"></param>
        /// <returns></returns>
        /// //TODO: generic for any entity
        public bool CreateNoteForIncident(string strIncidentID, string strDescription, string strSubject)
        {

            try
            {
                Entity Annotation = new Entity("annotation");
                Annotation["subject"] = strSubject;
                Annotation["notetext"] = strDescription;
                Annotation["objectid"] = new EntityReference("incident", new Guid(strIncidentID));
                //Annotation["langid"] = language;
                OrganizationService.Create(Annotation);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }


        /// <summary>
        /// Create email using the input parameters
        /// </summary>
        /// <param name="partyIds"></param>
        /// <param name="regardingObject"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Guid CreateEmail(EntityReference From, List<EntityReference> partyIds, EntityReference regardingObject, string subject, string message, List<EntityReference> ccList, List<EntityReference> bccList, EntityReference notificationConfig)
        {
            try
            {
                Entity[] toPartyList = new Entity[partyIds.Count];
                Entity[] frompartyList = new Entity[1];

                Entity email = new Entity("email");

                for (var i = 0; i < frompartyList.Length; i++)
                {
                    Entity fromParty = new Entity("activityparty");
                    fromParty.Attributes.Add("partyid", From);
                    frompartyList[i] = fromParty;
                }

                for (int i = 0; i < partyIds.Count; i++)
                {
                    Entity toParty = new Entity("activityparty");
                    toParty.Attributes.Add("partyid", partyIds[i]);
                    toPartyList[i] = toParty;
                }

                #region send email to cc ids if there is cc 
                if (ccList != null && ccList.Count > 0)
                {
                    Entity[] arrCC = new Entity[ccList.Count];
                    for (int i = 0; i < ccList.Count; i++)
                    {
                        Entity ccParty = new Entity("activityparty");
                        ccParty.Attributes.Add("partyid", ccList[i]);
                        arrCC[i] = ccParty;
                    }
                    email.Attributes.Add("cc", arrCC);
                }
                #endregion

                #region send email to bcc ids if there is bcc 
                if (bccList != null && bccList.Count > 0)
                {
                    Entity[] arrBCC = new Entity[bccList.Count];
                    for (int i = 0; i < bccList.Count; i++)
                    {
                        Entity bccParty = new Entity("activityparty");
                        bccParty.Attributes.Add("partyid", bccList[i]);
                        arrBCC[i] = bccParty;
                    }
                    email.Attributes.Add("bcc", arrBCC);
                }
                #endregion

                email.Attributes.Add("subject", subject);
                email.Attributes.Add("description", message);
                email.Attributes.Add("from", frompartyList);
                email.Attributes.Add("to", toPartyList);
                email.Attributes.Add("regardingobjectid", (EntityReference)regardingObject);
                email.Attributes.Add("ldv_notificationconfigurationid", notificationConfig);

                Guid emailGuid = OrganizationService.Create(email);
                return emailGuid;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
        /// <summary>
        /// Send the email using email id 
        /// </summary>
        /// <param name="EmailID"></param>
        public void SendEmail(Guid EmailID)
        {
            try
            {
                // Create a SendEmail request.
                SendEmailRequest requestSendEmail = new SendEmailRequest();
                requestSendEmail.EmailId = EmailID;
                requestSendEmail.TrackingToken = "";
                requestSendEmail.IssueSend = true;

                // Send the e-mail message.
                SendEmailResponse responseSendEmail = (SendEmailResponse)OrganizationService.Execute(requestSendEmail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        /// <summary>
        /// Create SMS using the input parametres
        /// </summary>
        /// <param name="regardingId"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="message"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        /// 

        /// <summary>
        /// Retrieve user name from entity users by userID
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string RetrieveUserName(Guid UserId)
        {
            string UserName = string.Empty;
            //DynamicEntity User = RetrieveDynamicEntity(UserId.ToString(), EntityName.systemuser.ToString());
            try
            {
                //retrieve user record
                Microsoft.Xrm.Sdk.Query.ColumnSet Fields = new Microsoft.Xrm.Sdk.Query.ColumnSet();
                Fields.AddColumn("fullname");
                Entity user = OrganizationService.Retrieve("usersettings", UserId, Fields);

                //get user name
                UserName = user.Attributes["fullname"].ToString();

                return UserName;
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }


        }

        /// <summary>
        /// Get Users IDs by the security role name
        /// </summary>
        /// <param name="sRoles"></param>
        /// <returns></returns>
        public List<Guid> GetUserIDsByRole(string sRoles)
        {
            try
            {
                string[] roles = sRoles.Split(',');

                for (int i = 0; i < roles.Length; i++)
                {
                    roles[i] = roles[i].Trim();
                }

                QueryExpression rolesQuery = new QueryExpression("role");
                rolesQuery.ColumnSet = new ColumnSet(new string[] { "roleid" });
                rolesQuery.Criteria = new FilterExpression(LogicalOperator.And);
                rolesQuery.Criteria.AddCondition("name", ConditionOperator.In, roles);

                EntityCollection rolesCollection = OrganizationService.RetrieveMultiple(rolesQuery);

                // Terminate if no valid roles returned.
                if (rolesCollection.Entities.Count == 0)
                {
                    return new List<Guid>();
                }

                Guid[] roleIds = new Guid[rolesCollection.Entities.Count];

                for (int i = 0; i < rolesCollection.Entities.Count; i++)
                {
                    roleIds[i] = rolesCollection.Entities[i].Id;
                }

                QueryExpression linkQuery = new QueryExpression()
                {
                    EntityName = "systemuser",
                    ColumnSet = new ColumnSet("systemuserid"),
                    LinkEntities =
                        {
                            new LinkEntity()
                            {
                                LinkFromEntityName = "systemuser",
                                LinkFromAttributeName = "systemuserid",
                                LinkToEntityName = "systemuserroles",
                                LinkToAttributeName = "systemuserid",
                                LinkEntities = { new LinkEntity()
                                {
                                    LinkFromEntityName = "systemuserroles",
                                    LinkFromAttributeName = "roleid",
                                    LinkToEntityName = "role",
                                    LinkToAttributeName = "roleid",
                                    LinkCriteria =
                                    {
                                        Conditions = {
                                                        new ConditionExpression("roleid", ConditionOperator.In, roleIds)
                                                    }
                                    }
                                }
                                }
                            }
                        }
                };

                EntityCollection usersCollection = OrganizationService.RetrieveMultiple(linkQuery);

                if (usersCollection != null && usersCollection.Entities != null && usersCollection.Entities.Count > 0)
                {
                    List<Guid> userIds = new List<Guid>();

                    foreach (Entity user in usersCollection.Entities)
                    {
                        userIds.Add(user.Id);
                    }

                    return userIds;
                }
                else
                {
                    return new List<Guid>();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }



        /// <summary>
        /// Retrieve Drop Down Values
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        /// 
        public OptionMetadataCollection RetrieveDropdownValues(string entityName, string attributeName)
        {
            try
            {
                RetrieveAttributeRequest req = new RetrieveAttributeRequest();
                req.EntityLogicalName = entityName;
                req.LogicalName = attributeName;

                RetrieveAttributeResponse resp = (RetrieveAttributeResponse)OrganizationService.Execute(req);


                PicklistAttributeMetadata objPckLstAttMetadata = new PicklistAttributeMetadata();

                ICollection<object> objCollection = resp.Results.Values;

                objPckLstAttMetadata.OptionSet = ((EnumAttributeMetadata)(objCollection.ElementAt(0))).OptionSet;
                objPckLstAttMetadata.RequiredLevel = ((EnumAttributeMetadata)(objCollection.ElementAt(0))).RequiredLevel;

                return objPckLstAttMetadata.OptionSet.Options;

            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }



        /// <summary>
        /// Delete Entity by entity name and record ID the return bool value = true if it is deleted scussfully else the flg = false
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEntity(string entityName, Guid id)
        {
            try
            {
                OrganizationService.Delete(entityName, id);
                return true;
            }
            catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }



        /// <summary>
        /// create attachments foe email with etension and mime type based on input parametrs
        /// </summary>
        /// <param name="_emailId"></param>
        /// <param name="file"></param>
        /// <param name="subject"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public Guid AttachFileToEmail(Guid _emailId, byte[] file, string subject, string fileName, string fileExtension, string mimeType)
        {
            //Logger.LogInformation("In CreateAttachment Method");
            Guid _emailAttachmentId;


            //   ((OrganizationServiceProxy)OrganizationService).EnableProxyTypes();
            Entity attachment = new Entity("activitymimeattachment");
            attachment.Attributes["objectid"] = new EntityReference("email", _emailId);
            attachment.Attributes["objecttypecode"] = "email";
            attachment.Attributes["filename"] = fileName + fileExtension;
            attachment.Attributes["subject"] = subject;
            attachment["mimetype"] = mimeType;
            attachment.Attributes["body"] = System.Convert.ToBase64String(file);//System.Convert.ToBase64String(file);
            _emailAttachmentId = OrganizationService.Create(attachment);

            return _emailAttachmentId;
        }


        /// <summary>
        /// associate record between M:M entities 
        /// </summary>
        /// <param name="relationshipName"></param>
        /// <param name="entityname"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        public void AssociateSelfEntity(string relationshipName, string entityname, Guid id1, Guid id2)
        {
            Relationship relation = new Relationship(relationshipName);

            relation.PrimaryEntityRole = EntityRole.Referenced;

            EntityReference eRef = new EntityReference(entityname, id2);

            OrganizationService.Associate(entityname, id1, relation, new EntityReferenceCollection() { eRef });
        }

        /// <summary>
        /// Deasociate record between M:M Records
        /// </summary>
        /// <param name="relationshipName"></param>
        /// <param name="entityname"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        public void DisassociateSelfEntity(string relationshipName, string entityname, Guid id1, Guid id2)
        {
            Relationship relation = new Relationship(relationshipName);

            relation.PrimaryEntityRole = EntityRole.Referenced;

            EntityReference eRef = new EntityReference(entityname, id2);

            OrganizationService.Disassociate(entityname, id1, relation, new EntityReferenceCollection() { eRef });
        }
        /// <summary>
        /// gat parent entity id 
        /// </summary>
        /// <param name="parentEntity"></param>
        /// <param name="relatedEntity"></param>
        /// <returns></returns>
        public Guid GetParentEntityGUID(string parentEntity, Entity relatedEntity)
        {
            Guid ParentGUID = Guid.Empty;

            RetrieveEntityRequest retrieveEntityRequest = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Relationships,
                LogicalName = parentEntity,

            };
            RetrieveEntityResponse retrieveEntityResponse = (RetrieveEntityResponse)OrganizationService.Execute(retrieveEntityRequest);

            if (retrieveEntityResponse != null && retrieveEntityResponse.EntityMetadata != null && retrieveEntityResponse.EntityMetadata.OneToManyRelationships.Count() > 0)
            {
                var oneToNRelationship = retrieveEntityResponse.EntityMetadata.OneToManyRelationships.Where(x => x.ReferencedEntity == parentEntity && x.ReferencingEntity == relatedEntity.LogicalName).ToList()[0];
                if (relatedEntity.Contains(oneToNRelationship.ReferencingAttribute))
                    ParentGUID = ((EntityReference)relatedEntity.Attributes[oneToNRelationship.ReferencingAttribute]).Id;
                else
                {
                    Entity entity = RetrieveEntity(relatedEntity.Id.ToString(), relatedEntity.LogicalName, new string[] { oneToNRelationship.ReferencingAttribute });
                    if (entity.Attributes.Contains(oneToNRelationship.ReferencingAttribute))
                        ParentGUID = ((EntityReference)entity.Attributes[oneToNRelationship.ReferencingAttribute]).Id;

                }
            }

            return ParentGUID;

        }

        #region Document Configurations

        public AttributeMetadata RetrieveAttribute(string EntitySchemaName, string AttributeSchemaName)
        {

            RetrieveAttributeRequest retrieveAttributeRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = EntitySchemaName,
                LogicalName = AttributeSchemaName,
                RetrieveAsIfPublished = true
            };
            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)OrganizationService.Execute(retrieveAttributeRequest);

            AttributeMetadata retrievedAttributeMetadata = (AttributeMetadata)retrieveAttributeResponse.AttributeMetadata;

            return retrievedAttributeMetadata;
        }

        public RetrieveEntityResponse RetrieveEntity(RetrieveEntityRequest request)
        {
            return (RetrieveEntityResponse)OrganizationService.Execute(request);
        }

        public Entity RetrieveEntityWithAlternateKey(string entityName, string fieldName, object fieldValue)
        {
            RetrieveRequest request = new RetrieveRequest()
            {
                ColumnSet = new ColumnSet(true),
                Target = new EntityReference(entityName, fieldName, fieldValue)
            };
            return ((RetrieveResponse)OrganizationService.Execute(request)).Entity;
        }

        public string CreateAttribute(string TargetEntityName, AttributeMetadata attribute, string AttributeLogicalName)
        {
            CreateAttributeRequest createAttributeRequest = new CreateAttributeRequest
            {
                EntityName = TargetEntityName,
            };

            string AttributeGuid = null;
            string AttributeDisplayName = null;

            if (attribute.AttributeType == AttributeTypeCode.String)
            {
                StringAttributeMetadata RetrievedStringAttribute = (StringAttributeMetadata)attribute;
                StringAttributeMetadata stringAttribute = new StringAttributeMetadata
                {
                    SchemaName = RetrievedStringAttribute.SchemaName,
                    DisplayName = RetrievedStringAttribute.DisplayName,
                    RequiredLevel = RetrievedStringAttribute.RequiredLevel,
                    Description = RetrievedStringAttribute.Description,
                    MaxLength = RetrievedStringAttribute.MaxLength,
                    IsSecured = RetrievedStringAttribute.IsSecured

                };
                createAttributeRequest.Attribute = stringAttribute;
                AttributeDisplayName = stringAttribute.DisplayName.UserLocalizedLabel.Label;
            }
            else if (attribute.AttributeType == AttributeTypeCode.Lookup)
            {
                LookupAttributeMetadata RetrievedLookupAttribute = (LookupAttributeMetadata)attribute;
                string schemaName = RetrievedLookupAttribute.Targets[0] + "_" + TargetEntityName + "_" + RetrievedLookupAttribute.Targets[0] + "id";

                AttributeDisplayName = RetrievedLookupAttribute.DisplayName.UserLocalizedLabel.Label;
                CreateOneToManyResponse createOneToManyRelationshipResponse = CreateOneToManyRelation(RetrievedLookupAttribute.Targets[0], TargetEntityName, schemaName, AttributeLogicalName, AttributeDisplayName, OrganizationService);

                AttributeGuid = createOneToManyRelationshipResponse.AttributeId.ToString();
                AttributeDisplayName = RetrievedLookupAttribute.DisplayName.UserLocalizedLabel.Label;
            }
            else if (attribute.AttributeType == AttributeTypeCode.Picklist)
            {

                PicklistAttributeMetadata pickListAttribute = (PicklistAttributeMetadata)attribute;
                OptionSetMetadata OptionSet = new OptionSetMetadata();
                if (pickListAttribute.OptionSet.IsGlobal == true)
                {
                    OptionSet = new OptionSetMetadata
                    {
                        IsGlobal = true,
                        Name = pickListAttribute.OptionSet.Name
                    };
                }
                else
                {
                    OptionSet = new OptionSetMetadata(pickListAttribute.OptionSet.Options)
                    {
                        IsGlobal = false,
                        //Options = pickListAttribute.OptionSet.Options
                    };

                }
                PicklistAttributeMetadata OptionSetAttribute = new PicklistAttributeMetadata
                {
                    SchemaName = pickListAttribute.SchemaName,
                    DisplayName = pickListAttribute.DisplayName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None), //pickListAttribute.RequiredLevel,
                    Description = pickListAttribute.Description,
                    IsSecured = pickListAttribute.IsSecured,
                    OptionSet = OptionSet
                };
                createAttributeRequest.Attribute = OptionSetAttribute;
                AttributeDisplayName = pickListAttribute.DisplayName.UserLocalizedLabel.Label;

            }



            if (attribute.AttributeType != AttributeTypeCode.Lookup)
            {
                CreateAttributeResponse createAttributeResponse =
                    OrganizationService.Execute(createAttributeRequest) as CreateAttributeResponse;

                AttributeGuid = createAttributeResponse.AttributeId.ToString();
            }


            return AttributeDisplayName;
        }



        public CreateOneToManyResponse CreateOneToManyRelation(string ReferencedEntity, string ReferencingEntity, string SchemaName, string AttributeLogicalName, string AttributeDisplayName, IOrganizationService OrganizationService)
        {

            CreateOneToManyRequest createOneToManyRelationshipRequest =
                new CreateOneToManyRequest
                {
                    OneToManyRelationship =
                        new OneToManyRelationshipMetadata
                        {
                            ReferencedEntity = ReferencedEntity,
                            ReferencingEntity = ReferencingEntity,
                            SchemaName = SchemaName,
                            AssociatedMenuConfiguration = new AssociatedMenuConfiguration
                            {
                                Behavior = AssociatedMenuBehavior.UseLabel,
                                Group = AssociatedMenuGroup.Details,
                                Label = new Label(ReferencedEntity, 1033),
                                Order = 10000
                            },
                            CascadeConfiguration = new CascadeConfiguration
                            {
                                Assign = CascadeType.NoCascade,
                                Delete = CascadeType.RemoveLink,
                                Merge = CascadeType.NoCascade,
                                Reparent = CascadeType.NoCascade,
                                Share = CascadeType.NoCascade,
                                Unshare = CascadeType.NoCascade,
                            }
                        },
                    Lookup = new LookupAttributeMetadata
                    {
                        SchemaName = AttributeLogicalName,
                        DisplayName = new Label(AttributeDisplayName, 1033),
                        RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                        Description = new Label(" Migrated Lookup", 1033)
                    }
                };


            CreateOneToManyResponse createOneToManyRelationshipResponse =
                (CreateOneToManyResponse)OrganizationService.Execute(
                    createOneToManyRelationshipRequest);


            return createOneToManyRelationshipResponse;


        }

        public UpdateResponse UpdateRequest(UpdateRequest req)
        {
            return OrganizationService.Execute(req) as UpdateResponse;
        }

        public PublishXmlResponse PublishRequest(PublishXmlRequest publishRequest)
        {
            return OrganizationService.Execute(publishRequest) as PublishXmlResponse;
        }

        public WhoAmIResponse WhoAmI(WhoAmIRequest request)
        {
            return (WhoAmIResponse)OrganizationService.Execute(request);
        }


        #endregion

        #region Common  Methods
        public static bool IsConditionMet(IOrganizationService service, string fetchXml,
           EntityReference record, bool isActivity = false)
        {
            fetchXml.RequireNotEmpty("fetchXml");

            var primaryIdName = isActivity ? "activityid" : record.LogicalName + "id";

            var finalFetchXml = "";

            var querySplit = fetchXml.Split(new[] { "</entity>" }, StringSplitOptions.None);

            finalFetchXml += querySplit[0];
            finalFetchXml += "<filter type='and'> ";
            finalFetchXml += "<condition attribute='" + primaryIdName + "' operator='eq' value= '" + record.Id + "' /> ";
            finalFetchXml += "</filter>" + "</entity>";
            finalFetchXml += querySplit[querySplit.Length - 1];

            var isSatisfied = service.RetrieveMultiple(new FetchExpression(finalFetchXml)).Entities.Any();

            return isSatisfied;
        }
        #endregion


        #region Methods I used in stage configurations custom steps 
        ////Aya Alaam:  Methods I used in  stage configurations custom steps
        /////////////////////

        /// <summary>
        /// Retrieve EntityCollection By using fetchXml
        /// </summary>
        /// <param name="fetchXml"></param>
        /// <returns></returns>
        public EntityCollection RetrieveMultipleByFetch(FetchExpression fetchXml)
        {
            EntityCollection retrieved = new EntityCollection(null);
            if (fetchXml != null)
            {
                retrieved = OrganizationService.RetrieveMultiple(fetchXml);
                if (retrieved != null)
                {
                    return retrieved;
                }
            }
            return new EntityCollection();
        }

        /// <summary>
        /// Retrive ColumnValue From retrive Entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entitySchemaName"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public object RetriveColumnValueFromEntity(Guid entityId, string entitySchemaName, string column
            )
        {
            try
            {
                Logger.Log("in fn. RetriveColumnValueFromEntity", LogLevel.Debug);
                if (entityId == Guid.Empty)
                {
                    Logger.Log("  entityId:  " + entityId, LogLevel.Debug);

                    return null;
                }
                Entity result = OrganizationService.Retrieve(entitySchemaName, entityId, new ColumnSet(column));
                Logger.Log("result id :  " + result.Id +
                        "  , result name :  " + result.LogicalName, LogLevel.Debug);
                Logger.Log(" , field value : " + result.Attributes[column].ToString(), LogLevel.Debug);

                if (result.Id == Guid.Empty)
                {
                    return null;
                }
                return result.Attributes.Contains(column) ? result.Attributes[column] : null;
            }
            catch (Exception ex)
            {
                Logger.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw;// new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                // Logger.LogFunctionEnd();
            }
        }

        /// <summary>
        /// "Is condition Met" generic method to execute a fetch xml on specific record
        /// </summary>
        /// <param name="fetchXml"></param>
        /// <param name="record"></param>
        /// <param name="isActivity"></param>
        /// <returns></returns>
        public bool IsConditionMet(string fetchXml, EntityReference record,
            bool isActivity = false)
        {
            fetchXml.RequireNotEmpty("fetchXml");

            var primaryIdName = isActivity ? "activityid" : record.LogicalName + "id";

            var finalfetchXml = "";

            var querySplit = fetchXml.Split(new[] { "</entity>" }, StringSplitOptions.None);

            finalfetchXml += querySplit[0];
            finalfetchXml += "<filter type='and'> ";
            finalfetchXml += "<condition attribute='" + primaryIdName + "' operator='eq' value= '" + record.Id +
                             "' /> ";
            finalfetchXml += "</filter>" + "</entity>";
            finalfetchXml += querySplit[querySplit.Length - 1];

            var isSatisfied = OrganizationService.RetrieveMultiple(new FetchExpression(finalfetchXml)).Entities.Any();

            return isSatisfied;
        }

        /// <summary>
        /// Check if field exist in spacific entity
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool DoesFieldExist(String entityName, String fieldName) //Code Review To be added in DAL after Merge
        {
            try
            {
                Logger.Log("in fn. DoesFieldExist", LogLevel.Debug);
                RetrieveEntityRequest request = new RetrieveEntityRequest
                {
                    EntityFilters = Microsoft.Xrm.Sdk.Metadata.EntityFilters.Attributes,
                    LogicalName = entityName
                };
                Logger.Log("EntityFilters : " + request.EntityFilters, LogLevel.Debug);

                RetrieveEntityResponse response
                    = (RetrieveEntityResponse)OrganizationService.Execute(request);
                Logger.Log("response : " + response.Results, LogLevel.Debug);
                Logger.Log("return : " +
                        response.EntityMetadata.Attributes.FirstOrDefault(element => element.LogicalName == fieldName), LogLevel.Debug);

                return response.EntityMetadata.Attributes.FirstOrDefault(element => element.LogicalName == fieldName) !=
                       null;
            }
            catch (Exception ex)
            {
                Logger.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw;// new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                //Logger.LogFunctionEnd();
            }

        }

        /// <summary>
        /// retrive EntityReferenceCollection by using Query Expression
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public EntityCollection EntityReferenceCollectionByQueryExpression(Entity entity, string columns)
        {

            try
            {
                QueryExpression qe = new QueryExpression();
                qe.EntityName = entity.LogicalName;
                qe.ColumnSet = new ColumnSet();
                qe.ColumnSet.Columns.Add(columns);
                EntityCollection result1 = OrganizationService.RetrieveMultiple(qe);

                if (result1.Entities.Any())
                {
                    return result1;
                }
                return new EntityCollection(null);
            }
            catch (Exception ex)
            {
                Logger.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw;// new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                Logger.LogFunctionEnd();
            }


        }

        /// <summary>
        /// Retrive Primary Entity from it's Bpf instance
        /// </summary>
        /// <param name="bpfEntityLogicalName"></param>
        /// <param name="bpfEntityId"></param>
        /// <returns></returns>
        public Entity RetrivePrimaryEntityOfBpf(string bpfEntityLogicalName, Guid bpfEntityId)

        {
            try
            {
                Logger.Log("in RetrivePrimaryEntityOfBPF fn.", LogLevel.Debug);
                //  var bpfEntityLogicalName = "ldv_bpftestcustomstep";
                //string bpfEntityId = "{25DA21C8-5DFD-E811-8119-000D3A393005}";
                string entityLogicalName = String.Empty;
                RetrieveEntityRequest metadataRequest = new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.Attributes,
                    LogicalName = bpfEntityLogicalName
                };
                var eMetadataResponse = (RetrieveEntityResponse)OrganizationService.Execute(metadataRequest);
                var accountMetadata = eMetadataResponse.EntityMetadata.Attributes;
                foreach (var attribute in accountMetadata)
                {
                    if (attribute.LogicalName.Contains("bpf_ldv_") && !attribute.LogicalName.Contains("idname"))
                    {
                        entityLogicalName = attribute.LogicalName;
                        Logger.Log(" entityLogicalName : " + entityLogicalName, LogLevel.Debug);
                    }
                }

                if (entityLogicalName != String.Empty && bpfEntityId != Guid.Empty &&
                    bpfEntityLogicalName != String.Empty)
                {
                    if (entityLogicalName != String.Empty)
                    {
                        Entity target = OrganizationService.Retrieve(bpfEntityLogicalName,
                            bpfEntityId, new ColumnSet(entityLogicalName));
                        EntityReference targetEntityForBpf =
                            target.GetAttributeValue<EntityReference>(
                                entityLogicalName); //entityLogicalName.Replace("bpf_", "").Replace("id", ""));
                                                    // target.Attributes.Values

                        //object x=target.Attributes[bpfEntityLogicalName];
                        if (targetEntityForBpf != null)
                        {
                            Entity primaryEntity = OrganizationService.Retrieve(targetEntityForBpf.LogicalName,
                                targetEntityForBpf.Id, new ColumnSet(true));
                            //= targetEntityForBpf.Contains(entityLogicalName)
                            //    ? targetEntityForBpf.GetAttributeValue<EntityReference>(entityLogicalName)
                            //    : null;
                            if (primaryEntity != null)
                            {
                                Logger.Log(" primaryEntity : " + primaryEntity, LogLevel.Debug);
                                return primaryEntity;
                            }
                        }
                        else
                        {
                            Logger.Log("primaryEntity is  null ", LogLevel.Debug);
                        }
                    }
                    else
                    {
                        Logger.Log("target is  null ", LogLevel.Debug);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw;// new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                Logger.LogFunctionEnd();
            }


        }

        /// <summary>
        /// this method retrieves a dynamic entity using the record GUID
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Entity RetrieveEntityWithColumns(EntityReference entity, string[] columns)
        {
            try
            {
                Logger.Log("in RetrieveEntityWithColumns ", LogLevel.Info);

                Entity targetEntity = new Entity(entity.LogicalName);
                ColumnSet attributes;
                if (columns != null && columns.Length > 0)
                    attributes = new ColumnSet(columns);
                else
                    attributes = new ColumnSet() { AllColumns = true };

                targetEntity = OrganizationService.Retrieve(entity.LogicalName, entity.Id, attributes);
                Logger.Log("entity id: " + targetEntity.Id);
                return targetEntity;
            }
            catch (Exception ex)
            {
                Logger.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
                throw;// new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                // Logger.LogFunctionEnd();
            }

        }


        //////////////////////////
        #endregion

        public OptionMetadata[] GetOptionset(string entityName, IOrganizationService service, string optionsetName)
        {
            OptionMetadata[] optionList = new OptionMetadata[0];

            try
            {

                RetrieveOptionSetRequest retrieveOptionSetRequest =
                     new RetrieveOptionSetRequest
                     {
                         Name = optionsetName
                     };

                // Execute the request.
                RetrieveOptionSetResponse retrieveOptionSetResponse =
                    (RetrieveOptionSetResponse)service.Execute(retrieveOptionSetRequest);

                // Access the retrieved OptionSetMetadata.
                OptionSetMetadata retrievedOptionSetMetadata = (OptionSetMetadata)retrieveOptionSetResponse.OptionSetMetadata;

                // Get the current options list for the retrieved attribute.
                optionList = retrievedOptionSetMetadata.Options.ToArray();


            }
            catch (Exception)
            {


                throw;
            }

            return optionList;

        }

        //Retrieve Entity with all related Entities passed in relationships list of objects
        public Entity RetrieveEntityWithMultipleRelatedEntities(string EntityId, string EntityName, string[] Columns, List<RelationShipObject> relationShips)
        {
            try
            {
                RetrieveRequest retrieveRequest = new RetrieveRequest();
                retrieveRequest.Target = new EntityReference(EntityName, Guid.Parse(EntityId));


                ColumnSet attributes;
                if (Columns != null && Columns.Length > 0)
                    attributes = new ColumnSet(Columns);
                else
                    attributes = new ColumnSet() { AllColumns = true };

                retrieveRequest.ColumnSet = attributes;

                retrieveRequest.RelatedEntitiesQuery = new RelationshipQueryCollection();
                foreach (RelationShipObject relationship in relationShips)
                {
                    ColumnSet relationsShipAttributes;
                    if (relationship.RelationshipColumnset != null && relationship.RelationshipColumnset.Length > 0)
                        relationsShipAttributes = new ColumnSet(relationship.RelationshipColumnset);
                    else
                        relationsShipAttributes = new ColumnSet(true);
                    //The name of the relationship
                    retrieveRequest.RelatedEntitiesQuery.Add(new Relationship(relationship.RelationshipName),
                        //Name of the related entities
                        new QueryExpression(relationship.RelationshipEntity)
                        {

                            //The columns to retrieve for the related records

                            //ColumnSet = new ColumnSet(true),
                            ColumnSet = relationsShipAttributes
                            // Note that you can further filter the related records to be returned by
                            // specifying the Criteria property for the QueryExpression.
                        });

                }

                var response = (RetrieveResponse)ServiceProxy.Execute(retrieveRequest);
                var entity = (Entity)response.Entity;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Create Entity with All related entities passed in the obj paramater
        public Guid CreateEntityWithRelatedEntities(Entity newEntity, string entityPrimaryKey, List<RelationShipObject> obj)
        {
            try
            {
                newEntity.Id = Guid.Empty;
                newEntity.Attributes.Remove(entityPrimaryKey);

                foreach (RelationShipObject relationship in obj)
                {
                    for (int i = 0; i < newEntity.RelatedEntities[new Relationship(relationship.RelationshipName)].Entities.Count; i++)
                    {
                        newEntity.RelatedEntities[new Relationship(relationship.RelationshipName)].Entities[i].Attributes.Remove(relationship.RelationshipPrimaryKey);
                        newEntity.RelatedEntities[new Relationship(relationship.RelationshipName)].Entities[i].Id = Guid.Empty;
                    }
                }


                Guid NeWEntityGuid = OrganizationService.Create(newEntity);
                return NeWEntityGuid;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get Entity Guid for an attribute value
        /// </summary>
        /// <param name="EntityLogicalName"></param>
        /// <param name="AttributeKey"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        public Guid GetEntityGuidByAttribute(string EntityLogicalName, string AttributeKey, object AttributeValue) //Code Review : Move it CRM access layer and update try & catch
        {
            try
            {
                QueryExpression query = new QueryExpression(EntityLogicalName);
                query.ColumnSet = new ColumnSet(false);
                query.Criteria = new FilterExpression();
                query.Criteria.Conditions.Add(new ConditionExpression(AttributeKey, ConditionOperator.Equal, AttributeValue));

                var collection = OrganizationService.RetrieveMultiple(query);
                return collection.Entities.Count > 0 ? collection.Entities[0].Id : Guid.Empty;
                //return collection?.Count > 0 ? collection.FirstOrDefault().Id : Guid.Empty;
            }
            catch (Exception)
            {


                throw;
            }
        }
        public class RelationShipObject
        {
            public string RelationshipName { get; set; }
            public string RelationshipEntity { get; set; }
            public string[] RelationshipColumnset { get; set; }
            public string RelationshipPrimaryKey { get; set; }
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            try
            {
                OrganizationResponse response = OrganizationService.Execute(request);
                return response;
            }

              catch (Exception Err)
            {
                Logger.Log(Err);
                throw;
            }
        }


    }
}
