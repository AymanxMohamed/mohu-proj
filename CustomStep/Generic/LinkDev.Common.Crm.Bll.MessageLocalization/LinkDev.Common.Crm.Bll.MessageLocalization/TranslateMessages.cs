using System;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace LinkDev.Common.Crm.Bll.MessageLocalization
{
    public static class TranslateMessages
    {
        private static string _beginningofMessageCode = "0x";
        private static string _httpResponseCodeSeparator = "x";
        private static string _messageCodeSeparator = " | ";

        public static Message ExtractMessageCodes(string message)
        {
            var outMessage = new Message();
            outMessage.TranslatedMessage = message;
            if (message.StartsWith(_beginningofMessageCode))
            {
                var messageCodeSeparatorIndx = message.IndexOf(_messageCodeSeparator);
                if (messageCodeSeparatorIndx >= 0)
                {
                    var tmp = message.Substring(0, messageCodeSeparatorIndx);
                    tmp = tmp.Remove(0, _beginningofMessageCode.Length);
                    var isContainingHttpCode = tmp.IndexOf(_httpResponseCodeSeparator) >= 0 ? true : false;
                    var tmps = tmp.Split(_httpResponseCodeSeparator.ToCharArray());
                    if (isContainingHttpCode)
                    {
                        if (tmps.Length > 0)
                        {
                            outMessage.HttpResponseCode = int.Parse(tmps[0]);
                        }
                        if (tmps.Length > 1)
                        {
                            outMessage.MessageCode = tmps[1];
                        }
                    }
                    else
                    {
                        outMessage.MessageCode = tmps[0];
                    }


                }
            }

            return outMessage;
        }

        public static Message ExtractMessageCodes(IOrganizationService serviceProxy , string message , string languageCode)
        {
            var outMessage = new Message();
            outMessage.TranslatedMessage = message;
            if (message.StartsWith(_beginningofMessageCode))
            {
                var messageCodeSeparatorIndx = message.IndexOf(_messageCodeSeparator);
                if (messageCodeSeparatorIndx >= 0)
                {
                    var tmp = message.Substring(0, messageCodeSeparatorIndx);
                    tmp = tmp.Remove(0, _beginningofMessageCode.Length);
                    var isContainingHttpCode = tmp.IndexOf(_httpResponseCodeSeparator) >= 0 ? true : false;
                    var tmps = tmp.Split(_httpResponseCodeSeparator.ToCharArray());
                    if (isContainingHttpCode)
                    {
                        if (tmps.Length > 0)
                        {
                            outMessage.HttpResponseCode = int.Parse(tmps[0]);
                        }
                        if (tmps.Length > 1)
                        {
                            outMessage.MessageCode = tmps[1];
                        }
                    }
                    else
                    {
                        outMessage.MessageCode = tmps[0];
                    }


                }
            }
            if(!string.IsNullOrEmpty( outMessage.MessageCode))
            {
                outMessage = GetMessageUsingCode(serviceProxy, outMessage.MessageCode, languageCode);
            }

            return outMessage;
        }

        public static Message GetMessageUsingCode(IOrganizationService serviceProxy, string messageCode, string languageCode, string messageParametersCommaSeperated = null)
        {

            int languageCodeParsed;
            Message message = new Message();
            var messageCodeConstruction = "";
            if (!int.TryParse(languageCode, out languageCodeParsed))
            {
                message.TranslatedMessage = "Invalid Language Code";
                return message;
            }
            try
            {
                QueryExpression query = new QueryExpression(Messages.EntityLogicalName);
                FilterExpression filter1 = new FilterExpression();
                filter1.AddCondition(Messages.MessageCode, ConditionOperator.Equal, messageCode);
                query.ColumnSet = new ColumnSet(true);
                query.Criteria.AddFilter(filter1);
                var messages = serviceProxy.RetrieveMultiple(query);

                if (messages.Entities.Count > 0)
                {
                    var tmp = GetMessage(serviceProxy, messages.Entities[0].ToEntityReference(), languageCode, messageParametersCommaSeperated);

                    if (messages[0].Contains(Messages.MessageCode))
                        message.MessageCode = messages[0][Messages.MessageCode].ToString();
                    if (messages[0].Contains(Messages.HttpResponseMessageCode))
                        message.HttpResponseCode = int.Parse(messages[0][Messages.HttpResponseMessageCode].ToString());

                    // ex: 0x401x1001
                    if (!string.IsNullOrEmpty(message.MessageCode) || message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction = $"{_beginningofMessageCode}";
                    }
                    if (message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction += $"{message.HttpResponseCode}";
                    }
                    if (!string.IsNullOrEmpty(message.MessageCode) && message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction += $"{_httpResponseCodeSeparator}{message.MessageCode}";
                    }
                    else if (!string.IsNullOrEmpty(message.MessageCode) && !message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction += $"{message.MessageCode}";
                    }

                    if (!string.IsNullOrEmpty(messageCodeConstruction))
                    {
                        messageCodeConstruction += $"{_messageCodeSeparator}";
                    }

                    message.TranslatedMessage = $"{messageCodeConstruction}{tmp}";

                    return message;
                }
            }
            catch (System.Exception)
            {
            }

            switch (languageCodeParsed)
            {
                case 1025:
                    message.TranslatedMessage = "الرسالة [" + messageCode + "] ليست موجودة";
                    return message;
                default:
                    message.TranslatedMessage = "Message [" + messageCode + "] Not Found";
                    return message;
            }
        }

        public static Message GetMessageWithCodes(IOrganizationService serviceProxy, string messageName, string languageCode, string messageParametersCommaSeperated = null)
        {
            int languageCodeParsed;
            Message message = new Message();
            var messageCodeConstruction = "";
            if (!int.TryParse(languageCode, out languageCodeParsed))
            {
                message.TranslatedMessage = "Invalid Language Code";
                return message;
            }
            try
            {
                QueryExpression query = new QueryExpression(Messages.EntityLogicalName);
                FilterExpression filter1 = new FilterExpression();
                filter1.AddCondition(Messages.MessageName, ConditionOperator.Equal, messageName);
                query.ColumnSet = new ColumnSet(true);
                query.Criteria.AddFilter(filter1);
                var messages = serviceProxy.RetrieveMultiple(query);

                if (messages.Entities.Count > 0)
                {
                    var tmp = GetMessage(serviceProxy, messages.Entities[0].ToEntityReference(), languageCode, messageParametersCommaSeperated);

                    if (messages[0].Contains(Messages.MessageCode))
                        message.MessageCode = messages[0][Messages.MessageCode].ToString();
                    if (messages[0].Contains(Messages.HttpResponseMessageCode))
                        message.HttpResponseCode = int.Parse(messages[0][Messages.HttpResponseMessageCode].ToString());

                    // ex: 0x401x1001
                    if (!string.IsNullOrEmpty(message.MessageCode) || message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction = $"{_beginningofMessageCode}";
                    }
                    if (message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction += $"{message.HttpResponseCode}";
                    }
                    if (!string.IsNullOrEmpty(message.MessageCode) && message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction += $"{_httpResponseCodeSeparator}{message.MessageCode}";
                    }
                    else if (!string.IsNullOrEmpty(message.MessageCode) && !message.HttpResponseCode.HasValue)
                    {
                        messageCodeConstruction += $"{message.MessageCode}";
                    }

                    if (!string.IsNullOrEmpty(messageCodeConstruction))
                    {
                        messageCodeConstruction += $"{_messageCodeSeparator}";
                    }

                    message.TranslatedMessage = $"{messageCodeConstruction}{tmp}";

                    return message;
                }
            }
            catch (System.Exception)
            {
            }

            switch (languageCodeParsed)
            {
                case 1025:
                    message.TranslatedMessage = "الرسالة [" + messageName + "] ليست موجودة";
                    return message;
                default:
                    message.TranslatedMessage = "Message [" + messageName + "] Not Found";
                    return message;
            }
        }


        /// <summary>
        /// Get Translated message from CRM, given the message name and reference to the organization service
        /// </summary>
        /// <param name="context">Reference to context (used to get user language)</param>
        /// <param name="serviceProxy">Reference to organization service</param>
        /// <param name="messageName">Message name</param>
        /// <param name="messageParametersCommaSeperated">Comma Separated Parameters "Param1,Param2"</param>
        /// <returns>The translated message</returns>
        public static string GetMessage(IPluginExecutionContext context, IOrganizationService serviceProxy, string messageName, string messageParametersCommaSeperated = null)
        {
            string languageCode = GetUserLanguage(context.UserId.ToString(), serviceProxy);
            return GetMessage(serviceProxy, messageName, languageCode, messageParametersCommaSeperated);
        }

        /// <summary>
        /// Get Translated message from CRM, given the message name and reference to the organization service
        /// </summary>
        /// <param name="serviceProxy">Reference to organization service</param>
        /// <param name="messageName">Message name</param>
        /// <param name="languageCode">Language code
        /// 1033 for English
        /// 1025 for Arabic</param>
        /// <param name="messageParametersCommaSeperated">Comma Separated Parameters "Param1,Param2"</param>
        /// <returns>The translated message</returns>
        public static string GetMessage(IOrganizationService serviceProxy, string messageName, string languageCode, string messageParametersCommaSeperated = null)
        {
            return GetMessageWithCodes(serviceProxy, messageName, languageCode, messageParametersCommaSeperated).TranslatedMessage;
        }

        /// <summary>
        /// Get Translated message from CRM, given the message name and reference to the organization service
        /// </summary>
        /// <param name="serviceProxy">Reference to organization service</param>
        /// <param name="messageName">Message name</param>
        /// <param name="languageCode">Language code
        /// 1033 for English
        /// 1025 for Arabic</param>
        /// <param name="messageParametersCommaSeperated">Comma Separated Parameters "Param1,Param2"</param>
        /// <returns>The translated message</returns>
        public static string GetMessage(IOrganizationService serviceProxy, EntityReference messageName, string languageCode, string messageParametersCommaSeperated = null)
        {
            int languageCodeParsed;
            if (!int.TryParse(languageCode, out languageCodeParsed))
            {
                return "Invalid Language Code";
            }
            try
            {
                QueryExpression queryLocalizations = new QueryExpression(MessageLocalization.EntityLogicalName);
                FilterExpression filter2 = new FilterExpression();
                filter2.AddCondition(MessageLocalization.MessageId, ConditionOperator.Equal,
                    messageName.Id);
                queryLocalizations.ColumnSet = new ColumnSet(true);
                queryLocalizations.Criteria.AddFilter(filter2);
                var messagesLocalized = serviceProxy.RetrieveMultiple(queryLocalizations);

                foreach (var messageLocalization in messagesLocalized.Entities)
                {
                    if (messageLocalization.Contains(MessageLocalization.MessageText) &&
                        messageLocalization.Contains(MessageLocalization.LocalizationLanguage) &&
                        messageLocalization.Attributes[MessageLocalization.LocalizationLanguage] is
                            OptionSetValue)
                    {
                        var localizationLanguageValue =
                            (OptionSetValue)
                                messageLocalization.Attributes[MessageLocalization.LocalizationLanguage];
                        if (localizationLanguageValue.Value == languageCodeParsed)
                        {
                            return ParametersSubstitution(messageLocalization.Attributes[MessageLocalization.MessageText].ToString(), messageParametersCommaSeperated);
                        }
                    }
                }

            }
            catch (System.Exception)
            {
            }

            switch (languageCodeParsed)
            {
                case 1025:
                    return "الرسالة [" + messageName + "] ليست موجودة";
                default:
                    return "Message [" + messageName + "] Not Found";
            }
        }
        /// <summary>
        /// Gets the current language for a specific user
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <param name="organiationService">Reference to organization service</param>
        /// <returns>language code</returns>
        public static string GetUserLanguage(string userId, IOrganizationService organiationService)
        {
            string currentLanguage = string.Empty;
            try
            {
                //retrieve user record
                ColumnSet fields = new ColumnSet();
                fields.AddColumn("uilanguageid");

                Entity user = organiationService.Retrieve("usersettings", new Guid(userId), fields);


                //get language
                currentLanguage = user.Attributes["uilanguageid"].ToString();

            }
            catch (System.Exception)
            {
                //expLog.CatchException(Err);
            }

            return currentLanguage;
        }

        private static string ParametersSubstitution(string message, string messageParametersCommaSeperated)
        {
            try
            {
                if (!string.IsNullOrEmpty(messageParametersCommaSeperated))
                {
                    var parametersSplited = messageParametersCommaSeperated.Split(',');
                    if (parametersSplited.Length > 0)
                    {
                        message = string.Format(message, parametersSplited);
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return message;
        }

        public static string ConvertFromISO6391toLCIDDecimal(string ISO6391Code)
        {
            // Default english
            var lCIDDecimal = "";
            if (string.IsNullOrEmpty(ISO6391Code))
                throw new System.Exception($"'{nameof(ISO6391Code)}' can't be empty");

            var firstPartOfISO6391Code = ISO6391Code.Split('-')[0].ToLower();
            switch (firstPartOfISO6391Code)
            {
                case "ar":
                    lCIDDecimal = "1025";
                    break;
                case "en":
                    lCIDDecimal = "1033";
                    break;
            }

            return lCIDDecimal;
        }

        public static string GetMessagename(IOrganizationService organiationService, EntityReference messageentityReference)
        {
            var message = 
                organiationService.Retrieve(messageentityReference.LogicalName, messageentityReference.Id, new ColumnSet(true));

            if (message.Contains(Messages.MessageName))
            {
                return message[Messages.MessageName].ToString();
            }
            return string.Empty;
        }
    }

    public static class Messages
    {
        public static string EntityLogicalName = "ldv_message";

        /// <summary>
        /// Type:String
        /// ShemaName; ldv_messagename
        /// </summary>
        public static string MessageName
        {
            get { return "ldv_messagename"; }
        }

        /// <summary>
        /// Type: String
        /// ShemaName; ldv_parametersdescription
        /// </summary>
        public static string ParametersDescription
        {
            get { return "ldv_parametersdescription"; }
        }

        public static string MessageCode
        {
            get { return "ldv_messagecode"; }
        }

        public static string HttpResponseMessageCode
        {
            get { return "ldv_httpresponsemessagecode"; }
        }
    }

    public static class MessageLocalization
    {
        public static string EntityLogicalName = "ldv_messagelocalization";

        /// <summary>
        /// Type:Lookup
        /// ShemaName: ldv_messageid
        /// </summary>
        public static string MessageId
        {
            get { return "ldv_messageid"; }
        }

        /// <summary>
        /// Type: String
        /// ShemaName: ldv_messagetext
        /// </summary>
        public static string MessageText
        {
            get { return "ldv_messagetext"; }
        }

        /// <summary>
        /// Type: Optionset
        /// ShemaName: ldv_localizationlanguage
        /// </summary>
        public static string LocalizationLanguage
        {
            get { return "ldv_localizationlanguage"; }
        }

    }

    public class Message
    {
        public string TranslatedMessage { get; set; }
        public string MessageCode { get; set; }
        public int? HttpResponseCode { get; set; }
    }
}
