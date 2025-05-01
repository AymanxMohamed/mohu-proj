using System.Reflection;
using Microsoft.Xrm.Sdk.Query;

namespace MOHU.Integration.Domain.Features.Tickets.Constants;

public static partial class TicketsConstants
{
    public static class IntegrationInformation
    {
        public static class Fields
        {
            public const string DepartmentDecision = "ldv_departmentdecisioncode";
            public const string DepartmentClosureReasonsComment = "ldv_closurereasons";
            public const string NeedMoreDetailsComment = "ldv_needsmoredetails";
            public const string IntegrationClosureReason = "ldv_integrationclosurereason";
            public const string IntegrationClosureDate = "ldv_integrationclosuredate";
            public const string IntegrationUpdatedBy = "ldv_integrationupdatedby";
            public const string IntegrationComment = "ldv_integrationcomment";
            public const string IntegrationLastActionDate = "ldv_integrationlastactiondate";
            public const string IntegrationStatus = "ldv_integrationstatuscode";
            public const string IsCompanyPortalUpdated = "ldv_iscompanyportalupdated";
            public const string IsNusukPortalUpdated = "ldv_isnusukportalupdated";
            public const string CompanyServiceNeedMoreInformation = "ldv_companiesserviceneededinformation";
            public const string CompanyServiceDecisionCode = "ldv_companiesservicedecisioncode";
        }
    }
}