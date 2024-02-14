using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GetConfigurationValues : CustomStepBase
    {
        [RequiredArgument]
        [Input("Key")]
        public InArgument<string> Key { get; set; }

        [Output("IntOutput")]
        public OutArgument<int> IntOutput { get; set; }
        [Output("StringOutput")]
        public OutArgument<string> StringOutput { get; set; }
        [Output("DoubleOutput")]
        public OutArgument<double> DoubleOutput { get; set; }
        [Output("DateTimeOutput")]
        public OutArgument<DateTime> DateTimeOutput { get; set; }


        public override void ExtendedExecute()
        {
            string KeyValue = Key.Get<string>(ExecutionContext);

            if (KeyValue == null)
                throw new Exception(string.Format($"KeyValue is null "));

            var value = CrmConfigurationKeys.GetPropertyValueByName(KeyValue);

            if(value.GetType() == typeof(int))
            {
                IntOutput.Set(ExecutionContext, value);
            }
            else if (value.GetType() == typeof(string))
            {
                StringOutput.Set(ExecutionContext, value);
            }
            else if (value.GetType() == typeof(double))
            {
                DoubleOutput.Set(ExecutionContext, value);
            }
            else if (value.GetType() == typeof(DateTime))
            {
                DateTimeOutput.Set(ExecutionContext, value);
            }
            else if (value.GetType() == typeof(Guid))
            {
                StringOutput.Set(ExecutionContext, value.ToString());
            }
        }
    }
}
