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

    public class MapMutliSelectValue : CustomStepBase
    {

        #region "Input Parameters"

        [Input("Pass Logical Name and Id Or Use Target From field on Form")]
        [RequiredArgument]
        public InArgument<bool> passLogicalNameandIdOrUseTargetFromFieldonForm { get; set; }

        [Input("Target Entity Name")]
        [RequiredArgument]
        public InArgument<string> targetEntityName { get; set; }

        [Input("Target Entity Id (mandatory in case you will not use a field on form)")]
        public InArgument<string> targetEntityId { get; set; }

        [Input("Target Field Schema Name")]
        [RequiredArgument]
        public InArgument<string> targetFieldSchemaName { get; set; }

        [Input("Values")]
        [RequiredArgument]
        public InArgument<string> Values { get; set; }

        [Input("Clear Old Values")]
        [RequiredArgument]
        [Default("true")]
        public InArgument<bool> ClearOldValued { get; set; }
        #endregion


        public override void ExtendedExecute()
        {
            Tracer.LogComment(
                Logger.LoggerHandler.GetMethodFullName(), 
                $"'{nameof(targetEntityName)}':'{targetEntityName.Get<string>(ExecutionContext)}', '{nameof(targetEntityId)}': '{targetEntityId.Get<string>(ExecutionContext)}'", Logger.SeverityLevel.Info);

            EntityReference TargetEntity = null;

            if (passLogicalNameandIdOrUseTargetFromFieldonForm.Get<bool>(ExecutionContext))
            {
                if (string.IsNullOrEmpty(targetEntityId.Get<string>(ExecutionContext)))
                {
                    throw new Exception($"'{nameof(targetEntityId)}' can't be null as you choose to pass logical name and Id");
                }
                   
                Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"targetEntityName '{targetEntityName.Get<string>(ExecutionContext)}', '{targetEntityId.Get<string>(ExecutionContext)}'", Logger.SeverityLevel.Info);


                TargetEntity =
                    new EntityReference(
                        targetEntityName.Get<string>(ExecutionContext),
                        new Guid(targetEntityId.Get<string>(ExecutionContext)));
            }
            else
            {
                var PrimaryEntity = 
                    new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);

                Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"PrimaryEntity {PrimaryEntity.LogicalName}, '{PrimaryEntity.Id}'", Logger.SeverityLevel.Info);


                TargetEntity = 
                    (EntityReference)CrmStringHandler.SubstituteToAttribute(
                        PrimaryEntity,
                        targetEntityName.Get<string>(ExecutionContext),
                        OrganizationService);
            }

            var SchemaName = targetFieldSchemaName.Get<string>(ExecutionContext);
            var clearOldValued = ClearOldValued.Get<bool>(ExecutionContext);


            var values = Values.Get<string>(ExecutionContext);
            Tools.MapValue(OrganizationService, clearOldValued, values, TargetEntity, SchemaName);
        }
    }
}
