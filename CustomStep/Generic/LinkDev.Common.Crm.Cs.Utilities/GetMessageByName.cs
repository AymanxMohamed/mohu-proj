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
    public class GetMessageByName : CustomStepBase
    {
        [Input("Message Name")]
        [RequiredArgument]
        public InArgument<string> MessageNameInput { get; set; }

        [Input("Language Code")]
        [RequiredArgument]
        public InArgument<string> LanguageCodeInput { get; set; }

        [Output("Message")]
        [RequiredArgument]
        public OutArgument<string> Message { get; set; }
        public override void ExtendedExecute()
        {
            string MessageName = MessageNameInput.Get(ExecutionContext);
            string LanguageCode = LanguageCodeInput.Get(ExecutionContext);
            string fetchXML = @"<fetch>
                                  <entity name='ldv_messagelocalization'>
                                    <attribute name='ldv_messagetext' />
                                    <filter>
                                      <condition attribute='ldv_localizationlanguage' operator='eq' value='" + LanguageCode + @"' />
                                    </filter>
                                    <link-entity name='ldv_message' from='ldv_messageid' to='ldv_messageid'>
                                      <filter>
                                        <condition attribute='ldv_messagename' operator='eq' value='" + MessageName + @"' />
                                      </filter>
                                    </link-entity>
                                  </entity>
                                </fetch>";
            EntityCollection entitycollection = OrganizationService.RetrieveMultiple(new FetchExpression(fetchXML));
            if (entitycollection.Entities.Count == 1)
            {
                if (entitycollection.Entities[0].Contains("ldv_messagetext"))
                {

                    Message.Set(ExecutionContext, entitycollection.Entities[0].GetAttributeValue<string>("ldv_messagetext"));
                }
                else
                {
                    Message.Set(ExecutionContext, "Message is missing for language code " + LanguageCode);
                }
            }
            else if (entitycollection.Entities.Count > 1)
            {
                Message.Set(ExecutionContext, "There is multi messages with same name " + MessageName+ "for language code " + LanguageCode);
            }
            else
            {
                Message.Set(ExecutionContext, "Message is missing");
            }



        }
    }
}
