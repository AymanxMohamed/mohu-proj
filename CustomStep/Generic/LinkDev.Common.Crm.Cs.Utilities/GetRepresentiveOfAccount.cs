using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Utilities;

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
    public class GetRepresentiveOfAccount : CustomStepBase
    {
        #region Input Parameters
        [Input("Account")]
        [RequiredArgument]
        [ReferenceTarget("account")]
        public InArgument<EntityReference> Account { get; set; }

        [Output("Contact")]
        [ReferenceTarget("contact")]
        public OutArgument<EntityReference> contact { get; set; }
        #endregion
        public override void ExtendedExecute()
        {
            Guid accountId = Account.Get<EntityReference>(ExecutionContext).Id;
            // to be implemented based on GEA Logic

            //Guid contactId = GetGenricContactPerson(accountId);
            Guid contactId = Guid.Empty;
            if (contactId!= Guid.Empty)
            {
                contact.Set(ExecutionContext, new EntityReference("contact", contactId));

            }
            else
            {
                contact.Set(ExecutionContext, null);

            }
        }

        private Guid GetGenricContactPerson(Guid accountId)
        {
            var fetch =
                @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                  <entity name='ldv_authenticationrole'>
                    <attribute name='ldv_name' />
                    <attribute name='createdon' />
                    <attribute name='ldv_roletypecodes' />
                    <attribute name='ldv_contactid' />
                    <attribute name='ldv_accounttypecodes' />
                    <attribute name='ldv_accountid' />
                    <attribute name='ldv_authenticationroleid' />
                    <order attribute='createdon' descending='true' />
                    <filter type='and'>
                      <condition attribute='statecode' operator='eq' value='0' />
                      <condition attribute='ldv_accountid' operator='eq' uitype='account' value='{"+ accountId + @"}' />
                            <condition attribute='ldv_roletypecodes' operator='contain-values'>
                              <value>1</value>
                            </condition>
                    </filter>
                  </entity>
                </fetch>";

            var relatedAuth = OrganizationService.RetrieveMultiple(new FetchExpression(fetch));

            if(relatedAuth.Entities.Count > 0)
            {
                var enitity = relatedAuth.Entities[0];
                if (enitity.Contains("ldv_contactid"))
                {
                    return enitity.GetAttributeValue<EntityReference>("ldv_contactid").Id;
                }
                else
                    return Guid.Empty;
            }
            else
            {
                return Guid.Empty;
            }
            
        }
    }
    class ContactPersonModel
    {
        public string ContactName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactMobileNo { get; set; }
        public string ContactEmail { get; set; }
    }
}
