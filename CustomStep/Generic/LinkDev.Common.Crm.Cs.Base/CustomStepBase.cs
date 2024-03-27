using LinkDev.Common.Crm.Logger;
using LinkDev.Gea.Crm.Bll.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

namespace LinkDev.Common.Crm.Cs.Base
{
    public abstract class CustomStepBase : CodeActivity
    {
        protected internal ILogger Tracer { get; private set; }
        protected internal IOrganizationService OrganizationService { get; set; }
        protected internal IWorkflowContext Context { get; private set; }
        protected internal CodeActivityContext ExecutionContext { get; private set; }
        protected internal ConfigurationKeys CrmConfigurationKeys { get; private set; }
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

            CrmConfigurationKeys = Configurations.GetConfiguration(OrganizationService);

            Tracer = new LoggerHandler(
                CrmConfigurationKeys.EnableSystemLoggingInfoBoolen,
                CrmConfigurationKeys.EnableSystemLoggingWarningsBoolean,
                CrmConfigurationKeys.EnableSystemLoggingErrorsBoolean, OrganizationService);

            try
            {
                Tracer.LogComment(this.GetType().FullName, $"Started with {nameof(Context.PrimaryEntityName)}: '{Context.PrimaryEntityName}', {nameof(Context.PrimaryEntityId)}: '{Context.PrimaryEntityId}', '{nameof(Context.UserId)}': '{Context.UserId}', '{nameof(Context.InitiatingUserId)}': '{Context.InitiatingUserId}'", SeverityLevel.Info);

                var inputParamsLog = LogInputParameters();
                if (!string.IsNullOrEmpty(inputParamsLog)) Tracer.LogComment(this.GetType().FullName, $"Input Parameters\r\n{inputParamsLog}", Logger.SeverityLevel.Info);

                LanguageCode = GetUserLanguage();

                Tracer.LogComment(this.GetType().FullName, $"User Language '{LanguageCode}'", Logger.SeverityLevel.Info);

                ExtendedExecute();

                var outputParamsLog = LogOutputParameters();
                if (!string.IsNullOrEmpty(outputParamsLog)) Tracer.LogComment(this.GetType().FullName, $"Output Parameters\r\n{outputParamsLog}", Logger.SeverityLevel.Info);

                Tracer.LogComment(this.GetType().FullName, "Finish ExtendedExecute", Logger.SeverityLevel.Info);
            }
            catch (System.Exception exception)
            {
                Tracer.LogException(LoggerHandler.GetMethodFullName(), exception);
                throw new InvalidPluginExecutionException(exception.Message);
            }
            finally
            {
                Tracer.LogComment(this.GetType().FullName, $"Finished", Logger.SeverityLevel.Info);
                tracingService.Trace(Tracer.ToString());
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
            catch (System.Exception exception)
            {
                defaultLanguageCode = "1025";
                Tracer.LogComment(this.GetType().FullName, $"GetUserLanguage: {exception.Message}", Logger.SeverityLevel.Error);
            }

            return defaultLanguageCode;
        }


        public string GetSpecificUserLanguage(EntityReference User)
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
                        new ConditionExpression("systemuserid", ConditionOperator.Equal, User.Id)
                        }
                    }
                }).Entities.FirstOrDefault();

                if (userSettings.Contains("uilanguageid") && userSettings.GetAttributeValue<int>("uilanguageid") != 0)
                {
                    defaultLanguageCode = userSettings.GetAttributeValue<int>("uilanguageid").ToString();
                }
            }
            catch (System.Exception exception)
            {
                defaultLanguageCode = "1025";
                Tracer.LogComment(this.GetType().FullName, $"GetSpecificUserLanguage: {exception.Message}", Logger.SeverityLevel.Error);
            }

            return defaultLanguageCode;
        }

        private string LogInputParameters()
        {
            var log = "";
            try
            {

                var propertyDict = this.GetType().GetProperties();
                for (int i = 0; i < propertyDict.Length; i++)
                {
                    var item = propertyDict[i];

                    if (item.PropertyType.BaseType != typeof(InArgument))
                        continue;

                    if (!string.IsNullOrEmpty(log))
                        log += ", ";

                    log += $"'{item.Name}': ";

                    if (item.PropertyType == typeof(InArgument<EntityReference>))
                    {
                        var value = item.GetValue(this) as InArgument<EntityReference>;
                        log += value.Get(ExecutionContext) == null ? "''" : $"LogicalName: '{value.Get(ExecutionContext).LogicalName}' & Id: '{value.Get(ExecutionContext).Id}'";
                    }
                    else if (item.PropertyType == typeof(InArgument<OptionSetValue>))
                    {
                        var value = item.GetValue(this) as InArgument<OptionSetValue>;
                        log += value.Get(ExecutionContext) == null ? "''" : $"'{value.Get(ExecutionContext).Value}'";

                    }
                    else if (item.PropertyType == typeof(InArgument<Money>))
                    {
                        var value = item.GetValue(this) as InArgument<Money>;
                        log += value.Get(ExecutionContext) == null ? "''" : $"'{value.Get(ExecutionContext).Value}'";
                    }
                    else
                    {
                        var value = item.GetValue(this) as InArgument;
                        log += value.Get(ExecutionContext) == null ? "''" : $"'{value.Get(ExecutionContext).ToString()}'";
                    }


                }
            }
            catch (System.Exception exception)
            {
                Tracer.LogComment(LoggerHandler.GetMethodFullName(), exception.Message, Logger.SeverityLevel.Error);

            }

            return log;

        }

        private string LogOutputParameters()
        {
            var log = "";
            try
            {

                var propertyDict = this.GetType().GetProperties();
                for (int i = 0; i < propertyDict.Length; i++)
                {
                    var item = propertyDict[i];

                    if (item.PropertyType.BaseType != typeof(OutArgument))
                        continue;

                    if (!string.IsNullOrEmpty(log))
                        log += ", ";

                    log += $"'{item.Name}': ";

                    if (item.PropertyType == typeof(OutArgument<EntityReference>))
                    {
                        var value = item.GetValue(this) as OutArgument<EntityReference>;
                        log += value.Get(ExecutionContext) == null ? "''" : $"LogicalName: '{value.Get(ExecutionContext).LogicalName}' & Id: '{value.Get(ExecutionContext).Id}'";
                    }
                    else if (item.PropertyType == typeof(OutArgument<OptionSetValue>))
                    {
                        var value = item.GetValue(this) as OutArgument<OptionSetValue>;
                        log += value.Get(ExecutionContext) == null ? "''" : $"'{value.Get(ExecutionContext).Value}'";

                    }
                    else if (item.PropertyType == typeof(OutArgument<Money>))
                    {
                        var value = item.GetValue(this) as OutArgument<Money>;
                        log += value.Get(ExecutionContext) == null ? "''" : $"'{value.Get(ExecutionContext).Value}'";
                    }
                    else
                    {
                        var value = item.GetValue(this) as OutArgument;
                        log += value.Get(ExecutionContext) == null ? "''" : $"'{value.Get(ExecutionContext).ToString()}'";
                    }


                }
            }
            catch (System.Exception exception)
            {
                Tracer.LogComment(LoggerHandler.GetMethodFullName(), exception.Message, Logger.SeverityLevel.Error);

            }

            return log;

        }

        public abstract void ExtendedExecute();


    }
}
