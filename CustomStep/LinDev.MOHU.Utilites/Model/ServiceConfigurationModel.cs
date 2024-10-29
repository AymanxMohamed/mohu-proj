using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites.Model
{
    public static class ServiceConfigurationModel
    {
        public static string EntitySchemaName { get; } = "ldv_serviceconfigration";
        public static string StageConfigration { get; } = "ldv_stageconfigurationid";
        public static string StatusReason { get; } = "ldv_substatusid";
        public static string PortalStatus { get; } = "ldv_portalstatusid";
        public static string Team { get; } = "ldv_teamid";
    }
}
