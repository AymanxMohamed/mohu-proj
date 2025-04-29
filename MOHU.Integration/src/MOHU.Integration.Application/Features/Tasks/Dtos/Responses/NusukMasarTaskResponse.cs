using MOHU.Integration.Domain.Features.Tasks;

namespace MOHU.Integration.Application.Features.Tasks.Dtos.Responses;

public class NusukMasarCrmUserTaskResponse
{
    private NusukMasarCrmUserTaskResponse(CrmTask task)
    {
        Id = task.Id.Id;
        Decision = task.Decision;
        Comment = task.Comment;
    }
    
    public Guid Id { get; init; }

    public string? Decision { get; init; }
    
    public string? Comment { get; init; }
    
    public static implicit operator NusukMasarCrmUserTaskResponse?(CrmTask? task) => task is null 
        ? null 
        : new NusukMasarCrmUserTaskResponse(task);

    public static NusukMasarCrmUserTaskResponse Create(CrmTask crmTask) => new(crmTask);
}