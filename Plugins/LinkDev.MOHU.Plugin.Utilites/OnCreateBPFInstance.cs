using LinDev.MOHU.Utilites.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.MOHU.Plugin.Utilites
{
    public class OnCreateBPFInstance : IPlugin
    {
        ITracingService tracingService;
        IOrganizationService service;
        public void Execute(IServiceProvider serviceProvider)
        {
           tracingService =
            (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)
            serviceProvider.GetService(typeof(IPluginExecutionContext));
            // Obtain the organization service reference which you will need for
            // web service calls.
            IOrganizationServiceFactory serviceFactory =
            (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
              service = serviceFactory.CreateOrganizationService(context.UserId);
            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
            {
                try
                {
                    // Obtain the target entity from the input parameters.
                    Entity entity = (Entity)context.InputParameters["Target"];
                   
                    entity = new Entity("phonetocaseprocess", new Guid("a2487435-e8e9-ee11-904d-6045bda18d51"));
                   
                    EntityReference bpfProcess = null;
                    if (entity.LogicalName== "phonetocaseprocess")
                    {
                        bpfProcess = GetBPFFromService(entity.ToEntityReference());
                    }
                    UpdateRequestService(bpfProcess, entity.ToEntityReference());
                }
                catch (Exception Ex)
                {
                    throw new NotImplementedException(Ex.Message);
                }
            }
        }
        EntityReference GetBPFFromService(EntityReference request)
        {
            EntityReference BPF = null;

 
            // Instantiate QueryExpression query
            var query = new QueryExpression("ldv_service");
            query.Distinct = true;

            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns("ldv_serviceid", "ldv_name", "createdon");
            query.AddOrder("ldv_name", OrderType.Ascending);

            // Add link-entity ai
            var ai = query.AddLink("incident", "ldv_serviceid", "ldv_serviceid");
            ai.EntityAlias = "ai";

            // Add link-entity aj
            var aj = ai.AddLink("phonetocaseprocess", "incidentid", "incidentid");
            aj.EntityAlias = "aj";

            // Define filter aj.LinkCriteria
            aj.LinkCriteria.AddCondition("businessprocessflowinstanceid", ConditionOperator.Equal, request.Id);


            // Instantiate QueryExpression query
            //var query = new QueryExpression("ldv_service");
            //query.Distinct = true;
            //// Add columns to query.ColumnSet
            //query.ColumnSet.AddColumns("ldv_serviceid", "ldv_name", "createdon");
            //query.AddOrder("ldv_name", OrderType.Ascending);
            //// Add link-entity ac
            //var ac = query.AddLink("incident", "ldv_serviceid", "ldv_serviceid");
            //ac.EntityAlias = "ac";
            //// Add link-entity ad
            //var ad = ac.AddLink(request.LogicalName, "incidentid", "incidentid");
            //ad.EntityAlias = "ad";
            //// Define filter ad.LinkCriteria
            //ad.LinkCriteria.AddCondition("businessprocessflowinstanceid", ConditionOperator.Equal, request.Id);
            var entities =service.RetrieveMultiple(query);
            if (entities.Entities.Count>0)
            {
                var serviceId = entities.Entities[0].Id;
                Entity serviceValue = service.Retrieve (ServiceEntity.EntityLogicalName, serviceId, new ColumnSet(ServiceEntity.Process));
                BPF = serviceValue.GetAttributeValue<EntityReference>(ServiceEntity.Process);
            }
            return BPF;
        }

        void UpdateRequestService(EntityReference bpfProcess, EntityReference request)
        {
            Entity requestEntity = new Entity(request.LogicalName, request.Id);
            //requestEntity.Attributes.Add(RequestEntity.ProcessId, bpfProcess);


            SetProcessRequest setProcReq = new SetProcessRequest
            {
                Target = new EntityReference(request.LogicalName, request.Id),
                NewProcess = new EntityReference("workflow", bpfProcess.Id)
            };
            SetProcessResponse setProcResp = (SetProcessResponse)service.Execute(setProcReq);


            
             
            // Create the task in Microsoft Dynamics CRM.
            tracingService.Trace("FollowupPlugin: Update the BPF Instance.");
            //service.Update(requestEntity);
        }
    }
}