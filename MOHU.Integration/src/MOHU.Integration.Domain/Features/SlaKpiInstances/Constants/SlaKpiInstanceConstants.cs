using Microsoft.Xrm.Sdk.Query;

namespace MOHU.Integration.Domain.Features.SlaKpiInstances.Constants;

public class SlaKpiInstanceConstants
{
    public const string LogicalName = "slakpiinstance";

    public static class Fields
    {
        public const string ApplicableFromValue = "applicablefromvalue";
        public const string ComputedFailureTime = "computedfailuretime";
        public const string ComputedWarningTime = "computedwarningtime";
        public const string ElapsedTime = "elapsedtime";
        public const string FailureTime = "failuretime";
        public const string Status = "status";
        public const string SucceedOn = "succeededon";
        public const string WarningTime = "warningtime";
        public const string WarningTimeReached = "warningtimereached";
    }
}