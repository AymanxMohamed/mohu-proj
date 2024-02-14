using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Gea.Crm.Bll.Common
{
    public class ConfigurationKeys
    {
        public object GetPropertyValueByName(string key)
        {
            var propertyInfos
                = typeof(ConfigurationKeys).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var item in propertyInfos)
            {
                if (item.Name == key)
                    return item.GetValue(this);

            }
            throw new KeyNotFoundException($"'{key}' not found in configuration keys");
        }

        public bool EnableSystemLoggingErrorsBoolean { get; set; }
        public bool EnableSystemLoggingWarningsBoolean { get; set; }
        public bool EnableSystemLoggingInfoBoolen { get; set; }
        public double ConfigurationEntityCachingDurationInMinutes { get; set; }
        public double ServiceDefinitionEntityCachingDurationInMinutes { get; set; }
        public double LocalizedOptionSetsCachingDurationInMinutes { get; set; }
        public string PortalUrl { get; set; }
        public string PortalAPI { get; set; }
        public string CrmDomain { get; set; }
        public double CRExpiryDateReminder1DurationInDays { get; set; }
        public double CRExpiryDateReminder2DurationInDays { get; set; }
        public double CRExpiryDateReminder3DurationInDays { get; set; }
        public string OrganizationName { get; set; }
        public string ReportingServer { get; set; }
        public string ReportingServerDomainName { get; set; }
        public string ReportingServerUserName { get; set; }
        public string ReportingServerPassword { get; set; }
        public string LiveShowAnonymousPermitUrl { get; set; }
        public string EntertainmentAnonymousPermitUrl { get; set; }
        public string EventAnonymousPermitUrl { get; set; }
        public Guid CRMAdminGuid { get; set; }
        public int CustomJobsMonitorWindowsServiceThresholdInMinutes { get; set; }
        public int CustomJobsMonitorWindowsServiceCheckingIntervalInMinutes { get; set; }
        public Guid CustomJobsEngineOnDemandWFGuid { get; set; }
        public string GeaPaymentSystemUrlRetrieveFeesInfo { get; set; }
        public string ApiPaymentUrl { get; set; }
        public string SmsGatewayIntegrationUrl { get; set; }
        public string RecreationalFacilityAnonymousPermitUrl { get; set; }
        public bool EnableSmsGatewayIntegration { get; set; }
        public int EntertainmentCentrePermitExpireAfter { get; set; }
        public int RecreationalFacilityPermitExpireAfter { get; set; }
        public string AmusementParkAnonymousPermitUrl { get; set; }
        public int AmusementParkPermitExpireAfter { get; set; }
        public int AmusementRemainingDaysForExpiryNotLessThan { get; set; }
        public int AmusementRemainingDaysForExpiryNotGreaterThan { get; set; }
        public int ArtisticAndEntertainmentRemainingDaysForExpiryNotLessThan { get; set; }
        public int ArtisticAndEntertainmentRemainingDaysForExpiryNotGreaterThan { get; set; }
        public int TicketProviderLicenseRemainingDaysForExpiryNotLessThan { get; set; }
        public int TicketProviderLicenseRemainingDaysForExpiryNotGreaterThan { get; set; }
        public string EntertainmentCentreLicenseAnonymousPermitUrl { get; set; }
        public int EntertainmentCentreRemainingDaysForExpiryNotLessThan { get; set; }
        public int EntertainmentCentreRemainingDaysForExpiryNotGreaterThan { get; set; }
        public int RecreationalFacilitiesRemainingDaysForExpiryNotLessThan { get; set; }
        public int RecreationalFacilitiesRemainingDaysForExpiryNotGreaterThan { get; set; }
        public int CrowdManagementRemainingDaysForExpiryNotLessThan { get; set; }
        public int CrowdManagementRemainingDaysForExpiryNotGreaterThan { get; set; }
        public int RecreationalFacilityOpertaionRemainingDaysForExpiryNotLessThan { get; set; }
        public int RecreationalFacilityOperationRemainingDaysForExpiryNotGreaterThan { get; set; }
        public string FakeCrJson { get; set; }
        public int RecreationalFacilitiesOperationPermitExpireAfter { get; set; }
        public int ArtisticAndEntertainmentPermitExpireAfter { get; set; }
        public int CrowdmanagementCenterPermitExpireAfter { get; set; }
        public string CrowdmanagementCenterAnonymousPermitUrl { get; set; }
        public string RecreationalFacilitiesOperationAnonymousURL { get; set; }
        public string ArtisticAndEntertainmentAnonymousURL { get; set; }
        public int TicketProviderPermitExpireAfter { get; set; }
        public string TicketProviderLicenseAnonymousURL { get; set; }
        public string EfaaIntegrationUserName { get; set; }
        public string EfaaIntegrationPassword { get; set; }
        public string GeaEfaaServerIp { get; set; }
        public string TawakkalnaClientId { get; set; }
        public string TawakkalnaClientSecret { get; set; }
        public string TawakkalnaBaseUrl { get; set; }
        public int LicenseExpiryDateReminder1DurationInDays { get; set; }
    }
}
