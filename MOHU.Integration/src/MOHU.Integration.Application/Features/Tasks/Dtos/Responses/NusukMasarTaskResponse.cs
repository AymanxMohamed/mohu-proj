using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tasks;

namespace MOHU.Integration.Application.Features.Tasks.Dtos.Responses;

public class NusukMasarTaskResponse
{
    protected NusukMasarTaskResponse(CrmTask task)
    {
        Id = task.Id.Id;
        Status = task.Status.ToLookup();
        Priority = task.Priority.ToLookup();
        Decision = task.Decision;
        Comment = task.Comment;
        CreatedOn = task.CreatedOn;
        ModifiedOn = task.ModifiedOn;
        ActualEnd = task.ActualEnd;
        TaskType = task.TaskType.ToLookup();
    }
    
    public Guid Id { get; init; }

    public LookupResponse<int>? Status { get; init; }

    public LookupResponse<int>? Priority { get; init; }
    
    public string? Decision { get; init; }
    
    public string? Comment { get; init; }
    
    public LookupResponse<int>? TaskType { get; init; }

    public DateTime? CreatedOn { get; init; }
    
    public DateTime? ModifiedOn { get; init; }
    
    public DateTime? ActualEnd { get; init; }
    
    public static implicit operator NusukMasarTaskResponse?(CrmTask? task) => task is null 
        ? null 
        : new NusukMasarTaskResponse(task);

    public static NusukMasarTaskResponse Create(CrmTask crmTask) => new(crmTask);
}