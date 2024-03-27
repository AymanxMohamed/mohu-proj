using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GetFileLocationID : CustomStepBase
    {
        [Input("Document Setting ID")]
        [RequiredArgument]
        public InArgument<string> DocumentSettingID { get; set; }

        [Input("Regarding ID")]
        [RequiredArgument]
        public InArgument<string> RegardingID { get; set; }

        [Output("File Location")]
        [ReferenceTarget("ldv_filelocation")]
        [RequiredArgument]
        public OutArgument<EntityReference> FileLocation { get; set; }
        public override void ExtendedExecute()
        {
            string fetchXML = @"<fetch>
                                  <entity name='ldv_filelocation'>
                                    <attribute name='activityid' />
                                    <filter>
                                      <condition attribute='regardingobjectid' operator='eq' value='" + RegardingID.Get(ExecutionContext) + @"' />
                                      <condition attribute='ldv_documentsettingid' operator='eq' value='" + DocumentSettingID.Get(ExecutionContext) + @"' />
                                    </filter>
                                  </entity>
                                </fetch>";
            EntityCollection entityCollection = OrganizationService.RetrieveMultiple(new FetchExpression(fetchXML));
            if (entityCollection.Entities.Count > 0)
            {
                FileLocation.Set(ExecutionContext, entityCollection.Entities[0].ToEntityReference());
            }
            else
            {
                FileLocation.Set(ExecutionContext, null);
            }
        }
    }
}
