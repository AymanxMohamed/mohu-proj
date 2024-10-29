using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites.Model
{
    public static class CaseModel
    {
        public static string EntitySchemaName { get; } = "incident";
        public static string StatusReason { get; } = "ldv_substatusid";
        public static string PortalStatus { get; } = "ldv_portalstatusid";
    }
}
