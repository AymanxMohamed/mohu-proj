using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GetMulitiSelectValues : CustomStepBase
    {

        #region "Input Parameters"

        [Input("Target Field Schema Name")]
        [RequiredArgument]
        public InArgument<string> targetFieldSchemaName { get; set; }

        [Output("Values")]
        [RequiredArgument]
        public OutArgument<string> Values { get; set; }

        #endregion

        public override void ExtendedExecute()
        {
            string TargetFieldSchemaName =
               targetFieldSchemaName.Get(ExecutionContext).ToString();
            string output = string.Empty;

            var RetreivedEntity =
                OrganizationService.Retrieve(
                    Context.PrimaryEntityName,
                    Context.PrimaryEntityId,
                    new Microsoft.Xrm.Sdk.Query.ColumnSet(
                        new string[] { TargetFieldSchemaName }));
            if (RetreivedEntity.Contains(TargetFieldSchemaName))
            {

                OptionSetValueCollection multiselect = RetreivedEntity.GetAttributeValue<OptionSetValueCollection>(TargetFieldSchemaName);

                if (multiselect.Count != 0)
                {
                    int[] arr = new int[multiselect.Count];
                    int i = 0;
                    foreach (var option in multiselect)
                    {
                        int value = option.Value;
                        arr[i] = value;
                        i++;
                    }
                    output = string.Join(",", arr);
                    Values.Set(ExecutionContext, output);
                }
            }
        }
    }
}
