using System.Text.Json;
using MOHU.Integration.WebApi.Common.WebHooks;

namespace MOHU.Integration.WebApi.Features.Mohu.Webhooks;

public class MohuTicketCreatedWebHook : AppWebHook
{
    public override IActionResult HandleWebhook(JsonElement jsonElement)
    {
        var ticket = jsonElement.ToObject<MohuTicketCreatedPayload>();

        if (ticket == null)
        {
            return Ok();
        }
        
        throw new NotImplementedException();
    }
    
    // private readonly ITaskService _taskService;
    //
    // public SetListTasksLastYearTaskUrlWebHook(ITaskService taskService)
    // {
    //     _taskService = taskService;
    // }

    // public override IActionResult HandleWebhook([FromBody] JsonElement jsonElement)
    // {
    //     var taskWebHook = jsonElement.ToObject<WebHookRoot<ClickUpTask>>();
    //
    //     var triggeredTask = taskWebHook?.Payload;
    //
    //     if (triggeredTask is null)
    //         throw new ApplicationException(
    //             $"{nameof(GetAffidavitTypeFromTaskThatHasSameIhoInDoeRepsListWebhook)} Task can't be null");
    //
    //     _taskService.SetListTasksUrlCustomFieldWithCustomFieldHasTaskId(
    //         triggeredTask.List.Id,
    //         StudentTaskCustomFieldConstants.LastYearTaskUrl,
    //         taskCustomFieldId: StudentTaskCustomFieldConstants.LastYearTaskId);
    //     
    //     return Ok();
    // }
}