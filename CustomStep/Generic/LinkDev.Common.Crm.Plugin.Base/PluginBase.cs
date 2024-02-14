using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Logger;
using System.Diagnostics;
using Microsoft.Xrm.Sdk.Query;
using LinkDev.Gea.Crm.Bll.Common;

namespace LinkDev.Common.Crm.Plugin.Base
{
    public abstract class PluginBase : IPlugin
    {
        protected internal ConfigurationKeys CrmConfigurationKeys { get; private set; }
        protected internal ILogger Tracer { get; private set; }
        protected internal IOrganizationService OrganizationService { get; private set; }
        protected internal IPluginExecutionContext Context { get; private set; }
        protected internal string LanguageCode { get; private set; }
        public ITracingService TracingService { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            // Extract the tracing service for use in debugging sandboxed plug-ins.
            // If you are not registering the plug-in in the sandbox, then you do
            // not have to add any tracing service related code.
            TracingService =
                    (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
              Context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the organization service reference which you will need for
            // web service calls.
            var serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            OrganizationService = serviceFactory.CreateOrganizationService(Context.UserId);

            CrmConfigurationKeys = Configurations.GetConfiguration(OrganizationService);

            Tracer = new LoggerHandler(
                CrmConfigurationKeys.EnableSystemLoggingInfoBoolen,
                CrmConfigurationKeys.EnableSystemLoggingWarningsBoolean,
                CrmConfigurationKeys.EnableSystemLoggingErrorsBoolean, OrganizationService);

            try
            {
                Tracer.LogComment(this.GetType().FullName, $"Started with {nameof(Context.PrimaryEntityName)}: '{Context.PrimaryEntityName}', {nameof(Context.PrimaryEntityId)}: '{Context.PrimaryEntityId}'", Logger.SeverityLevel.Info);

                LanguageCode = GetUserLanguage();

                Tracer.LogComment(this.GetType().FullName, $"User Language '{LanguageCode}'", Logger.SeverityLevel.Info);

                ExtendedExecute();

                Tracer.LogComment(this.GetType().FullName, "Finish ExtendedExecute", Logger.SeverityLevel.Info);
            }
            catch (Exception exception)
            {
                Tracer.LogException(LoggerHandler.GetMethodFullName(), exception);
                throw new InvalidPluginExecutionException(exception.Message);
            }
            finally
            {
                Tracer.LogComment(this.GetType().FullName, $"Finished", Logger.SeverityLevel.Info);
                TracingService.Trace(Tracer.ToString());
                Tracer.FlushLogs();
            }
        }
        private string GetUserLanguage()
        {
            var defaultLanguageCode = "1025";
            try
            {
                Entity userSettings = OrganizationService.RetrieveMultiple(

                new QueryExpression("usersettings")
                {
                    ColumnSet = new ColumnSet("uilanguageid"),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                        new ConditionExpression("systemuserid", ConditionOperator.Equal, Context.UserId)
                        }
                    }
                }).Entities.FirstOrDefault();

                if (userSettings.Contains("uilanguageid") && userSettings.GetAttributeValue<int>("uilanguageid") != 0)
                {
                    defaultLanguageCode = userSettings.GetAttributeValue<int>("uilanguageid").ToString();
                }
            }
            catch (Exception exception)
            {
                Tracer.LogComment(this.GetType().FullName, $"GetUserLanguage: {exception.Message}", Logger.SeverityLevel.Error);
                defaultLanguageCode = "1025";
            }

            return defaultLanguageCode;
        }

        public abstract void ExtendedExecute();
    }
}
