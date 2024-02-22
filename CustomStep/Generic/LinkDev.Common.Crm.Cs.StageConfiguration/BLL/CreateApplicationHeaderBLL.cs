using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
using LinkDev.CRM.Library.DAL;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.BLL
{   
    public class CreateApplicationHeaderBLL
    {
        private IOrganizationService OrganizationService;
        private CRMAccessLayer crmAccess;
        ITracingService tracingService;
        public CreateApplicationHeaderBLL(IOrganizationService service, ITracingService tracingServices)
        {
            OrganizationService = service;
            crmAccess = new CRMAccessLayer(OrganizationService);
            tracingService = tracingServices;
        }
        public EntityReference CreateAppHeaderFromRequest(Entity target)
        {
            Guid applicationHeaderId = Guid.Empty;
            try
            {
                if (target == null || target.Id == Guid.Empty) return null;
                Entity targetEntity = crmAccess.RetrieveEntity(target.Id, target.LogicalName, new string[] { });
                if (targetEntity != null)
                {
                    if (targetEntity.Contains(RequestEntity.ApplicationHeader))
                    {
                        return new EntityReference(ApplicationHeaderEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.ApplicationHeader]).Id);
                    }
                    Entity newApplicationHeader = new Entity(ApplicationHeaderEntity.LogicalName);
                    //adding  application id to application header 
                    newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Regarding, targetEntity.ToEntityReference());

                    if (targetEntity.LogicalName == IncidentEntity.LogicalName)
                    {
                        tracingService.Trace(" Incident Entity ");
                        if (targetEntity.Attributes.Contains(RequestEntity.Customer))
                        {
                            tracingService.Trace($" LogicalName { ( (EntityReference)targetEntity.Attributes[RequestEntity.Customer]).LogicalName} , Id { ((EntityReference)targetEntity.Attributes[RequestEntity.Customer]).Id } ");
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Customer, new EntityReference( ( (EntityReference)targetEntity.Attributes[RequestEntity.Customer]).LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.Customer]).Id));
                        }
                       
                        if (((EntityReference)targetEntity.Attributes[RequestEntity.Customer]).LogicalName == "account")
                        {
                            tracingService.Trace(" account ");
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Account, new EntityReference(ContactEntity.LogicalName, ((EntityReference)targetEntity.Attributes["customerid"]).Id));
                        }
                        //adding  contact of the Request to application header 

                        else if (((EntityReference)targetEntity.Attributes[RequestEntity.Customer]).LogicalName == "contact")
                        {
                            tracingService.Trace(" contact ");

                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, new EntityReference(ContactEntity.LogicalName, ((EntityReference)targetEntity.Attributes["customerid"]).Id));
                        }
                        //    //adding  name of the Request to application header 
                        if (targetEntity.Attributes.Contains(RequestEntity.Name))
                        {
                            newApplicationHeader.Attributes.Add("subject", targetEntity.Attributes[RequestEntity.Name]);
                        }
                    }





                    //adding  service to application header 
                    if (targetEntity.Attributes.Contains(RequestEntity.Service))
                    {
                        newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Service, new EntityReference(ServiceDefinitionEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.Service]).Id));
                    }
                    //adding  service Status to application header 
                    //if (targetEntity.Attributes.Contains(RequestEntity.ServiceStatus))
                    //{
                    //    newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.ServiceStatus, new EntityReference(ServiceStatusEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.ServiceStatus]).Id));
                    //}
                    //adding  service Sub Status to application header 
                    if (targetEntity.Attributes.Contains(RequestEntity.ServiceSubStatus))
                    {
                        newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.ServiceSubStatus, new EntityReference(ServiceSubStatusEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.ServiceSubStatus]).Id));
                    }
                    //adding  Portal Status to application header 
                    if (targetEntity.Attributes.Contains(RequestEntity.PortaServiceSubStatus))
                    {
                        newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.PortalStatus, new EntityReference(ServiceSubStatusEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.PortaServiceSubStatus]).Id));
                    }

               

                    // create application header
                    applicationHeaderId = crmAccess.CreateEntity(newApplicationHeader);
                    tracingService.Trace(" Created  app header ");

                    if (applicationHeaderId != Guid.Empty)
                    {
                        Entity requestEntity = new Entity(targetEntity.LogicalName, targetEntity.Id);
                        requestEntity.Attributes.Add(RequestEntity.ApplicationHeader, new EntityReference(ApplicationHeaderEntity.LogicalName, applicationHeaderId));
                        crmAccess.UpdateEntity(requestEntity);
                        tracingService.Trace(" Create  app header ");


                    }
                }
                return new EntityReference(ApplicationHeaderEntity.LogicalName, applicationHeaderId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
