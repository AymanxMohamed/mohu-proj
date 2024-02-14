using LinkDev.Common.Crm.Bll.MessageLocalization;
using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{
	public class CancelOngionRequest : CustomStepBase
	{
		#region InputPramter
		[Input("Entity Logical Name")]
		[RequiredArgument]
		public InArgument<string> EntityLogicalName { get; set; }

		[Input("Entity Id")]
		[RequiredArgument]
		public InArgument<string> EntityId { get; set; }

		[Input("Cancellation Reason")]
		[RequiredArgument]
		public InArgument<string> CancellationReason { get; set; }

		[Input("Cancellation Reason Field LogicalName")]
		[RequiredArgument]
		public InArgument<string> CancellationReasonFieldLogicalName { get; set; }

		[Input("CRM Status")]
		[ReferenceTarget("ldv_servicestatus")]
		public InArgument<EntityReference> CRMStatus { get; set; }

		[Input("CRM Sub-Status")]
		[ReferenceTarget("ldv_servicesubstatus")]
		public InArgument<EntityReference> CRMSubstatus { get; set; }

		[Input("Portal Status")]
		[ReferenceTarget("ldv_servicesubstatus")]
		public InArgument<EntityReference> PortalStatus { get; set; }

		[Input("Status Reason Code")]
		[RequiredArgument]
		public InArgument<int> StatusReasonCode { get; set; }

		[Input("Status")]
		[RequiredArgument]
		public InArgument<int> StatusCode { get; set; }

		[Input("Force Request Cancellation (ignore disable cancel request flage in case cancel from CRM)")]
		[Default("true")]
		[RequiredArgument]
		public InArgument<bool> ForceRequestCancellation { get; set; }

		[Input("CancellationFlagLogicalName (Update This Filed Only  If Contain data  whithout Execute Process Cancel)")]
		public InArgument<string> CancellationFlagLogicalName { get; set; }
		#endregion

		#region OutPutPramter
		[Output("FailureMessage")]
		public OutArgument<string> FailureMessage { get; set; }
		#endregion

		public override void ExtendedExecute()
		{
			try
			{
				#region Paramter
				var entityLogicalName = EntityLogicalName.Get<string>(ExecutionContext);

				var entityId = EntityId.Get<string>(ExecutionContext);

				var cancellationReason = CancellationReason.Get<string>(ExecutionContext);

				var cancellationReasonFieldLogicalName = CancellationReasonFieldLogicalName.Get<string>(ExecutionContext);

				var crmStatus = CRMStatus.Get<EntityReference>(ExecutionContext);

				var crmSubStatus = CRMSubstatus.Get<EntityReference>(ExecutionContext);

				var portalStatus = PortalStatus.Get<EntityReference>(ExecutionContext);

				var statusCode = StatusCode.Get<int>(ExecutionContext);

				var statusReasonCode = StatusReasonCode.Get<int>(ExecutionContext);

				var forceCancellation = ForceRequestCancellation.Get<bool>(ExecutionContext);

				var cancellationFlagLogicalName = CancellationFlagLogicalName.Get<string>(ExecutionContext);
                #endregion

                if (!forceCancellation)
                {
					Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"forceCancellatio is true\n", Logger.SeverityLevel.Info);

					if (TargetEntityIsDisableForCancel(entityId, entityLogicalName))
                    {
                        var errMessage = "";
                        if (LanguageCode == "1033")
                        {
                            errMessage = TranslateMessages.GetMessage(OrganizationService, "Common Cancel Ongoing Request", LanguageCode);
                        }
                        else
                        {
                            errMessage = TranslateMessages.GetMessage(OrganizationService, "Common Cancel Ongoing Request", LanguageCode);
                        }

                        FailureMessage.Set(ExecutionContext, errMessage);
                    }
					else
					{
						CancelRequest(cancellationFlagLogicalName, entityLogicalName, entityId, cancellationReasonFieldLogicalName,
							cancellationReason, crmStatus, crmSubStatus, portalStatus, statusCode, statusReasonCode);
					}
				}
				else
				{
					CancelRequest(cancellationFlagLogicalName, entityLogicalName, entityId, cancellationReasonFieldLogicalName, 
						cancellationReason, crmStatus, crmSubStatus, portalStatus, statusCode, statusReasonCode);
				}
			}
			catch (Exception ex)
			{
				Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"Exception: {ex.Message}   \n", Logger.SeverityLevel.Error);

				FailureMessage.Set(ExecutionContext, ex.Message);

			}
		}
		/// <summary>
		/// Check on Flage <ldv_isdisablecancelrequest>  if it true , request muste be on stagen can't cancel request like  Payment stage,Issuance license,etc..
		/// </summary>
		/// <param name="targetEntityId"> request crm id  </param>
		/// <param name="targetEntityLogicalName"> request entity logical name</param>
		/// <returns></returns>
		bool TargetEntityIsDisableForCancel(string targetEntityId, string targetEntityLogicalName)
		{
			// retrive target entity with attr <ldv_isdisablecancelrequest>
			var targetEntity = OrganizationService.Retrieve(
				targetEntityLogicalName,
				Guid.Parse(targetEntityId),
				new Microsoft.Xrm.Sdk.Query.ColumnSet("ldv_name", "ldv_isdisablecancelrequest"));

			//return true if disable cancel request is true 
			return targetEntity.Contains("ldv_isdisablecancelrequest") && targetEntity.GetAttributeValue<bool>("ldv_isdisablecancelrequest");
		}
		private void CloseOpenTasks(Guid regardingId, int stateCode, int statusReasonCode)
        {
			GetOpenTasks(regardingId).ForEach(t =>
			{
				OrganizationService.Execute(
				new SetStateRequest
				{
					State = new OptionSetValue(stateCode),
					Status = new OptionSetValue(statusReasonCode),
					EntityMoniker = t.ToEntityReference()
				});
			});
		}

        private List<Entity> GetOpenTasks(Guid regardingId)
        {
			var qe = new QueryExpression("task");
			qe.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0); // open tasks
			qe.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, regardingId);
			return OrganizationService.RetrieveMultiple(qe).Entities.ToList();
		}

		private void CancelRequest(string cancellationFlagLogicalName, string entityLogicalName, string entityId,
									string cancellationReasonFieldLogicalName, string cancellationReason, EntityReference crmStatus,
									EntityReference crmSubStatus, EntityReference portalStatus, int statusCode, int statusReasonCode)
        {
			Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"CancelRequest start \n", Logger.SeverityLevel.Info);

			if (string.IsNullOrWhiteSpace(cancellationFlagLogicalName))
			{
				var targetEntity = new Entity(entityLogicalName, Guid.Parse(entityId))
				{
					[cancellationReasonFieldLogicalName] = cancellationReason,
					["ldv_crmstatusid"] = crmStatus,
					["ldv_crmsubstatusid"] = crmSubStatus,
					["ldv_portalstatusid"] = portalStatus,
					["statecode"] = new OptionSetValue(statusCode),
					["statuscode"] = new OptionSetValue(statusReasonCode),
					["ldv_isdisablecancelrequest"] = true // must be set to true to avoid infinte loops
				};

				OrganizationService.Update(targetEntity);

				CloseOpenTasks(Guid.Parse(entityId), 1, 5);
			}
			else
			{
				OrganizationService.Update(new Entity(entityLogicalName, Guid.Parse(entityId))
				{
					[cancellationFlagLogicalName] = true
				}); ;
			}
		}
	}
}
