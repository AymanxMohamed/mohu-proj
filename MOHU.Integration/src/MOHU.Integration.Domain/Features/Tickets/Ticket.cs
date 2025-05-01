using Common.Crm.Domain.Common.Factories;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.Tasks;
using MOHU.Integration.Domain.Features.Tasks.Enums;
using MOHU.Integration.Domain.Features.Tickets.Constants;
using MOHU.Integration.Domain.Features.Tickets.Entities;
using MOHU.Integration.Domain.Features.Tickets.Enums;
using Newtonsoft.Json;

namespace MOHU.Integration.Domain.Features.Tickets;

public partial class Ticket : CrmEntity
{
    private Ticket(Entity entity)
        : base(entity)
    {
        BasicInformation = TicketBasicInformation.Create(entity);
        IntegrationInformation = TicketIntegrationInformation.Create(entity);
        Classification = TicketClassification.Create(entity);
        CustomerInformation = TicketCustomerInformation.Create(entity);
    }
    
    private Ticket(
        EntityReference? id, 
        TicketBasicInformation basicInformation,
        TicketIntegrationInformation integrationInformation,
        TicketClassification classification,
        TicketCustomerInformation customerInformation)
        : base(id ?? EntityReferenceFactory.Create(TicketsConstants.LogicalName))
    {
        BasicInformation = basicInformation;
        IntegrationInformation = integrationInformation;
        Classification = classification;
        CustomerInformation = customerInformation;
    }

    public TicketBasicInformation BasicInformation { get; init; }
    
    public TicketIntegrationInformation IntegrationInformation { get; init; }

    public TicketClassification Classification { get; init; }
    
    public TicketCustomerInformation CustomerInformation { get; init; }

    [JsonIgnore]
    public List<CrmTask> Tasks { get; private set; } = [];
    
    public CrmTask? LastCrmUserTask => Tasks
        .FirstOrDefault(x => x.TaskType == TaskTypeEnum.BusinessUser);
    
    [JsonIgnore]
    public CrmTask? LastOpenCompanyTask => Tasks
        .FirstOrDefault(x => x is { TaskType: TaskTypeEnum.Company, Status: TaskStatusEnum.Open });

    public static Ticket Create(Entity entity) => new(entity);

    public static Ticket Create(
        EntityReference? id,
        TicketBasicInformation basicInformation,
        TicketIntegrationInformation integrationInformation,
        TicketClassification classification,
        TicketCustomerInformation customerInformation) => 
        new (id, basicInformation, integrationInformation, classification, customerInformation);


    public void UpdateIntegrationInformation(string comment, string updatedBy, IntegrationStatus integrationStatus)
    {
        var status = Classification.IsErshadOrMafkoden() 
            ? IntegrationStatus.CloseTheTicket 
            : integrationStatus; 
        
        IntegrationInformation.Update(comment, updatedBy, status);
    }
    
    public void Activate(TicketActiveStatusReasonEnum statusReason = TicketActiveStatusReasonEnum.InProgress)
        => BasicInformation.Activate(statusReason);
    
    public void Cancel(TicketCancelledStatusReasonEnum statusReason = TicketCancelledStatusReasonEnum.Cancelled)
        => BasicInformation.Cancel(statusReason);
    
    public void Resolve(TicketResolvedStatusReasonEnum statusReason = TicketResolvedStatusReasonEnum.TicketResolved)
        => BasicInformation.Resolve(statusReason);

    public Ticket SetCrmTasks(List<CrmTask> tasks)
    {
        Tasks = tasks;
        return this;
    }

    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        BasicInformation.UpdateEntity(entity);
        IntegrationInformation.UpdateEntity(entity);
        Classification.UpdateEntity(entity);
        CustomerInformation.UpdateEntity(entity);
        
        return entity;
    }
}
