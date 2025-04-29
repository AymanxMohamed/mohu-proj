namespace MOHU.Integration.Domain.Features.SlaKpiInstances.Enums;

public enum SlaKpiInstanceStatusEnum
{
    InProgress = 0,
    NonCompliant = 1,
    NearingNoncompliance = 2,
    Paused = 3,
    Succeeded = 4,
    Cancelled = 5,
}