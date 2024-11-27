using Linkdev.CRM.DataContracts.Enums;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.NotificationTemplates.Entities
{
    public class NotificationConfigurations
    {
        public  List<EntityReference> toParty;
        public  List<EntityReference> ccParty;
        //public List<string> MobileNumbers;
        //public List<Guid> contactId;
        public List<Entity> contactLst;
        public List<Entity> accountLst;

        public Entity notificationTemp;
        public Guid notificationTempid;
        public Language Language;
        public EntityReference NotificationConfiguration;

    }
   
}
