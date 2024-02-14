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
        public CreateApplicationHeaderBLL(IOrganizationService service)
        {
            OrganizationService = service;
            crmAccess = new CRMAccessLayer(OrganizationService);
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


                    if (targetEntity.LogicalName.ToLower().Equals("msdyn_workorder"))
                    {
                        if (targetEntity.Attributes.Contains("msdyn_name"))
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Name, targetEntity.Attributes["msdyn_name"]);

                    }
                    else
                    {
                        if (targetEntity.Attributes.Contains(RequestEntity.Name))
                        {
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Name, targetEntity.Attributes[RequestEntity.Name]);
                        }
                        else if (targetEntity.Attributes.Contains(RequestEntity.RenwalName))
                        {
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Name, targetEntity.Attributes[RequestEntity.RenwalName]);
                        }
                    }



                    if (targetEntity.LogicalName == IncidentEntity.LogicalName)
                    {
                        //adding  contact of the Request to application header 
                        if (targetEntity.Attributes.Contains(IncidentEntity.Contact))
                        {
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, new EntityReference(ContactEntity.LogicalName, ((EntityReference)targetEntity.Attributes["customerid"]).Id));
                        }
                    }
                    else
                    //adding  contact of the Request to application header 
                    if (targetEntity.Attributes.Contains(RequestEntity.Contact))
                    {
                        newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, new EntityReference(ContactEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.Contact]).Id));
                    }

                    // adding for Violation Request in GEA 
                    if (targetEntity.LogicalName.ToLower().Equals("ldv_violationrequest"))
                    {
                        if (targetEntity.Attributes.Contains("ldv_offender"))
                        {
                            var Customer = (EntityReference)targetEntity["ldv_offender"];
                            if (Customer.LogicalName == AccountEntity.LogicalName)
                            {
                                newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Account, Customer);
                            }
                            else if (Customer.LogicalName == ContactEntity.LogicalName)
                            {
                                newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, Customer);
                            }
                        }
                    }

                    // adding for Contract Submission Request in GEA 
                    if (targetEntity.LogicalName.ToLower().Equals("ldv_contractsubmissionlanddepartment"))
                    {
                        if (targetEntity.Attributes.Contains("ldv_investor"))
                        {
                            var Customer = (EntityReference)targetEntity["ldv_investor"];
                            if (Customer.LogicalName == AccountEntity.LogicalName)
                            {
                                newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Account, Customer);
                                if (targetEntity.Attributes.Contains("ldv_representativeid"))
                                {
                                    var representativeCustomer = (EntityReference)targetEntity["ldv_representativeid"];
                                    newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, representativeCustomer);
                                }
                            }
                            else if (Customer.LogicalName == ContactEntity.LogicalName)
                            {
                                newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, Customer);
                            }
                        }
                    }

                    // adding for Contract Renewal in GEA 
                    if (targetEntity.LogicalName.ToLower().Equals("ldv_contractrenewalrequest") || targetEntity.LogicalName.ToLower().Equals("ldv_contractmodification"))
                    {
                        if (targetEntity.Attributes.Contains("ldv_investorid"))
                        {
                            var Customer = (EntityReference)targetEntity["ldv_investorid"];
                            if (Customer.LogicalName == AccountEntity.LogicalName)
                            {
                                newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Account, Customer);
                                if (targetEntity.Attributes.Contains("ldv_representativeid"))
                                {
                                    var representativeCustomer = (EntityReference)targetEntity["ldv_representativeid"];
                                    newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, representativeCustomer);
                                }
                            }
                            else if (Customer.LogicalName == ContactEntity.LogicalName)
                            {
                                newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Contact, Customer);
                            }

                        }
                    }

                    //adding  account to application header 

                    if (targetEntity.LogicalName.ToLower().Equals("msdyn_workorder"))
                    {
                        if (targetEntity.Attributes.Contains("msdyn_serviceaccount"))
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Account, new EntityReference(AccountEntity.LogicalName, ((EntityReference)targetEntity.Attributes["msdyn_serviceaccount"]).Id));

                    }
                    else
                    {
                        if (targetEntity.Attributes.Contains(RequestEntity.Account))
                        {
                            newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Account, new EntityReference(AccountEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.Account]).Id));
                        }
                    }


                    //adding  service to application header 
                    if (targetEntity.Attributes.Contains(RequestEntity.Service))
                    {
                        newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.Service, new EntityReference(ServiceDefinitionEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.Service]).Id));
                    }
                    //adding  service Status to application header 
                    if (targetEntity.Attributes.Contains(RequestEntity.ServiceStatus))
                    {
                        newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.ServiceStatus, new EntityReference(ServiceStatusEntity.LogicalName, ((EntityReference)targetEntity.Attributes[RequestEntity.ServiceStatus]).Id));
                    }
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

                    ////adding  Submission date to application header 
                    //if (targetEntity.Attributes.Contains(ApplicationHeaderEntity.SubmissionDate))
                    //{
                    //    newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.SubmissionDate, ((DateTime)targetEntity.Attributes[ApplicationHeaderEntity.SubmissionDate]));
                    //}
                    ////adding  modified on to application header 
                    //if (targetEntity.Attributes.Contains(RequestEntity.CreatedOn  ))
                    //{
                    //    newApplicationHeader.Attributes.Add(ApplicationHeaderEntity.LastModifiedDate, ((DateTime)targetEntity.Attributes[RequestEntity.CreatedOn]));
                    //}

                    // create application header
                    applicationHeaderId = crmAccess.CreateEntity(newApplicationHeader);
                    if (applicationHeaderId != Guid.Empty)
                    {
                        Entity requestEntity = new Entity(targetEntity.LogicalName, targetEntity.Id);
                        requestEntity.Attributes.Add(RequestEntity.ApplicationHeader, new EntityReference(ApplicationHeaderEntity.LogicalName, applicationHeaderId));
                        crmAccess.UpdateEntity(requestEntity);
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
