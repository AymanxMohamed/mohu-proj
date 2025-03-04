using MOHU.Integration.Domain.Individuals.Enums;

namespace MOHU.Integration.Domain.Individuals;

public class Individual
{
    public Individual()
    {
    }

    private Individual(Entity entity)
    {
    }
    
    public Guid Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }
    
    public string? EnglishName { get; init; }
    
    public string? ArabicName { get; init; }
    
    public GenderEnum Gender { get; init; }
    
    public MartialStatusEnum MartialStatus { get; init; }
    
    public DateTime BirthDate { get; init; }

    public string? PlaceOfBirth { get; init; }
    
    public string? HijriBirthDate { get; init; }
    
    public string? Email { get; init; }

    public string? MobileNumber { get; init; }
    
    public IdTypeEnum IdType { get; init; }

    public string? IdNumber { get; init; }

    public string? PassportNumber { get; init; }

    public Guid? NationalityId { get; init; }

    public Guid? CountryOfResidence { get; init; }
    
    public string? HajVisaPermitStatus { get; init; }
    
    public string? OriginCode { get; init; }
    
    public int ElmReferenceId { get; init; }

    

    public static Individual Create(Entity entity)
    {
        return new Individual();
    }
}