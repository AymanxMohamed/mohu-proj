using LinkDev.Common.Crm.Logger;
using LinkDev.Common.Crm.Plugin.Base;
using LinkDev.Common.Crm.Utilities;
using Microsoft.Xrm.Sdk;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace LinkDev.Common.Crm.Plugin.Utilities
{
    public class SetServiceSetting : PluginBase
    {
        private readonly Crm.Utilities.ServiceSettingEntityConditions _conditions;
        public SetServiceSetting(string unsecureString)
        {

            if (String.IsNullOrWhiteSpace(unsecureString))
            {
                throw new InvalidPluginExecutionException("Input is required for this plugin to be executed.");
            }

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(unsecureString)))
            {
                var deserializer = new DataContractJsonSerializer(typeof(ServiceSettingEntityConditions), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true });
                _conditions = (ServiceSettingEntityConditions)deserializer.ReadObject(ms);
            }

            ValidateConditionsJson(_conditions);
        }

        private void ValidateConditionsJson(Crm.Utilities.ServiceSettingEntityConditions conditions)
        {
            if (string.IsNullOrWhiteSpace(conditions.DependentFieldName) && conditions.DependentFieldType != 0)
                throw new InvalidPluginExecutionException("Dependent field name cannot be null.");

            if (conditions.DependentFieldType == 0 && !string.IsNullOrWhiteSpace(conditions.DependentFieldName))
                throw new InvalidPluginExecutionException("Dependent field value cannot be null.");

            if (string.IsNullOrWhiteSpace(conditions.ServiceSettingFieldName))
                throw new InvalidPluginExecutionException("Service field name cannot be null.");

            if (conditions.ServiceSettingConditions == null || conditions.ServiceSettingConditions.Count < 1)
                throw new InvalidPluginExecutionException("Service setting conditions cannot be null.");
        }

        public override void ExtendedExecute()
        {
            if (Context.InputParameters.Contains("Target") && Context.InputParameters["Target"] is Entity)
            {
                LogInputs();

                var entity = (Entity)Context.InputParameters["Target"];

                Tools.SetServiceSetting(
                    Tracer,
                    entity,
                    _conditions, OrganizationService);
            }
        }

        private void LogInputs()
        {
            Tracer.LogComment(
                LoggerHandler.GetMethodFullName(),
                $"ServiceSettingEntityConditions => ",
                Logger.SeverityLevel.Info);

            Tracer.LogComment(
                LoggerHandler.GetMethodFullName(),
                $"DependentFieldName: {_conditions.DependentFieldName} ",
                Logger.SeverityLevel.Info);

            Tracer.LogComment(
                LoggerHandler.GetMethodFullName(),
                $"DependentFieldType: {_conditions.DependentFieldType} ",
                Logger.SeverityLevel.Info);

            Tracer.LogComment(
                LoggerHandler.GetMethodFullName(),
                $"ServiceSettingFieldName: {_conditions.ServiceSettingFieldName} ",
                Logger.SeverityLevel.Info);

            foreach (var item in _conditions.ServiceSettingConditions)
            {
                Tracer.LogComment(
                    LoggerHandler.GetMethodFullName(),
                    $"key: {item.Key}, value: {item.Value}",
                    Logger.SeverityLevel.Info);
            }
        }
    }
}
