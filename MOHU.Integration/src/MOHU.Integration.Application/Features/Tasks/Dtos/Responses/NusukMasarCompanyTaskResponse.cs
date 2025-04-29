using MOHU.Integration.Domain.Features.Tasks;

namespace MOHU.Integration.Application.Features.Tasks.Dtos.Responses;

public class NusukMasarCompanyTaskResponse : NusukMasarTaskResponse
{
    private NusukMasarCompanyTaskResponse(CrmTask task)
        : base(task)
    {
        ProcessingTimeInMinutes = task.ProcessingTimeInMinutes;
        IsResolvedBySla = task.IsResolvedBySla;
        LevelOneSla = NusukMasarSlaSpiInstanceResponse.Create(task.LevelOneSla);
        LevelTwoSla = NusukMasarSlaSpiInstanceResponse.Create(task.LevelTwoSla);
        LevelThreeSla = NusukMasarSlaSpiInstanceResponse.Create(task.LevelThreeSla);
    }
    
    public int? ProcessingTimeInMinutes { get; init; }

    public bool? IsResolvedBySla { get; init; }

    public NusukMasarSlaSpiInstanceResponse? LevelOneSla { get; init; }
    
    public NusukMasarSlaSpiInstanceResponse? LevelTwoSla { get; init; }
    
    public NusukMasarSlaSpiInstanceResponse? LevelThreeSla { get; init; }

    public static implicit operator NusukMasarCompanyTaskResponse?(CrmTask? task) => task is null 
        ? null 
        : new NusukMasarCompanyTaskResponse(task);

    public new static NusukMasarCompanyTaskResponse Create(CrmTask crmTask) => new(crmTask);
}