using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class TryToSetState : CustomStepBase
    {
        [Input("Entity Id")]
        [RequiredArgument]
        public InArgument<string> EntityId { get; set; }

        [Input("Entity Logical Name")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalName { get; set; }

        [Input("State Code Value")]
        [RequiredArgument]
        public InArgument<int> StateCodeValue { get; set; }

        [Input("Status Code value")]
        [RequiredArgument]
        public InArgument<int> StatusCodeValue { get; set; }

        public override void ExtendedExecute()
        {
            try
            {
                var setStateRequest = new SetStateRequest();
                setStateRequest.State = new OptionSetValue(StateCodeValue.Get(ExecutionContext));
                setStateRequest.Status = new OptionSetValue(StatusCodeValue.Get(ExecutionContext));
                setStateRequest.EntityMoniker = new EntityReference(EntityLogicalName.Get(ExecutionContext), new Guid(EntityId.Get(ExecutionContext)));
                var stateSet = (SetStateResponse)OrganizationService.Execute(setStateRequest);
            }
            catch (Exception e)
            {
                Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"{e.Message}", Logger.SeverityLevel.Warning);
            }
        }
    }
}
