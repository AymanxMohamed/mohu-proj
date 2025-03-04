using MOHU.Integration.Domain.Individuals.Enums;

namespace MOHU.Integration.Domain.Individuals;

public class Individual
{
    private Individual(Entity entity)
    {
        Id = entity.ToEntityReference();
    }
    
    public EntityReference Id { get; init; }

    public DateTime BirthDate { get; init; }

    public string? PlaceOfBirth { get; init; }
    
    public string? HijriBirthDate { get; init; }
    
    public string? Email { get; init; }

    public string? MobileNumber { get; init; }
    
    public IdTypeEnum IdType { get; init; }

    public string? IdNumber { get; init; }

    public string? PassportNumber { get; init; }

    public EntityReference? NationalityId { get; init; }

    public EntityReference? CountryOfResidence { get; init; }
    
    public string? HajVisaPermitStatus { get; init; }
    
    public string? OriginCode { get; init; }
    
    public int ElmReferenceId { get; init; }

    

    public static Individual Create(Entity entity)
    {
        return new Individual(entity);
    }
}