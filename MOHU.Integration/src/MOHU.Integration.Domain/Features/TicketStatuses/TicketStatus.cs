using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.TicketStatuses.Constants;

namespace MOHU.Integration.Domain.Features.TicketStatuses;

public class TicketStatus : CrmEntity
{
    protected TicketStatus(Entity entity)
        : base(entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketStatus), TicketStatusesConstants.LogicalName);
        
        Name = entity.GetAttributeValue<string>(TicketStatusesConstants.Fields.Name);
        Code = entity.GetAttributeValue<string>(TicketStatusesConstants.Fields.Code);
        EnglishName = entity.GetAttributeValue<string>(TicketStatusesConstants.Fields.EnglishName);
        ArabicName = entity.GetAttributeValue<string>(TicketStatusesConstants.Fields.ArabicName);
    }
    
    public string? Name { get; init; }

    public string? Code { get; init; }

    public string? EnglishName { get; init; }

    public string? ArabicName { get; init; }

    public static TicketStatus Create(Entity entity) => new(entity);
    
    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketStatus), TicketStatusesConstants.LogicalName);
        entity.AssignIfNotNull(TicketStatusesConstants.Fields.Name, Name);
        entity.AssignIfNotNull(TicketStatusesConstants.Fields.Code, Code);
        entity.AssignIfNotNull(TicketStatusesConstants.Fields.EnglishName, EnglishName);
        entity.AssignIfNotNull(TicketStatusesConstants.Fields.ArabicName, ArabicName);

        return entity;
    }
}