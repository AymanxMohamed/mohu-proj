using LinkDev.Common.Crm.Cs.Base;
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
    public class TryToSetTextField : CustomStepBase
    {
        [Input("Entity Id")]
        [RequiredArgument]
        public InArgument<string> EntityId { get; set; }

        [Input("Entity Logical Name")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalName { get; set; }

        [Input("Field Logical Name")]
        [RequiredArgument]
        public InArgument<string> FieldLogicalName { get; set; }

        [Input("Field Value")]
        [RequiredArgument]
        public InArgument<string> FieldValue { get; set; }

        public override void ExtendedExecute()
        {
            try
            {
                var entity = new Entity(EntityLogicalName.Get(ExecutionContext));
                entity.Id = new Guid(EntityId.Get(ExecutionContext));
                entity[FieldLogicalName.Get(ExecutionContext)] = FieldValue.Get(ExecutionContext);

                OrganizationService.Update(entity);
            }
            catch (Exception e)
            {
                Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"{e.Message}", Logger.SeverityLevel.Warning);
            }
        }
    }
}
