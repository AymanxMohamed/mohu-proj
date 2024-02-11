//using LinkDev.MAAN.Common;
//using Microsoft.Crm.Sdk.Messages;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;
//using System;
//using System.Activities;
//using System.Collections.Generic;
////using System.Data.Entity.Core.Objects.DataClasses;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LinkDev.Common.Steps.MiniStageConfiguration.Logic
//{
//    public class SwitchToLookupLogic : StepLogic<SwitchToLookup>
//    {

//        protected override void ExecuteLogic()
//        {
//            tracingService.Trace($"  SwitchToLookupLogic");
//            log.LogInfo($" SwitchToLookupLogic");

//            #region map input paramaters 
//            //EntityReference processStage = codeActivity.ProcessStage.Get(executionContext);
//            string recordSchemaName = codeActivity.RecordSchemaName.Get(executionContext);
//            string recordId = codeActivity.RecordId.Get(executionContext);
//            codeActivity.IncubationApplication.Set(executionContext, null);
//            codeActivity.SocialCertificationApplication.Set(executionContext, null);
//            codeActivity.AllocationApplication.Set(executionContext, null);

//            if (recordSchemaName== "ldv_incubationapplication")
//            {
//                codeActivity.IncubationApplication.Set(executionContext, new EntityReference(recordSchemaName, new Guid(recordId)));
 
//            }
//            else if (recordSchemaName == "ldv_socialcertificationapplication")
//            {
//                codeActivity.SocialCertificationApplication.Set(executionContext, new EntityReference(recordSchemaName, new Guid(recordId)));

//            }
//            else if (recordSchemaName == "ldv_allocationapplication")
//            {
//                codeActivity.AllocationApplication.Set(executionContext, new EntityReference(recordSchemaName, new Guid(recordId)));

//            }
//            #endregion
//        }
//    }
//}
