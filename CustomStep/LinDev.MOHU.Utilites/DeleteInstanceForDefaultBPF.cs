using LinDev.MOHU.Utilites.Logic;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites
{
    public class DeleteInstanceForDefaultBPF : CodeActivity
    {
        #region "Input Parameters"

        [Input("logicalName")]
        public InArgument<string> EntityLogicalName { get; set; }

        [Input("Id")]
        public InArgument<string> EntityId { get; set; }

        #endregion
        protected override void Execute(CodeActivityContext context)
        {
            new DeleteInstanceForDefaultBPFLogic().Execute(this, context);
        }
    }
}
