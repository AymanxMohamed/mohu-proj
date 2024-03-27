using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Logger;

namespace LinkDev.Common.Crm.Bll.Base
{
    public abstract class BllBase
    {
        public ILogger Logger { get; private set; }
        public IOrganizationService OrganizationService { get; private set; }
        public string LanguageCode { get; private set; }
        public BllBase(IOrganizationService organizationService, ILogger logger,string languageCode)
        {
            OrganizationService = organizationService;
            Logger = logger;
            LanguageCode = languageCode;
        }
    }
}
