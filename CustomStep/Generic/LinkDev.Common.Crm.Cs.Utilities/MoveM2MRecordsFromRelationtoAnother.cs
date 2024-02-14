using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using LinkDev.Common.Crm.Utilities;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class MoveM2MRecordsFromRelationtoAnother : CustomStepBase
    {

        #region "Input Parameters"

        [Input("Source logicalname")]
        public InArgument<string> sourceLogicalName { get; set; }

        [Input("Source Id")]
        public InArgument<string> sourceId{ get; set; }

        [Input("The name of M2M relationsihp in the source")]
        public InArgument<string> sourceM2MRelationName { get; set; }

        [Input("Source Intersect Entity Name")]
        public InArgument<string> sourceIntersectEntityName { get; set; }
      
        [Input("Destination logicalName")]
        public InArgument<string> destinationLogicalName { get; set; }

        [Input("Destination Id")]
        public InArgument<string> destinationId { get; set; }

        [Input("The name of M2M relationsihp in the destination")]
        public InArgument<string> destinationM2MRelationName { get; set; }

        [Input("Destination Intersect Entity Name")]
        public InArgument<string> destinationIntersectEntityName { get; set; }

        [Input("Common entity logical name that's between source and destinaiton")]
        public InArgument<string> commonEntityBetweenSrsDstLogicalName { get; set; }

        [Input("clear any records in destination")]
        public InArgument<bool> clearAnyRecordsInDestination { get; set; }
        #endregion

        #region "output Parameters"

        #endregion

        public override void ExtendedExecute()
        {
               var primaryEntityRef = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);

                #region Inputs Validation
                if (string.IsNullOrEmpty(sourceLogicalName.Get(ExecutionContext)))
                {
                    throw new Exception($"Source Logical name is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(sourceId.Get(ExecutionContext)))
                {
                    throw new Exception($"Source Id is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(sourceM2MRelationName.Get(ExecutionContext)))
                {
                    throw new Exception($"The relation name in the source is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(sourceIntersectEntityName.Get(ExecutionContext)))
                {
                    throw new Exception($"Source Intersect Entity Name is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(destinationLogicalName.Get(ExecutionContext)))
                {
                    throw new Exception($"The destination Logical name is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(destinationId.Get(ExecutionContext)))
                {
                    throw new Exception($"The destination Id is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(destinationM2MRelationName.Get(ExecutionContext)))
                {
                    throw new Exception($"The relation name for the distination is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(destinationIntersectEntityName.Get(ExecutionContext)))
                {
                    throw new Exception($"Destination Intersect Entity Name is empty, kindly provide it");
                }

                if (string.IsNullOrEmpty(commonEntityBetweenSrsDstLogicalName.Get(ExecutionContext)))
                {
                    throw new Exception($"Common entity logical name that's between source and destinaiton is empty, kindly provide it");
                }
                #endregion

                var sourceRef =
                    new EntityReference(sourceLogicalName.Get(ExecutionContext), new Guid(sourceId.Get(ExecutionContext)));

                var destinationRef =
                    new EntityReference(destinationLogicalName.Get(ExecutionContext), new Guid(destinationId.Get(ExecutionContext)));



                Tools.MoveM2MRecordsFromRelationtoAnother(
                    commonEntityBetweenSrsDstLogicalName.Get(ExecutionContext),
                    sourceRef,
                    sourceM2MRelationName.Get(ExecutionContext),
                    sourceIntersectEntityName.Get(ExecutionContext),
                    destinationRef,
                    destinationM2MRelationName.Get(ExecutionContext),
                    destinationIntersectEntityName.Get(ExecutionContext),
                    clearAnyRecordsInDestination.Get(ExecutionContext),
                    OrganizationService);

        }
    }
}
