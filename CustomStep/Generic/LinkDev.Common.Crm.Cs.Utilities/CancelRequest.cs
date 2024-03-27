using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Crm.Sdk.Messages;
using LinkDev.Common.Crm.Cs.Base;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class CancelRequest : CustomStepBase
    {

        #region Arguments
        [Input("Related Application Id")]
        [RequiredArgument]
        public InArgument<string> RelatedApplicationId { get; set; }

        [Input("Entity Logical Name")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalName { get; set; }


        [Input("State code logical name")]
        [Default(" ")]
        [RequiredArgument]
        public InArgument<string> StateCodeLogicalName { get; set; }


        [Input("State code value")]
        [Default(" ")]
        [RequiredArgument]
        public InArgument<string> StateCodeValue { get; set; }

        [Input("service status")]
        [ReferenceTarget("ldv_servicestatus")]
        public InArgument<EntityReference> ServiceStatusCancelValue { get; set; }

        [Input("Service Status logical name")]
        [Default(" ")]
        public InArgument<string> ServiceStatusLogicalName { get; set; }
        #endregion

        public override void ExtendedExecute()
        {

            var RelatedApplicationRecordId =
                Guid.Parse(RelatedApplicationId.Get(ExecutionContext).ToString());

            var CanceledEntityLogicalName =
                EntityLogicalName.Get(ExecutionContext).ToString();

            var serviceStatusLogicalName =
                ServiceStatusLogicalName.Get(ExecutionContext);

            var serviceStatusCancelValue =
                ServiceStatusCancelValue.Get(ExecutionContext);


            var applicationHeaderEntity =
                OrganizationService.Retrieve(
                    Context.PrimaryEntityName,
                    Context.PrimaryEntityId,
                    new Microsoft.Xrm.Sdk.Query.ColumnSet(
                        new string[] {
                                    "ldv_relatedapplicationid",
                                    "ldv_services",
                                    "ldv_servicestatus" }));


            var CanceledEntity =
                OrganizationService.Retrieve(
                    CanceledEntityLogicalName,
                    RelatedApplicationRecordId,
                    new Microsoft.Xrm.Sdk.Query.ColumnSet(
                        new string[] {
                                    serviceStatusLogicalName }));

            if (CanceledEntity == null)
            {
                throw new Exception($"canceled Entity not found.");
            }
            else
            {
                //check if entity record is draft
                var ApplicationHeaderStatus = applicationHeaderEntity.Attributes["ldv_servicestatus"].ToString();
                var EntityRecordStatus = CanceledEntity.Attributes[serviceStatusLogicalName].ToString();

                if (EntityRecordStatus != ApplicationHeaderStatus)
                {
                    throw new Exception($"Application header status not updated the the Entity service status,please fix this and try again.");
                }

                //cancel related entity


                var entity = new Entity(CanceledEntityLogicalName)
                {
                    Id = RelatedApplicationRecordId,
                    Attributes =
                    {
                        new KeyValuePair<string, object>(serviceStatusLogicalName,new EntityReference(serviceStatusLogicalName, serviceStatusCancelValue.Id))
                    }
                };

                OrganizationService.Update(entity);

                int CanceledId = Convert.ToInt32(StateCodeValue.Get(ExecutionContext));
                SetStateRequest state = new SetStateRequest();
                // Set the Request Object's Properties
                state.State = new OptionSetValue(1);
                state.Status = new OptionSetValue(CanceledId);
                // Point the Request to the case whose state is being changed
                state.EntityMoniker = new EntityReference(entity.LogicalName, entity.Id);
                // Execute the Request
                SetStateResponse stateSet = (SetStateResponse)OrganizationService.Execute(state);


                //cancel application header
                entity = new Entity(Context.PrimaryEntityName);
                entity.Id = Context.PrimaryEntityId;
                entity.Attributes.Add("ldv_servicestatus", ServiceStatusCancelValue.Get(ExecutionContext));
                OrganizationService.Update(entity);
            }
        }
    }
}
