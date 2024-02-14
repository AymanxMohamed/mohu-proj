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
    public class GetFieldValue : CustomStepBase
    {

        #region Input Parameter

        [Input("FieldName")]
        [RequiredArgument]
        public InArgument<string> fieldName { get; set; }

        #endregion

        #region Output Parameter

        [Output("String Value")]
        public OutArgument<string> stringValue { get; set; }

        [Output("Int Value")]
        public OutArgument<int> intValue { get; set; }

        [Output("DateTime Value")]
        public OutArgument<DateTime> dateTimeValue { get; set; }

        [Output("Team")]
        [ReferenceTarget("team")]
        public OutArgument<EntityReference> Team { get; set; }

        [Output("SystemUser")]
        [ReferenceTarget("systemuser")]
        public OutArgument<EntityReference> User { get; set; }

        [Output("Queue")]
        [ReferenceTarget("queue")]
        public OutArgument<EntityReference> Queue { get; set; }

        //[Output("OptionSetValue")]
        //public OutArgument<OptionSetValue> optionSetValue { get; set; }

        [Output("LogicalName")]
        public OutArgument<string> EntityLogicalName { get; set; }

        [Output("Id")]
        public OutArgument<string> EntityId { get; set; }
        #endregion

        public override void ExtendedExecute()
        {
            Team.Set(ExecutionContext, null);
            Queue.Set(ExecutionContext, null);
            User.Set(ExecutionContext, null);

            var substituted = CrmStringHandler.SubstituteToAttribute(new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId), fieldName.Get<string>(ExecutionContext), OrganizationService);
            if (substituted != null)
            {
                var Type = substituted.GetType();
                switch (Type.Name)
                {
                    case "String":
                        stringValue.Set(ExecutionContext, substituted);
                        break;
                    case "Int32":
                        intValue.Set(ExecutionContext, substituted);
                        break;
                    case "DateTime":
                        dateTimeValue.Set(ExecutionContext, substituted);
                        break;
                    case "EntityReference":
                        EntityReference entity = (EntityReference)substituted;
                        switch (entity.LogicalName)
                        {
                            case "queue":
                                Queue.Set(ExecutionContext, substituted);
                                break;
                            case "team":
                                Team.Set(ExecutionContext, substituted);
                                break;
                            case "systemuser":
                                User.Set(ExecutionContext, substituted);
                                break;
                            default:
                                EntityLogicalName.Set(ExecutionContext, entity.LogicalName);
                                EntityId.Set(ExecutionContext, entity.Id.ToString());
                                break;
                        }
                        break;
                    //case "OptionSetValue":
                    //    optionSetValue.Set(ExecutionContext, substituted);
                    //    break;
                    default:
                        break;
                }
            }
        }
    }
}
