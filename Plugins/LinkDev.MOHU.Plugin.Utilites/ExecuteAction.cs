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
    public class ExecuteAction : IPlugin
    {
        ITracingService tracingService;
        IOrganizationService service;
        private readonly string UnsecureConfiguration;

        public ExecuteAction(string unsecureString)
        {
            if (String.IsNullOrWhiteSpace(unsecureString))
            {
                throw new InvalidPluginExecutionException("Unsecure string are required for this plugin to execute.");
            }
            UnsecureConfiguration = unsecureString;
        }
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

            tracingService.Trace( $"in Execute ");

            if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
            {
                try
                {
                    // Obtain the target entity from the input parameters.
                    Entity targetEntity = (Entity)context.InputParameters["Target"];
                    tracingService.Trace( $"  targetEntity logical name{targetEntity.LogicalName} " );
                    String[] workflowsid = UnsecureConfiguration.Split(',');
                    tracingService.Trace($"Number of workflows Id found in UnsecureConfiguration '{workflowsid.Length}'" );
                    if (workflowsid != null && workflowsid.Count() > 0)
                    {
                        foreach (string workflowid in workflowsid)
                        {
                            tracingService.Trace("workflow id:" + Guid.Parse(workflowid) );
                            tracingService.Trace("target entity:" + targetEntity.Id );
                            ExecuteWorkflowRequest request = new ExecuteWorkflowRequest()
                            {
                                WorkflowId = Guid.Parse(workflowid),
                                EntityId = targetEntity.Id,
                            };
                            tracingService.Trace("request.RequestId:" + request.RequestId );                                      
                            // Execute the workflow.
                            ExecuteWorkflowResponse response =
                            (ExecuteWorkflowResponse)service.Execute(request);
                            tracingService.Trace("response id:" + response.Id );
                        }
                    }
                    else
                    {
                        tracingService.Trace($"workflowsid Number  =0 or null  " );
                    }
                }
                catch (Exception Ex)
                {
                    throw new NotImplementedException(Ex.Message);
                }
            }
        }
    }
}