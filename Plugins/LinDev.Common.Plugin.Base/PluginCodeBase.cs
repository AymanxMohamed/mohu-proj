
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Maan.Common.Crm.Plugin.Base
{
    public abstract class PluginCodeBase : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Extract the tracing service for use in debugging sandboxed plug-ins.
            // If you are not registering the plug-in in the sandbox, then you do
            // not have to add any tracing service related code.
            ITracingService tracingService =
                    (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
            IPluginExecutionContext Context =
              (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the organization service reference which you will need for
            // web service calls.
            var serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService OrganizationService = serviceFactory.CreateOrganizationService(Context.UserId);

            //CrmConfigurationKeys = Configurations.GetConfiguration(OrganizationService);

            // Default till getting true value
            string LanguageCode = "1033";

            //try
            //{
            tracingService.Trace($"Started with {nameof(Context.PrimaryEntityName)}: '{Context.PrimaryEntityName}', {nameof(Context.PrimaryEntityId)}: '{Context.PrimaryEntityId}'");

            ExtendedExecute(OrganizationService, Context, LanguageCode, tracingService);
            //}
            //catch (Exception exception)
            //{
            //    tracingService.Trace(exception.Message);
            //    throw new InvalidPluginExecutionException(exception.Message);
            //}
            //finally
            //{
            //    tracingService.Trace($"Finished");
            //}
        }

        public abstract void ExtendedExecute(IOrganizationService OrganizationService, IPluginExecutionContext Context, string LanguageCode, ITracingService tracingService);
    }
}
