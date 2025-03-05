using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Features.Common.ElmReferencedEntities;
using MOHU.Integration.Domain.Features.Countries.Constants;
using MOHU.Integration.Domain.Features.Countries.Enums;

namespace MOHU.Integration.Domain.Features.Countries;

public partial class Country : CrmElmReferencedEntity
{
    private Country(Entity entity)
        : base(entity)
    {
        LdvId = entity.GetAttributeValue<string>(CountriesConstants.Fields.LdvId);
        ArabicName = entity.GetAttributeValue<string>(CountriesConstants.Fields.ArabicName);
        EnglishName = entity.GetAttributeValue<string>(CountriesConstants.Fields.EnglishName);
        Code = entity.GetAttributeValue<string>(CountriesConstants.Fields.Code);
    }
    
    private Country(
        EntityReference id, 
        int? elmReferenceId,
        ElmEntityTypeEnum? elmEntityType,
        string? ldvId,
        string? arabicName,
        string? englishName,
        string? code)
        : base(id, elmReferenceId)
    {
        ElmEntityType = elmEntityType;
        LdvId = ldvId;
        ArabicName = arabicName;
        EnglishName = englishName;
        Code = code;
    }
    
    public string? LdvId { get; init; }

    public string? ArabicName { get; init; }

    public string? EnglishName { get; init; }

    public string? Code { get; init; }

    public ElmEntityTypeEnum? ElmEntityType { get; init; }

    public static Country Create(Entity entity) => new(entity);

    public static Country Create(
        EntityReference id,
        int? elmReferenceId,
        ElmEntityTypeEnum? elmEntityType,
        string? ldvId,
        string? arabicName,
        string? englishName,
        string? code) => new (id, elmReferenceId, elmEntityType, ldvId, arabicName, englishName, code);

    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        entity.EnsureCanCreateFrom(objectToCreate: nameof(Country), CountriesConstants.LogicalName);
        
        entity.AssignIfNotNull(
            CountriesConstants.Fields.ElmEntityType, 
            ElmEntityType.ToOptionSetValue()
        );
        entity.AssignIfNotNull(CountriesConstants.Fields.LdvId,  LdvId);
        entity.AssignIfNotNull(CountriesConstants.Fields.ArabicName,  ArabicName);
        entity.AssignIfNotNull(CountriesConstants.Fields.EnglishName,  EnglishName);
        entity.AssignIfNotNull(CountriesConstants.Fields.Code,  Code);
        
        return entity;
    }
}