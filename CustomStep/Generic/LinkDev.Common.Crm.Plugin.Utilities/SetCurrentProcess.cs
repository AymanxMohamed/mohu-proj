using LinkDev.Common.Crm.Plugin.Base;
using LinkDev.Common.Crm.Utilities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Plugin.Utilities
{
    public class SetCurrentProcess : PluginBase
    {
        public override void ExtendedExecute()
        {
            if (Context.InputParameters.Contains("Target") && Context.InputParameters["Target"] is Entity)
            {

                var entity = (Entity)Context.InputParameters["Target"];

                Tools.SetCurrentProcess(
                    Tracer,
                    entity,
                    OrganizationService);
            }
        }
    }
}
