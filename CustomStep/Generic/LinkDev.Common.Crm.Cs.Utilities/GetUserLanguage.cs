using LinkDev.Common.Crm.Cs.Base;
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
    public class GetUserLanguage : CustomStepBase
    {
        [Input("User")]
        [RequiredArgument]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> User { get; set; }

        [Output("Language Code")]
        [RequiredArgument]
        public OutArgument<string> LanguageCode { get; set; }

        public override void ExtendedExecute()
        {
            EntityReference Userreferance = User.Get(ExecutionContext);
            LanguageCode.Set(ExecutionContext, "1025");
            try
            {
                Entity userSettings = OrganizationService.RetrieveMultiple(

                new QueryExpression("usersettings")
                {
                    ColumnSet = new ColumnSet("uilanguageid"),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                        new ConditionExpression("systemuserid", ConditionOperator.Equal, Userreferance.Id)
                        }
                    }
                }).Entities.FirstOrDefault();

                if (userSettings.Contains("uilanguageid") && userSettings.GetAttributeValue<int>("uilanguageid") != 0)
                {
                    LanguageCode.Set(ExecutionContext, userSettings.GetAttributeValue<int>("uilanguageid").ToString());
                }
            }
            catch (System.Exception exception)
            {
                LanguageCode.Set(ExecutionContext, "1025");
                Tracer.LogComment(this.GetType().FullName, $"GetUserLanguage: {exception.Message}", Logger.SeverityLevel.Error);
            }
        }
    }
}
