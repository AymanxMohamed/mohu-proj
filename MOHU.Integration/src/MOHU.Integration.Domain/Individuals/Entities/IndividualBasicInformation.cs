using MOHU.Integration.Domain.Individuals.Enums;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualBasicInformation
{
    public IndividualBasicInformation(Entity entity)
    {
    }
    
    public string? FirstName { get; init; }

    public string? LastName { get; init; }
    
    public string? EnglishName { get; init; }
    
    public string? ArabicName { get; init; }
    
    public GenderEnum Gender { get; init; }
    
    public MartialStatusEnum MartialStatus { get; init; }
    
    public static IndividualBasicInformation FromEntity(Entity entity) => new(entity);
}