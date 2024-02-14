
using LinkDev.Common.Crm.Bll.Base;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
using LinkDev.Common.Crm.Logger;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration
{
    public class CreateTask : CustomStepBase
    {
        [RequiredArgument]
        [Input("Stage Configuration")]
        [ReferenceTarget("ldv_stageconfiguration")]
        public InArgument<EntityReference> StageConfiguration { get; set; }
        [RequiredArgument]
        [Input("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        public InArgument<EntityReference> ApplicationHeader { get; set; }
        [RequiredArgument]
        [Input("RequestId")]
        public InArgument<string> RequestId { get; set; }

        [RequiredArgument]
        [Input("RequestSchemaName")]
        public InArgument<string> RequestSchemaName { get; set; }

        [Output("Current Task")]
        [ReferenceTarget("task")]
        public OutArgument<EntityReference> Task { get; set; }

        public override void ExtendedExecute()
        {
            EntityReference task = null;
            var bll = new CreateTaskBLL(OrganizationService, Tracer, LanguageCode);
            var createdTask = bll.CreateTask(StageConfiguration.Get(ExecutionContext), RequestId.Get(ExecutionContext), RequestSchemaName.Get(ExecutionContext), ApplicationHeader.Get(ExecutionContext));

            if (createdTask?.Id != Guid.Empty)
            {
                task = createdTask;
            }

            Task.Set(ExecutionContext, task);
        }
    }
}
