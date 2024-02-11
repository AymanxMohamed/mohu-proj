using LinkDev.MAAN.Common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Activities;
using System.Collections.Generic;
//using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Logic
{
    public class GetAssigningUserLogic : StepLogic<GetAssigningUser>
    {
        protected override void ExecuteLogic()
        {

            tracingService.Trace($"  GetAssigningUserLogic");
            log.LogInfo($" GetAssigningUserLogic");
          
            #region map input paramaters 
            //EntityReference processStage = codeActivity.ProcessStage.Get(executionContext);
            string assignField = codeActivity.AssignUserLogicalName.Get(executionContext);
            string entityLogicalName = codeActivity.EntityLogicalName.Get(executionContext);
            string entityId = codeActivity.EntityId.Get(executionContext);
            #endregion
          //  codeActivity.AssignUser.Set(executionContext, null);
            #region Logic
            log.LogInfo($" rentityLogicalName {entityLogicalName} , entityId {entityId}");

            //var query = new QueryExpression(entityLogicalName);
            //query.ColumnSet.AddColumns(assignField);
            //query.Criteria.AddCondition(entityLogicalName+"id", ConditionOperator.Equal, entityId);
            ColumnSet ColumnSet = new ColumnSet();
            ColumnSet.AddColumn(assignField);
            Entity requestEntity = service.Retrieve(entityLogicalName,new Guid( entityId), ColumnSet);
            if (requestEntity != null && requestEntity?.Id != null&& requestEntity.Contains(assignField) )
            {
                log.LogInfo($" requestEntity contain assignField");

                EntityReference user = requestEntity.GetAttributeValue<EntityReference>(assignField) ;
                log.LogInfo($" user ID {user.Id}");

                codeActivity.AssignUser.Set(executionContext, user);
            }
            #endregion
           
        }
    }
}
