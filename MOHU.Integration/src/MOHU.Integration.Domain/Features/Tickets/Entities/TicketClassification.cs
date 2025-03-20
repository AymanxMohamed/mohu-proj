using MOHU.Integration.Domain.Features.Tickets.Constants;

namespace MOHU.Integration.Domain.Features.Tickets.Entities;

public class TicketClassification
{
    private TicketClassification(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketClassification), TicketsConstants.LogicalName);
        
        RequestType = entity.GetAttributeValue<EntityReference>(TicketsConstants.Classification.Fields.RequestType);
        Service = entity.GetAttributeValue<EntityReference>(TicketsConstants.Classification.Fields.Service);
        MainCategory = entity.GetAttributeValue<EntityReference>(TicketsConstants.Classification.Fields.MainCategory);
        SubCategory = entity.GetAttributeValue<EntityReference>(TicketsConstants.Classification.Fields.SubCategory);
        SecondarySubCategory = entity.GetAttributeValue<EntityReference>(TicketsConstants.Classification.Fields.SecondarySubCategory);
    }

    public TicketClassification(
        EntityReference? requestType, 
        EntityReference? service, 
        EntityReference? mainCategory,
        EntityReference? subCategory, 
        EntityReference? secondarySubCategory)
    {
        RequestType = requestType;
        Service = service;
        MainCategory = mainCategory;
        SubCategory = subCategory;
        SecondarySubCategory = secondarySubCategory;
    }

    public EntityReference? RequestType { get; init; }
    
    public EntityReference? Service { get; init; }
    
    public EntityReference? MainCategory { get; init; }
    
    public EntityReference? SubCategory { get; init; }
    
    public EntityReference? SecondarySubCategory { get; init; }
    

    public static TicketClassification Create(Entity entity) => new(entity);

    public static TicketClassification Create(
        EntityReference? requestType, 
        EntityReference? service, 
        EntityReference? mainCategory,
        EntityReference? subCategory, 
        EntityReference? secondarySubCategory) => 
        new (requestType, service, mainCategory, subCategory, secondarySubCategory);

    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketClassification), TicketsConstants.LogicalName);
        
        entity.AssignIfNotNull(TicketsConstants.Classification.Fields.RequestType, RequestType);
        entity.AssignIfNotNull(TicketsConstants.Classification.Fields.Service, Service);
        entity.AssignIfNotNull(TicketsConstants.Classification.Fields.MainCategory, MainCategory);
        entity.AssignIfNotNull(TicketsConstants.Classification.Fields.SubCategory, SubCategory);
        entity.AssignIfNotNull(TicketsConstants.Classification.Fields.SecondarySubCategory, SecondarySubCategory);
    }
}