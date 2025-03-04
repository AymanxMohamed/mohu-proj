using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Individuals.Constants;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualIdentification
{
    private IndividualIdentification(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualIdentification), IndividualConstants.LogicalName);
        
        IdNumber = entity.GetAttributeValue<string>(IndividualConstants.Fields.Identification.IdNumber);
        
        PassportNumber = entity.GetAttributeValue<string>(IndividualConstants.Fields.Identification.PassportNumber);
        
        IdType = entity
            .GetOptionSetValue(IndividualConstants.Fields.Identification.IdType)
            .ToEnum<IdTypeEnum>();
    }

    private IndividualIdentification(IdTypeEnum? idType, string? idNumber, string? passportNumber)
    {
        IdType = idType;
        IdNumber = idNumber;
        PassportNumber = passportNumber;
    }

    public IdTypeEnum? IdType { get; init; }

    public string? IdNumber { get; init; }

    public string? PassportNumber { get; init; }
    
    public static IndividualIdentification Create(Entity entity) => new(entity);

    public static IndividualIdentification Create(IdTypeEnum? idType, string? idNumber, string? passportNumber)
        => new(idType, idNumber, passportNumber);
    
    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualIdentification), IndividualConstants.LogicalName);

        entity.AssignIfNotNull(
                IndividualConstants.Fields.Identification.PassportNumber, 
                PassportNumber);

        entity.AssignIfNotNull(
            IndividualConstants.Fields.Identification.IdType, 
            IdType.ToOptionSetValue()
        );
    }
}