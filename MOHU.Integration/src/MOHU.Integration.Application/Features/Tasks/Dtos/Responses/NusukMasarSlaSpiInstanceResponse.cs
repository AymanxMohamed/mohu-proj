using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.SlaKpiInstances;

namespace MOHU.Integration.Application.Features.Tasks.Dtos.Responses;

public class NusukMasarSlaSpiInstanceResponse
{
    private NusukMasarSlaSpiInstanceResponse(SlaKpiInstance slaKpiInstance)
    {
        FailureTime = slaKpiInstance.FailureTime;
        Status = slaKpiInstance.Status.ToLookup();
        WarningTime = slaKpiInstance.WarningTime;
    }

    public DateTime? FailureTime { get; init; }
    
    public LookupResponse<int>? Status { get; init; }
    
    public DateTime? WarningTime { get; init; }

    public static implicit operator NusukMasarSlaSpiInstanceResponse(SlaKpiInstance slaKpiInstance)
        => new(slaKpiInstance);

    public static NusukMasarSlaSpiInstanceResponse? Create(SlaKpiInstance? slaKpiInstance)
        => slaKpiInstance is null 
            ? null 
            : new NusukMasarSlaSpiInstanceResponse(slaKpiInstance);
}