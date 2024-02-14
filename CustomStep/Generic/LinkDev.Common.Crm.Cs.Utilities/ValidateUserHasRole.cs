using LinkDev.Common.Crm.Bll.MessageLocalization;
using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class ValidateUserHasRole : CustomStepBase
    {
        [Input("Role Name")]
        [RequiredArgument]
        public InArgument<string> RoleName { get; set; }

        [Input("Message Name")]
        [RequiredArgument]
        [Default("[general] insufficient privilege")]
        public InArgument<string> MessageName { get; set; }

        public override void ExtendedExecute()
        {
            Guid _userId = Context.InitiatingUserId;
            Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"InitiatingUserId {_userId}\n", Logger.SeverityLevel.Info);
            string _roleName = RoleName.Get(ExecutionContext);
            string _messageName = MessageName.Get(ExecutionContext);
            string _errorMessage = TranslateMessages.GetMessage(OrganizationService, _messageName, LanguageCode);
            Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"'Message Text' *{_errorMessage}*\n", Logger.SeverityLevel.Info);
            if (!UserHasRole(_userId, _roleName))
                throw new InvalidPluginExecutionException(OperationStatus.Canceled, _errorMessage);
        }
        private bool UserHasRole(Guid userId, string roleName)
        {
            bool hasRole = false;
            QueryExpression query = new QueryExpression("systemuserroles");
            query.Criteria.AddCondition("systemuserid", ConditionOperator.Equal, userId);
            LinkEntity link = query.AddLink("role", "roleid", "roleid", JoinOperator.Inner);
            link.LinkCriteria.AddCondition("name", ConditionOperator.Equal, roleName);
            hasRole = OrganizationService.RetrieveMultiple(query).Entities.Count > 0;
            Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"'UserHasRole'*{hasRole}*\n", Logger.SeverityLevel.Info);
            return hasRole;
        }
    }
}
