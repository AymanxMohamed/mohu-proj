using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tasks;

namespace MOHU.Integration.Application.Features.Tasks.Dtos.Responses;

public class NusukMasarCrmTaskResponse
{
    private NusukMasarCrmTaskResponse(CrmTask task)
    {
        Id = task.Id.Id;
        Status = task.Status.ToLookup();
        Priority = task.Priority.ToLookup();
        TaskType = task.TaskType.ToLookup();
        CreatedOn = task.CreatedOn;
        ModifiedOn = task.ModifiedOn;
        ActualEnd = task.ActualEnd;
        Decision = task.Decision;
        Comment = task.Comment;
        ProcessingTimeInMinutes = task.ProcessingTimeInMinutes;
        IsResolvedBySla = task.IsResolvedBySla;
        LevelOneSla = NusukMasarSlaSpiInstanceResponse.Create(task.LevelOneSla);
        LevelTwoSla = NusukMasarSlaSpiInstanceResponse.Create(task.LevelTwoSla);
        LevelThreeSla = NusukMasarSlaSpiInstanceResponse.Create(task.LevelThreeSla);
    }
    
    public Guid Id { get; init; }

    public LookupResponse<int>? Status { get; init; }

    public LookupResponse<int>? Priority { get; init; }
    
    public LookupResponse<int>? TaskType { get; init; }

    public DateTime? CreatedOn { get; init; }
    
    public DateTime? ModifiedOn { get; init; }
    
    public DateTime? ActualEnd { get; init; }
    
    public string? Decision { get; init; }
    
    public string? Comment { get; init; }
    
    public int? ProcessingTimeInMinutes { get; init; }

    public bool? IsResolvedBySla { get; init; }

    public NusukMasarSlaSpiInstanceResponse? LevelOneSla { get; init; }
    
    public NusukMasarSlaSpiInstanceResponse? LevelTwoSla { get; init; }
    
    public NusukMasarSlaSpiInstanceResponse? LevelThreeSla { get; init; }

    public static implicit operator NusukMasarCrmTaskResponse(CrmTask task) => new(task);

    public static NusukMasarCrmTaskResponse Create(CrmTask crmTask) => new(crmTask);
}