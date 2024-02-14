using LinkDev.CRM.Library.DAL;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.BLL
{
    class TaskCommentHistoryBLL
    {
        private IOrganizationService OrganizationService;
        //private CrmLog log;
        private CRMAccessLayer crmAccess;

        /// <summary>
        /// the constractor which have service and log as input parameter
        /// </summary>
        /// <param name="service"></param>
        /// <param name="log"></param>
        public TaskCommentHistoryBLL(IOrganizationService service)//,CrmLog log)
        {
            OrganizationService = service;
            crmAccess = new CRMAccessLayer(OrganizationService);
            // this.log = log;
        }
    }
}
