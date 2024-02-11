using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinDev.MOHU.Utilites.Logic;
//using Linkdev.Maan.Steps.FundraisingCampaign.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;


namespace LinDev.MOHU.Utilites
{
    public class GetEntityReferencePrimitives : CodeActivity
    {
        #region "Input Parameters"

        [Input("Use the current entity as entity refrence")]
        [Default("true")]
        public InArgument<bool> UseWFContext { get; set; }

        [Input("Lookup logical name (useWFContext is false)")]
        public InArgument<string> EntityRefrenceFieldLogicalName { get; set; }
        #endregion

        #region "Output Parameters"

        [Output("logicalName")]
        public OutArgument<string> EntityLogicalName { get; set; }

        [Output("Id")]
        public new OutArgument<string> EntityId { get; set; }

        #endregion



        protected override void Execute(CodeActivityContext context)
        {
            new GetEntityReferencePrimitivesLogic().Execute(this, context);
        }
    }
}
