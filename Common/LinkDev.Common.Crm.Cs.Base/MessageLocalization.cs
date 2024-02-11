using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Maan.Core.Helper
{
    public class MessageLocalization
    {
        //private CrmAccessLayer _crmAccess;
        private IOrganizationService _organizationService;
        public string LanguageCode { get; private set; }
        public StringBuilder Tracing { get; private set; }

        public MessageLocalization(IOrganizationService organizationService, string languageCode)
        {
            //_crmAccess = new CrmAccessLayer(organizationService);
            _organizationService = organizationService;
            LanguageCode = languageCode;
            Tracing = new StringBuilder();
        }

        public MessageLocalization(IOrganizationService organizationService)
        {
            //_crmAccess = new CrmAccessLayer(organizationService);
            _organizationService = organizationService;
            Tracing = new StringBuilder();
        }

        public string GetMessageByName(string Name)
        {
            OrganizationServiceContext context = new OrganizationServiceContext(_organizationService);
            var Messages = (from c in context.CreateQuery("ldv_messages")
                            where c["ldv_name"].Equals(Name)
                            select c).ToList();
            //List<Entity> Messages = RetrieveMultiple(_organizationService, "ldv_messages", new string[] { "ldv_name" }, new object[] { Name }, new string[] { });
            if (Messages.Count > 0)
            {
                if (Messages[0] != null && Messages[0].Attributes.Contains("ldv_messageinarabic") && LanguageCode == "1025")
                {
                    return Messages[0].Attributes["ldv_messageinarabic"].ToString();
                }
                else if (Messages[0] != null && Messages[0].Attributes.Contains("ldv_englishmessage") && LanguageCode == "1033")
                {
                    return Messages[0].Attributes["ldv_englishmessage"].ToString();
                }
                else if (LanguageCode == "1025")
                {
                    return "نعتذر عن عدم وجود الرساله";
                }
                else
                {
                    return "Sorry, Message not found.";
                }
            }
            else
            {
                if (LanguageCode == "1025")
                    return "نعتذر عن عدم وجود الرساله";
                return "Sorry, Message not found.";
            }
        }

        public string GetUserLanguage(Guid userId)
        {
            string languageCode = "1033";
            Entity userSettings = _organizationService.RetrieveMultiple(
            new QueryExpression("usersettings")
            {
                ColumnSet = new ColumnSet("uilanguageid"),
                Criteria = new FilterExpression
                {
                    Conditions =
                     {
                     new ConditionExpression("systemuserid", ConditionOperator.Equal, userId)
                     }
                }
            }).Entities.FirstOrDefault();
            if (userSettings != null && userSettings.Contains("uilanguageid"))
            {
                if (userSettings != null && userSettings.Contains("uilanguageid"))
                {
                    languageCode = userSettings["uilanguageid"].ToString();
                }
            }
            return languageCode;
        }

        public string GetMessageByUserId(string MessageId, Guid UserId)
        {
            var ULC = GetUserLanguage(UserId);
            return GetMessageByName(MessageId, ULC);
        }

        public string GetMessageByName(string Name, string _languageCode)
        {
            try
            {
                OrganizationServiceContext context = new OrganizationServiceContext(_organizationService);
                var Messages = (from c in context.CreateQuery("ldv_messages")
                                where c["ldv_name"].Equals(Name)
                                select c).ToList();
                //List<Entity> Messages = RetrieveMultiple(_organizationService, "ldv_messages", new string[] { "ldv_name" }, new object[] { Name }, new string[] { });
                if (Messages.Count > 0)
                {
                    if (Messages[0] != null && Messages[0].Attributes.Contains("ldv_messageinarabic") && _languageCode == "1025")
                    {
                        return Messages[0].Attributes["ldv_messageinarabic"].ToString();
                    }
                    else if (Messages[0] != null && Messages[0].Attributes.Contains("ldv_englishmessage") && _languageCode == "1033")
                    {
                        return Messages[0].Attributes["ldv_englishmessage"].ToString();
                    }
                    else if (_languageCode == "1025")
                    {
                        return "نعتذر عن عدم وجود الرساله";
                    }
                    else
                    {
                        return "Sorry, Message not found.";
                    }
                }
                else
                {
                    if (_languageCode == "1025")
                        return "نعتذر عن عدم وجود الرساله";
                    return "Sorry, Message not found.";
                }
            }
            catch (Exception ex)
            {
                if (_languageCode == "1025")
                    return "نعتذر عن عدم وجود الرساله";
                return "Sorry, Message not found.";
            }

        }
    }
}
