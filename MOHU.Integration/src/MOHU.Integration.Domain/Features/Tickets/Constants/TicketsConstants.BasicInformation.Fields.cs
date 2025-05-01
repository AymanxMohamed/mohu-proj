namespace MOHU.Integration.Domain.Features.Tickets.Constants;

public static partial class TicketsConstants
{
    public static class BasicInformation
    {
        public static class Fields
        {
            public const string Id = "incidentid";
            public const string TicketNumber = "ticketnumber";
            public const string Title = "title";
            public const string Description = "ldv_description";
            public const string Priority = "ldv_prioritycode";
            public const string Origin = "caseorigincode";
            public const string SubOrigin = "ldv_subsourceid";
            public const string Company = "ldv_company";
            public const string StatusReason = "ldv_substatusid";
            public const string PortalStatus = "ldv_portalstatusid";
        }

        public static class CompanyCheckFields
        {
            public const string StatusReason = "ldv_substatusid";
            public const string Company = "ldv_company";
        }
    }
}