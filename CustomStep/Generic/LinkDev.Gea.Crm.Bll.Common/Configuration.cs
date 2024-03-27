using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Gea.Crm.Bll.Common
{
    public static class Configurations
    {
        const string ldv_configurationEntityLogicalName = "ldv_configuration";
        const string ldv_name = "ldv_name";
        const string ldv_value = "ldv_value";

        static Dictionary<string, string> _configurations = new Dictionary<string, string>();
        static readonly object retrievingConfigurationsLock = new object();

        public static ConfigurationKeys GetConfiguration(IOrganizationService organizationService)
        {
            // lock in case multiple codes tried to retrieve 
            // configurations
            lock (retrievingConfigurationsLock)
            {
                if (_configurations == null || _configurations.Count <= 0)
                {
                    var query = new QueryExpression()
                    {
                        NoLock = true,
                        EntityName = ldv_configurationEntityLogicalName,
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression()
                        {
                            Conditions =
                        {
                            new ConditionExpression(ldv_name,ConditionOperator.NotNull),
                            new ConditionExpression(ldv_value,ConditionOperator.NotNull)
                        }
                        }
                    };

                    var retrievedConfigurations = organizationService.RetrieveMultiple(query);

                    foreach (var item in retrievedConfigurations.Entities)
                    {
                        if (_configurations.ContainsKey(item[ldv_name].ToString()))
                            throw new Exception($"'{query.EntityName}' contains records with duplicate keys '{item[ldv_name]}', key must be unique");

                        _configurations.Add(item[ldv_name].ToString(), item[ldv_value].ToString());
                    }
                }
            }

            return GetConfigurationObject();
        }

        private static ConfigurationKeys GetConfigurationObject()
        {
            var configurationKeys = new ConfigurationKeys();

            var propertyInfos
                = typeof(ConfigurationKeys).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var item in propertyInfos)
            {
                KeyValuePair<string, string> value = new KeyValuePair<string, string>();
                foreach (var configItr in _configurations)
                {
                    if (configItr.Key == item.Name)
                    {
                        value = configItr;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(value.Key))
                {
                    // Guid type is failing wehn using Convert.ChangeType
                    if (item.PropertyType.FullName.ToLower().Equals(typeof(Guid).FullName.ToLower()))
                        item.SetValue(configurationKeys,  Guid.Parse(value.Value));
                    else item.SetValue(configurationKeys, Convert.ChangeType(value.Value, item.PropertyType));
                }
                    
            }

            return configurationKeys;
        }
        public static string RetriveGeaConfiguration(string configurationName, IOrganizationService organizationService)
        {
            if (configurationName == null && configurationName == string.Empty)
                return null;
            var configuration = new QueryExpression("ldv_configuration");
            configuration.ColumnSet.AddColumns("ldv_value");
            configuration.Criteria.AddCondition("ldv_name", ConditionOperator.Equal, configurationName);
            EntityCollection result = organizationService.RetrieveMultiple(configuration);
            if (result.Entities.Count == 0)
                return null;
            else
            {
                string value = result[0].Attributes.Contains("ldv_value") ? (string)(result[0].Attributes["ldv_value"]) : string.Empty;
                if (value == string.Empty) return null;
                else
                {
                    return value;
                }
            }
        }
    }
}
