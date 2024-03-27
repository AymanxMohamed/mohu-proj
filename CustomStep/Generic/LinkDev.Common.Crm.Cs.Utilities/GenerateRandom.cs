using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Logger;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GenerateRandom : CustomStepBase
    {
        #region "Input Parameters"

        [Input("Min Integer Value")]
        [Default("1")]
        public InArgument<int> MinIntArg { get; set; }

        [Input("Max Integer Value")]
        [Default("999999999")]
        public InArgument<int> MaxIntArg { get; set; }

        [RequiredArgument]
        [Input("Return GUID")]
        [Default("False")]
        public InArgument<bool> IsGuidArg { get; set; }
        #endregion

        #region "Output Parameters"
        [Output("Generated Integer")]
        public OutArgument<int> IntArg { get; set; }

        [Output("Generated String")]
        public OutArgument<string> StringArg { get; set; }
        #endregion

        public override void ExtendedExecute()
        {
            int minValue = MinIntArg.Get(ExecutionContext);
            int num1 = MaxIntArg.Get(ExecutionContext);
            int num2 = IsGuidArg.Get(ExecutionContext) ? 1 : 0;
            int num3 = num2 != 0 ? 0 : new Random().Next(minValue, num1 + 1);
            string str = num2 != 0 ? Guid.NewGuid().ToString() : num3.ToString();


            IntArg.Set(ExecutionContext, num3);
            StringArg.Set(ExecutionContext, str);

            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"IntArg '{IntArg}', StringArg '{str}'", SeverityLevel.Info);
        }
    }
}
