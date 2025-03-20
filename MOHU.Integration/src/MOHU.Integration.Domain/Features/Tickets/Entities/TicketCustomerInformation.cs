using MOHU.Integration.Domain.Features.Individuals.Constants;
using MOHU.Integration.Domain.Features.Tickets.Constants;

namespace MOHU.Integration.Domain.Features.Tickets.Entities;

public class TicketCustomerInformation
{
    private TicketCustomerInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketCustomerInformation), TicketsConstants.LogicalName);
        
        CustomerReference = entity.GetAttributeValue<EntityReference>(TicketsConstants.CustomerInformation.Fields.CustomerReference);
    }
    
    private TicketCustomerInformation(
        EntityReference? customerReference)
    {
        CustomerReference = customerReference;
    }
    
    public bool IsIndividual => CustomerReference is { LogicalName: IndividualConstants.LogicalName };
    
    public EntityReference? CustomerReference { get; init; }

    public static TicketCustomerInformation Create(Entity entity) => new(entity);

    public static TicketCustomerInformation Create(EntityReference? customerReference) => 
        new (customerReference);

    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketCustomerInformation), TicketsConstants.LogicalName);
        entity.AssignIfNotNull(TicketsConstants.CustomerInformation.Fields.CustomerReference, CustomerReference);
    }
}