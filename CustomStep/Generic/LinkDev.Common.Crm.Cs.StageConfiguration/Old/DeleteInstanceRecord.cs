//using LinkDev.Common.Crm.Cs.Base;
//using LinkDev.Common.Crm.Logger;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Sdk.Workflow;
//using System;
//using System.Activities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

//namespace LinkDev.Common.Crm.Cs.StageConfiguration
//{
//     class DeleteInstanceRecord : CustomStepBase
//    {
//        #region "Input Parameters"
//        [RequiredArgument]
//        [Input("Instance Schema Name")]
//        public InArgument<string> InstanceSchemaName { get; set; }

//        [RequiredArgument]
//        [Input("Lookup Id")]
//        public InArgument<string> LookupId { get; set; }

//        [RequiredArgument]
//        [Input("Lookup Schema Name in Instance")]
//        public InArgument<string> LookupSchemaName { get; set; }
//        #endregion

//        public override void ExtendedExecute()
//        {
//            #region Check if input paramaters are null
//            if (InstanceSchemaName.Get<string>(ExecutionContext) == null)
//                throw new Exception(string.Format($"EntityLogicalName { InstanceSchemaName} is null "));
//            if (LookupId.Get<string>(ExecutionContext) == null)
//                throw new Exception(string.Format($"EntityLogicalName { LookupId} is null "));
//            if (LookupSchemaName.Get<string>(ExecutionContext) == null)
//                throw new Exception(string.Format($"EntityLogicalName { LookupSchemaName} is null "));
//            #endregion

//            string lookId = LookupId.Get<string>(ExecutionContext);
//            string lookupSchemaName = LookupSchemaName.Get<string>(ExecutionContext);
//            string instanceSchemaName= InstanceSchemaName.Get<string>(ExecutionContext);
//            EntityReference request = new EntityReference(lookupSchemaName, new Guid(lookId));

//            #region Query

//            var instanceQuery = new QueryExpression(instanceSchemaName);
//            instanceQuery.Criteria.AddCondition(lookupSchemaName, ConditionOperator.Equal, lookId);

//            #endregion
//            var retrieveInstance = OrganizationService.RetrieveMultiple(instanceQuery);
//            if (retrieveInstance.Entities.Any())
//            {
//                Entity instanceEntity = retrieveInstance.Entities.FirstOrDefault();
//                if (instanceEntity.Id!=null&& instanceEntity.Id!=Guid.Empty)
//                {
//                    Tracer.LogComment(this.GetType().FullName, $" instanceEntity id {instanceEntity.Id}", SeverityLevel.Info);
//                    OrganizationService.Delete(instanceEntity.LogicalName, instanceEntity.Id);
//                }
//            }
//        }
//    }
//}
     
 
