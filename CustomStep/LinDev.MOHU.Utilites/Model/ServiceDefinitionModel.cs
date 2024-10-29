using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites.Model
{
    public static class ServiceDefinitionModel
    {
        public static string EntitySchemaName { get; } = "ldv_service";
        public static string ID { get; } = "ldv_serviceid";
        public static string ServiceConfigrationFields { get; } = "ldv_serviceconfigurationfields";
    }
}
