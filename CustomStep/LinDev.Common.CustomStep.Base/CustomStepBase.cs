using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Base
{
    public abstract class CustomStepBase : CodeActivity
    {
        protected internal IOrganizationService OrganizationService { get; private set; }
        protected internal IWorkflowContext Context { get; private set; }
        protected internal CodeActivityContext ExecutionContext { get; private set; }
        protected internal ITracingService tracingService { get; private set; }
        protected internal string LanguageCode { get; private set; }
        protected override void Execute(CodeActivityContext executionContext)
        {
            ExecutionContext = executionContext;

            // Extract the tracing service for use in debugging sandboxed plug-ins.
            // If you are not registering the plug-in in the sandbox, then you do
            // not have to add any tracing service related code.
            tracingService =
                    ExecutionContext.GetExtension<ITracingService>();

            // Obtain the execution context from the service provider.
            Context =
                ExecutionContext.GetExtension<IWorkflowContext>();

            // Obtain the organization service reference which you will need for
            // web service calls.
            var serviceFactory =
                ExecutionContext.GetExtension<IOrganizationServiceFactory>();

            OrganizationService = serviceFactory.CreateOrganizationService(Context.UserId);

            // Default till getting true value
            LanguageCode = "1033";


            tracingService.Trace($"Started with {nameof(Context.PrimaryEntityName)}: '{Context.PrimaryEntityName}', {nameof(Context.PrimaryEntityId)}: '{Context.PrimaryEntityId}'");
            ExtendedExecute();

            tracingService.Trace("Finish ExtendedExecute");

        }
        public abstract void ExtendedExecute();
    }
}

