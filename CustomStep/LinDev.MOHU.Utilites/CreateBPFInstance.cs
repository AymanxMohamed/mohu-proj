using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites
{
    public class CreateBPFInstance : CodeActivity
    {
        #region "Input Parameters"

         [RequiredArgument]
        [Input("EntityReferenceId")]
        public InArgument<string> EntityReferenceId { get; set; }
        [RequiredArgument]
        [Input("EntityReferenceSchemaName")]
        public InArgument<string> EntityReferenceName { get; set; }

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
