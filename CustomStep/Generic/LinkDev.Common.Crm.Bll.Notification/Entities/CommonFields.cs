using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Bll.Notification.Entities
{
    public class CommonFields
    {
        public static string PreferredLanguage = "ldv_preferredlanguagecode";
    }
    public class SystemUserFields
    {
        public static string PreferredLanguage = "ldv_preferredlanguagecode";
    }

    public class NotificationPrimitives
    {
        public bool WithSms { get; set; }
        public Entity user { get; set; }
        public string mobileField { get; set; }
        public string toType { get; set; }
        public Guid toGuid { get; set; }
        public string mobile { get; set; }
        public int userPreferredLanguage { get; set; }
    }


}


