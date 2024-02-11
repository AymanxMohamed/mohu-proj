using LinkDev.Common.Steps.MiniStageConfiguration.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration
{
    public class GetMaanStageConfiguration : CodeActivity
    {

        #region "Input Parameters"
        [Input("Is Context Target Entity?")]
        [Default("False")]
        public InArgument<bool> IsContextIsTargetEntity { get; set; }

       // [RequiredArgument]
        [Input("Process Stage Id")]
        public InArgument<string> StageId { get; set; }

        //[Input("Process Stage Id as string")]
        //public InArgument<string> StageIdAsString { get; set; }
        /////////////////////

        [Input("Entity Id")]
        public InArgument<string> EntityId { get; set; }

        //[RequiredArgument]
        [Input("Entity Schema Name")]
        public InArgument<string> EntitySchemaName { get; set; }

        [Input("BPF Schema Name")]
        public InArgument<string> BPFSchemaName { get; set; }
        [Input("Schema Name Of Target Entity In BPF")]
        public InArgument<string> SchemaNameOfTargetEntityInBPF { get; set; }
        #endregion
        #region "Output Parameters"

        [Output("Maan Stage Configuration")]
        [ReferenceTarget("ldv_ministageconfiguration")]
        public OutArgument<EntityReference> StageConfiguration { get; set; }

        #endregion

        protected override void Execute(CodeActivityContext context)
        {
            new MaanStageConfigurationLogic().Execute(this, context);
        }
    }
}
